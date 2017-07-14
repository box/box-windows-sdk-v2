using Box.V2.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Box.V2.Request
{
    public class HttpRequestHandler : IRequestHandler
    {
        const HttpStatusCode TooManyRequests = (HttpStatusCode)429;

        public async Task<IBoxResponse<T>> ExecuteAsync<T>(IBoxRequest request)
            where T : class
        {
            // Need to account for special cases when the return type is a stream
            bool isStream = typeof(T) == typeof(Stream);
            var numRetries = 3;

            try
            {
                // TODO: yhu@ better handling of different request
                var isMultiPartRequest = request.GetType() == typeof(BoxMultiPartRequest);
                var isBinaryRequest = request.GetType() == typeof(BoxBinaryRequest);

                while (true)
                {
                    HttpRequestMessage httpRequest = null;

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

                    // If we are retrieving a stream, we should return without reading the entire response
                    HttpCompletionOption completionOption = isStream ?
                        HttpCompletionOption.ResponseHeadersRead :
                        HttpCompletionOption.ResponseContentRead;

                    Debug.WriteLine(string.Format("RequestUri: {0}", httpRequest.RequestUri));

                    HttpClient client = GetClient(request);

                    // Not disposing the reponse since it will affect stream response 
                    var response = await client.SendAsync(httpRequest, completionOption).ConfigureAwait(false);

                    //need to wait for Retry-After seconds and then retry request
                    var retryAfterHeader = response.Headers.RetryAfter;

                    // If we get a 429 error code and this is not a multi part request (meaning a file upload, which cannot be retried
                    // because the stream cannot be reset) and we haven't exceeded the number of allowed retries, then retry the request.
                    // If we get a 202 code and has a retry-after header, we will retry after
                    if (
                        ((response.StatusCode == TooManyRequests && !isMultiPartRequest)
                        ||
                        (response.StatusCode == HttpStatusCode.Accepted && retryAfterHeader != null)) 
                        && numRetries-- > 0)
                    {
                        TimeSpan delay = TimeSpan.FromSeconds(2);
                        if (retryAfterHeader.Delta.HasValue)
                        {
                            delay = retryAfterHeader.Delta.Value;
                        }

                        Debug.WriteLine("HttpCode : {0}. Waiting for {1} seconds to retry request. RequestUri: {2}", response.StatusCode, delay.Seconds, httpRequest.RequestUri);

                        await Task.Delay(delay);
                    }
                    else
                    {
                        BoxResponse<T> boxResponse = new BoxResponse<T>();
                        boxResponse.Headers = response.Headers;

                        // Translate the status codes that interest us 
                        boxResponse.StatusCode = response.StatusCode;
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
                        }

                        return boxResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
        }

        private class ClientFactory
        {
            private static readonly Lazy<HttpClient> autoRedirectClient =
                new Lazy<HttpClient>(() => CreateClient(true));

            private static readonly Lazy<HttpClient> nonAutoRedirectClient =
                new Lazy<HttpClient>(() => CreateClient(false));

            private static IDictionary<TimeSpan, HttpClient> httpClientCache = new Dictionary<TimeSpan, HttpClient>();

            // reuseable HttpClient instance
            public static HttpClient AutoRedirectClient { get { return autoRedirectClient.Value; } }
            public static HttpClient NonAutoRedirectClient { get { return nonAutoRedirectClient.Value; } }

            // Create new HttpClient per timeout
            public static HttpClient CreateClientWithTimeout(bool followRedirect, TimeSpan timeout)
            {
                lock (httpClientCache)
                {
                    HttpClient client = null;
                    if (!httpClientCache.ContainsKey(timeout))
                    {
                        // create new client with timeout
                        client = CreateClient(followRedirect);
                        client.Timeout = timeout;
                        // cache it
                        httpClientCache.Add(timeout, client);
                    }

                    return httpClientCache[timeout];
                }
            }

            private static HttpClient CreateClient(bool followRedirect)
            {
                HttpClientHandler handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip };
                handler.AllowAutoRedirect = followRedirect;

                return new HttpClient(handler);
            }
        }

        private HttpClient GetClient(IBoxRequest request)
        {
            HttpClient client = null;

            if (request.Timeout.HasValue)
            {
                var timeout = request.Timeout.Value;
                client = ClientFactory.CreateClientWithTimeout(request.FollowRedirect, timeout);
            }
            else
            {
                if (request.FollowRedirect)
                {
                    client = ClientFactory.AutoRedirectClient;
                }
                else
                {
                    client = ClientFactory.NonAutoRedirectClient;
                }
            }

            return client;
        }

        private HttpRequestMessage BuildRequest(IBoxRequest request)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = request.AbsoluteUri;
            httpRequest.Method = GetHttpMethod(request.Method);
            if (httpRequest.Method == HttpMethod.Get)
            {
                return httpRequest;
            }

            HttpContent content = null;

            // Set request content to string or form-data
            if (!string.IsNullOrWhiteSpace(request.Payload))
            {
                if (string.IsNullOrEmpty(request.ContentType))
                {
                    content = new StringContent(request.Payload);
                }
                else
                {
                    content = new StringContent(request.Payload, request.ContentEncoding, request.ContentType);
                }
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
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = request.AbsoluteUri;
            httpRequest.Method = GetHttpMethod(request.Method);

            HttpContent content = null;

            var filePart = request.Part as BoxFilePart;
            if (filePart != null)
            {
                content = new StreamContent(filePart.Value);
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
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, request.AbsoluteUri);
            MultipartFormDataContent multiPart = new MultipartFormDataContent();

            // Break out the form parts from the request
            var filePart = request.Parts.Where(p => p.GetType() == typeof(BoxFileFormPart))
                .Select(p => p as BoxFileFormPart)
                .FirstOrDefault(); // Only single file upload is supported at this time
            var stringParts = request.Parts.Where(p => p.GetType() == typeof(BoxStringFormPart))
                .Select(p => p as BoxStringFormPart);

            // Create the string parts
            foreach (var sp in stringParts)
                multiPart.Add(new StringContent(sp.Value), ForceQuotesOnParam(sp.Name));

            // Create the file part
            if (filePart != null)
            {
                StreamContent fileContent = new StreamContent(filePart.Value);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = ForceQuotesOnParam(filePart.Name),
                    FileName = ForceQuotesOnParam(filePart.FileName)
                };
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

