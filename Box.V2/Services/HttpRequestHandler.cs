using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Services
{
    public class HttpRequestHandler : IRequestHandler
    {
        private static HttpClient _client;
        private HttpClientHandler _handler;

        public HttpRequestHandler()
        {
            _handler = new HttpClientHandler();
            _client = new HttpClient(_handler);
            //_client.MaxResponseContentBufferSize = 1024 * 16;
        }

        public async Task<IBoxResponse<T>> ExecuteAsync<T>(IBoxRequest request) 
            where T : class
        {
            HttpRequestMessage httpRequest = request.GetType() == typeof(BoxMultiPartRequest) ?
                                                BuildMultiPartRequest(request as BoxMultiPartRequest) :
                                                BuildRequest(request);

            // Add headers
            foreach (var kvp in request.HttpHeaders)
                httpRequest.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);

            HttpResponseMessage response = await _client.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead);

            BoxResponse<T> boxResponse = new BoxResponse<T>()
            {
                // Sets the status to success, unauthorized, or error
                Status = response.IsSuccessStatusCode ?
                    ResponseStatus.Success :
                    response.StatusCode == System.Net.HttpStatusCode.Unauthorized ? 
                        ResponseStatus.Unauthorized :
                        ResponseStatus.Error
            };

            //if (typeof(T) == typeof(byte[]))
            //{
            //    var resObj = await response.Content.ReadAsByteArrayAsync();
            //    boxResponse.ResponseObject = (T)Convert.ChangeType(resObj, typeof(T), null);
            //}
            if (typeof(T) == typeof(Stream))
            {
                var resObj = await response.Content.ReadAsStreamAsync();

                boxResponse.ResponseObject = resObj as T;
            }
            else
                boxResponse.ContentString = await response.Content.ReadAsStringAsync();

            return boxResponse;
        }

        private HttpRequestMessage BuildRequest(IBoxRequest request)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = request.AbsoluteUri;
            //httpRequest.Content = new StringContent(request.GetQueryString(), Encoding.UTF8, "application/x-www-form-urlencoded");

            switch (request.Method)
            {
                case RequestMethod.GET:
                    httpRequest.Method = HttpMethod.Get;
                    return httpRequest;
                case RequestMethod.PUT:
                    httpRequest.Method = HttpMethod.Put;
                    break;
                case RequestMethod.DELETE:
                    httpRequest.Method = HttpMethod.Delete;
                    break;
                case RequestMethod.POST:
                    httpRequest.Method = HttpMethod.Post;
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

