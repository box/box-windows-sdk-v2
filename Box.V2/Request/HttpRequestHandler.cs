using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Utility;

namespace Box.V2.Request
{
    public class HttpRequestHandler : IRequestHandler
    {
        public const HttpStatusCode TooManyRequests = (HttpStatusCode)429;
        public const int RetryLimit = 5;
        private readonly TimeSpan _defaultRequestTimeout = new TimeSpan(0, 0, 100); // 100 seconds, same as default HttpClient timeout
        private readonly TimeSpan _timeout;

        public HttpRequestHandler(IWebProxy webProxy = null, TimeSpan? timeout = null)
        {
            ClientFactory.WebProxy = webProxy;
#if NET462
            System.Net.ServicePointManager.Expect100Continue = false;
#endif
            _timeout = timeout ?? _defaultRequestTimeout;
        }

        public async Task<IBoxResponse<T>> ExecuteAsyncWithoutRetry<T>(IBoxRequest request)
            where T : class
        {
            // Need to account for special cases when the return type is a stream
            var isStream = typeof(T) == typeof(Stream);
            HttpResponseMessage response = null;

            try
            {
                // TODO: yhu@ better handling of different request
                var isMultiPartRequest = request.GetType() == typeof(BoxMultiPartRequest);
                var isBinaryRequest = request.GetType() == typeof(BoxBinaryRequest);

                HttpRequestMessage httpRequest = GetHttpRequest(request, isMultiPartRequest, isBinaryRequest);
                Debug.WriteLine(string.Format("RequestUri: {0}", httpRequest.RequestUri));
                response = await GetResponse(request, isStream, httpRequest).ConfigureAwait(false);
                BoxResponse<T> boxResponse = await GetBoxResponse<T>(isStream, response).ConfigureAwait(false);

                return boxResponse;
            }
            catch (Exception ex)
            {
                response?.Dispose();
                Debug.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
        }

        public async Task<IBoxResponse<T>> ExecuteAsync<T>(IBoxRequest request)
            where T : class
        {
            // Need to account for special cases when the return type is a stream
            var isStream = typeof(T) == typeof(Stream);
            var retryCounter = 0;
            var expBackoff = new ExponentialBackoff();
            HttpResponseMessage response = null;

            try
            {
                // TODO: yhu@ better handling of different request
                var isMultiPartRequest = request.GetType() == typeof(BoxMultiPartRequest);
                var isBinaryRequest = request.GetType() == typeof(BoxBinaryRequest);

                while (true)
                {
                    using (HttpRequestMessage httpRequest = GetHttpRequest(request, isMultiPartRequest, isBinaryRequest))
                    {
                        Debug.WriteLine(string.Format("RequestUri: {0}", httpRequest.RequestUri));
                        response = await GetResponse(request, isStream, httpRequest).ConfigureAwait(false);
                        //need to wait for Retry-After seconds and then retry request
                        var retryAfterHeader = response.Headers.RetryAfter;

                        // If we get a retryable/transient error code and this is not a multi part request (meaning a file upload, which cannot be retried
                        // because the stream cannot be reset) and we haven't exceeded the number of allowed retries, then retry the request.
                        // If we get a 202 code and has a retry-after header, we will retry after
                        if (!isMultiPartRequest &&
                            (response.StatusCode == TooManyRequests
                            ||
                            response.StatusCode == HttpStatusCode.InternalServerError
                            ||
                            response.StatusCode == HttpStatusCode.BadGateway
                            ||
                            response.StatusCode == HttpStatusCode.ServiceUnavailable
                            ||
                            response.StatusCode == HttpStatusCode.GatewayTimeout
                            ||
                            (response.StatusCode == HttpStatusCode.Accepted && retryAfterHeader != null))
                            && retryCounter++ < RetryLimit)
                        {
                            response?.Dispose();
                            TimeSpan delay = expBackoff.GetRetryTimeout(retryCounter);

                            Debug.WriteLine("HttpCode : {0}. Waiting for {1} seconds to retry request. RequestUri: {2}", response.StatusCode, delay.Seconds, httpRequest.RequestUri);

                            await Task.Delay(delay);
                        }
                        else
                        {
                            BoxResponse<T> boxResponse = await GetBoxResponse<T>(isStream, response).ConfigureAwait(false);

                            return boxResponse;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response?.Dispose();
                Debug.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
        }
        private HttpRequestMessage GetHttpRequest(IBoxRequest request, bool isMultiPartRequest, bool isBinaryRequest)
        {
            HttpRequestMessage httpRequest;
            if (isMultiPartRequest)
            {
                httpRequest = BuildMultiPartRequest(request as BoxMultiPartRequest);
            }
            else if (isBinaryRequest)
            {
                httpRequest = BuildBinaryRequest(request as BoxBinaryRequest);
            }
            else
            {
                httpRequest = BuildRequest(request);
            }

            // Add headers
            foreach (var kvp in request.HttpHeaders)
            {
                // They could not be added to the headers directly
                if (kvp.Key == Constants.RequestParameters.ContentMD5
                    || kvp.Key == Constants.RequestParameters.ContentRange)
                {
                    httpRequest.Content.Headers.Add(kvp.Key, kvp.Value);
                }
                else
                {
                    httpRequest.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);
                }
            }

            return httpRequest;
        }

        private async Task<HttpResponseMessage> GetResponse(IBoxRequest request, bool isStream, HttpRequestMessage httpRequest)
        {
            // If we are retrieving a stream, we should return without reading the entire response
            HttpCompletionOption completionOption = isStream ?
                HttpCompletionOption.ResponseHeadersRead :
                HttpCompletionOption.ResponseContentRead;

            HttpClient client = GetClient(request);

            HttpResponseMessage response;
            using (var cts = new CancellationTokenSource())
            {
                if (request.Timeout.HasValue)
                {
                    if (request.Timeout.Value != Timeout.InfiniteTimeSpan)
                    {
                        cts.CancelAfter(request.Timeout.Value);
                    }
                }
                else
                {
                    cts.CancelAfter(_timeout);
                }

                var timeoutToken = cts.Token;

                try
                {
                    // Not disposing the reponse since it will affect stream response

                    response = await client.SendAsync(httpRequest, completionOption, timeoutToken).ConfigureAwait(false);
                }
                catch (TaskCanceledException ex)
                {
                    if (timeoutToken.IsCancellationRequested)
                    {
                        // Request timed out
                        throw new TimeoutException("Request timed out", ex);
                    }

                    // Request was canceled for unknown reason
                    throw ex;
                }
            }

            return response;
        }

        private static async Task<BoxResponse<T>> GetBoxResponse<T>(bool isStream, HttpResponseMessage response) where T : class
        {
            var boxResponse = new BoxResponse<T>
            {
                Headers = response.Headers,

                // Translate the status codes that interest us 
                StatusCode = response.StatusCode
            };
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.NoContent:
                case HttpStatusCode.Found:
                case HttpStatusCode.PartialContent: // Download with range
                    boxResponse.Status = ResponseStatus.Success;
                    break;
                case HttpStatusCode.Accepted:
                    boxResponse.Status = ResponseStatus.Pending;
                    break;
                case HttpStatusCode.Unauthorized:
                    boxResponse.Status = ResponseStatus.Unauthorized;
                    break;
                case HttpStatusCode.Forbidden:
                    boxResponse.Status = ResponseStatus.Forbidden;
                    break;
                case TooManyRequests:
                    boxResponse.Status = ResponseStatus.TooManyRequests;
                    break;
                default:
                    boxResponse.Status = ResponseStatus.Error;
                    break;
            }

            if (isStream && boxResponse.Status == ResponseStatus.Success)
            {
                var resObj = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                boxResponse.ResponseObject = resObj as T;
            }
            else
            {
                boxResponse.ContentString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                // We can safely dispose the response now since all of it has been read
                response.Dispose();
            }

            return boxResponse;
        }

        private class ClientFactory
        {
            private static readonly Lazy<HttpClient> _autoRedirectClient =
                new Lazy<HttpClient>(() => CreateClient(true, WebProxy));

            private static readonly Lazy<HttpClient> _nonAutoRedirectClient =
                new Lazy<HttpClient>(() => CreateClient(false, WebProxy));

            public static IWebProxy WebProxy { get; set; }

            // reuseable HttpClient instance
            public static HttpClient AutoRedirectClient { get { return _autoRedirectClient.Value; } }
            public static HttpClient NonAutoRedirectClient { get { return _nonAutoRedirectClient.Value; } }

            private static HttpClient CreateClient(bool followRedirect, IWebProxy webProxy)
            {
                var handler = new HttpClientHandler();
                if (handler.SupportsAutomaticDecompression)
                {
                    handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                }
                handler.AllowAutoRedirect = followRedirect;

                if (webProxy != null)
                {
                    handler.UseProxy = true;
                    handler.Proxy = webProxy;
                }

                // Ensure that clients use non-deprecated versions of TLS (i.e. TLSv1.1 or greater)
#if NETSTANDARD2_0
                try
                {
                    handler.SslProtocols |= System.Security.Authentication.SslProtocols.Tls12;
                }
                catch (Exception)
                {
                    Debug.WriteLine("Could not set TLSv1.2 security protocol!");
                }
#elif NET462
                System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
#else
                FAIL THE BUILD
#endif

                var client = new HttpClient(handler)
                {
                    Timeout = Timeout.InfiniteTimeSpan // Don't let HttpClient time out the request, since we manually handle timeout cancellation
                };
                return client;
            }
        }

        private HttpClient GetClient(IBoxRequest request)
        {
            return request.FollowRedirect ? ClientFactory.AutoRedirectClient : ClientFactory.NonAutoRedirectClient;
        }

        private HttpRequestMessage BuildRequest(IBoxRequest request)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = request.AbsoluteUri,
                Method = GetHttpMethod(request.Method)
            };
            if (httpRequest.Method == HttpMethod.Get)
            {
                return httpRequest;
            }

            HttpContent content;

            // Set request content to string or form-data
            if (!string.IsNullOrWhiteSpace(request.Payload))
            {
                content = string.IsNullOrEmpty(request.ContentType)
                    ? new StringContent(request.Payload)
                    : new StringContent(request.Payload, request.ContentEncoding, request.ContentType);
            }
            else
            {
                content = new FormUrlEncodedContent(request.PayloadParameters);
            }

            httpRequest.Content = content;

            return httpRequest;
        }

        private HttpRequestMessage BuildBinaryRequest(BoxBinaryRequest request)
        {
            var httpRequest = new HttpRequestMessage
            {
                RequestUri = request.AbsoluteUri,
                Method = GetHttpMethod(request.Method)
            };

            HttpContent content = null;

            var filePart = request.Part as BoxFilePart;
            if (filePart != null)
            {
                content = new ReusableContent(filePart.Value);
            }

            httpRequest.Content = content;

            return httpRequest;
        }

        private HttpMethod GetHttpMethod(RequestMethod requestMethod)
        {
            switch (requestMethod)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Options:
                    return HttpMethod.Options;
                default:
                    throw new InvalidOperationException("Http method not supported");
            }
        }

