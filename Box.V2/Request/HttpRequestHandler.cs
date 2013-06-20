using System;
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
        private static HttpClient _client;
        private HttpClientHandler _handler;

        public HttpRequestHandler()
        {
            _handler = new HttpClientHandler();
            _client = new HttpClient(_handler);
        }

        public async Task<IBoxResponse<T>> ExecuteAsync<T>(IBoxRequest request)
            where T : class
        {
            // Need to account for special cases when the return type is a stream
            bool isStream = typeof(T) == typeof(Stream);

            HttpRequestMessage httpRequest = request.GetType() == typeof(BoxMultiPartRequest) ?
                                                BuildMultiPartRequest(request as BoxMultiPartRequest) :
                                                BuildRequest(request);

            // Add headers
            foreach (var kvp in request.HttpHeaders)
                httpRequest.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);

            // If we are retrieving a stream, we should return without reading the entire response
            HttpCompletionOption completionOption = isStream ?
                HttpCompletionOption.ResponseHeadersRead :
                HttpCompletionOption.ResponseContentRead;

#if DEBUG
            Debug.WriteLine(string.Format("RequestUri: {0}", httpRequest.RequestUri));//, RequestHeader: {1} , httpRequest.Headers.Select(i => string.Format("{0}:{1}", i.Key, i.Value)).Aggregate((i, j) => i + "," + j)));
#endif

            try
            {
                HttpResponseMessage response = await _client.SendAsync(httpRequest, completionOption);

                BoxResponse<T> boxResponse = new BoxResponse<T>();

                // Translate the status codes that interest us 
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                    case HttpStatusCode.NoContent:
                        boxResponse.Status = ResponseStatus.Success;
                        break;
                    case HttpStatusCode.Accepted:
                        boxResponse.Status = ResponseStatus.Pending;
                        break;
                    case HttpStatusCode.Unauthorized:
                        boxResponse.Status = ResponseStatus.Unauthorized;
                        break;
                    default:
                        boxResponse.Status = ResponseStatus.Error;
                        break;
                }

                if (isStream && boxResponse.Status == ResponseStatus.Success)
                {
                    using (var resObj = await response.Content.ReadAsStreamAsync())
                    {
                        MemoryStream ms = new MemoryStream();
                        //resObj.Position = 0;
                        await resObj.CopyToAsync(ms);
                        boxResponse.ResponseObject = ms as T;
                    }
                }
                else
                    boxResponse.ContentString = await response.Content.ReadAsStringAsync();

                return boxResponse;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Exception: {0}", ex.Message));
                throw;
            }
        }

        private HttpRequestMessage BuildRequest(IBoxRequest request)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = request.AbsoluteUri;
            //httpRequest.Content = new StringContent(request.GetQueryString(), Encoding.UTF8, "application/x-www-form-urlencoded");

            switch (request.Method)
            {
                case RequestMethod.Get:
                    httpRequest.Method = HttpMethod.Get;
                    return httpRequest;
                case RequestMethod.Put:
                    httpRequest.Method = HttpMethod.Put;
                    break;
                case RequestMethod.Delete:
                    httpRequest.Method = HttpMethod.Delete;
                    break;
                case RequestMethod.Post:
                    httpRequest.Method = HttpMethod.Post;
                    break;
                case RequestMethod.Options:
                    httpRequest.Method = HttpMethod.Options;
                    break;
                default:
                    throw new InvalidOperationException("Http method not supported");
            }

            httpRequest.Content = !string.IsNullOrWhiteSpace(request.Payload) ?
                (HttpContent)new StringContent(request.Payload) :
                (HttpContent)new FormUrlEncodedContent(request.PayloadParameters);

            return httpRequest;
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

            // Create the file part
            StreamContent fileContent = new StreamContent(filePart.Value);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = ForceQuotesOnParam(filePart.Name),
                FileName = ForceQuotesOnParam(filePart.FileName)
            };
            multiPart.Add(fileContent);

            // Create the string part
            foreach (var sp in stringParts)
                multiPart.Add(new StringContent(sp.Value), ForceQuotesOnParam(sp.Name));

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