        private HttpRequestMessage BuildMultiPartRequest(BoxMultiPartRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, request.AbsoluteUri);
            var multiPart = new MultipartFormDataContent();

            // Break out the form parts from the request
            var filePart = request.Parts.Where(p => p.GetType() == typeof(BoxFileFormPart))
                .Select(p => p as BoxFileFormPart)
                .FirstOrDefault(); // Only single file upload is supported at this time
            var stringParts = request.Parts.Where(p => p.GetType() == typeof(BoxStringFormPart))
                .Select(p => p as BoxStringFormPart);

            // Create the string parts
            foreach (var sp in stringParts)
            {
                multiPart.Add(new StringContent(sp.Value), ForceQuotesOnParam(sp.Name));
            }

            // Create the file part
            if (filePart != null)
            {
                var fileContent = new ReusableContent(filePart.Value);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = ForceQuotesOnParam(filePart.Name),
                    FileName = ForceQuotesOnParam(filePart.FileName)
                };
                if (!string.IsNullOrEmpty(filePart.ContentType))
                {
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(filePart.ContentType);
                }
                multiPart.Add(fileContent);
            }

            httpRequest.Content = multiPart;

            return httpRequest;
        }

        /// <summary>
        /// Adds quotes around the named parameters
        /// This is required as the API will currently not take multi-part parameters without quotes
        /// </summary>
        /// <param name="name">The name parameter to add quotes to</param>
        /// <returns>The name parameter surrounded by quotes</returns>
        private string ForceQuotesOnParam(string name)
        {
            return string.Format("\"{0}\"", name);
        }
    }
}

