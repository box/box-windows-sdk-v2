using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Services
{
    public class HttpRequestHandler : IRequestHandler
    {
        private static HttpClient _client;

        public HttpRequestHandler()
        {
            _client = new HttpClient();
        }

        public async Task<IBoxResponse<T>> ExecuteAsync<T>(IBoxRequest request) 
        {
            HttpClientHandler handler = new HttpClientHandler();
            //client.MaxResponseContentBufferSize = 25500;

            HttpRequestMessage httpRequest = BuildRequest(request);
            
            HttpResponseMessage response = await _client.SendAsync(httpRequest);

            BoxResponse<T> boxResponse = new BoxResponse<T>()
            {
                Status = response.IsSuccessStatusCode ?
                    ResponseStatus.Success :
                    ResponseStatus.Error,
            };


            if (typeof(T) == typeof(byte[]))
            {
                var resObj = await response.Content.ReadAsByteArrayAsync();
                boxResponse.ResponseObject = (T)Convert.ChangeType(resObj, typeof(T), null);
            }
            else if (typeof(T) == typeof(MemoryStream))
            {
                var resObj = await response.Content.ReadAsStreamAsync();
                boxResponse.ResponseObject = (T)Convert.ChangeType(resObj, typeof(T), null);
            }
            else
                boxResponse.ContentString = await response.Content.ReadAsStringAsync();

            return boxResponse;
        }

        private HttpRequestMessage BuildRequest(IBoxRequest request)
        {
            HttpRequestMessage httpRequest = null;

            switch (request.Method)
            {
                case RequestMethod.PUT:
                    httpRequest = new HttpRequestMessage(HttpMethod.Put, request.AbsoluteUri);
                    httpRequest.Content = new StringContent(request.GetQueryString());
                    break;
                case RequestMethod.DELETE:
                    httpRequest = new HttpRequestMessage(HttpMethod.Delete, request.AbsoluteUri);
                    httpRequest.Content = new StringContent(request.GetQueryString());
                    break;
                case RequestMethod.POST:
                    httpRequest = new HttpRequestMessage(HttpMethod.Post, request.AbsoluteUri);
                    //httpRequest.Content = new StringContent(request.GetQueryString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                    httpRequest.Content = new FormUrlEncodedContent(request.PayloadParameters);
                    break;
                case RequestMethod.GET:
                    httpRequest = new HttpRequestMessage(HttpMethod.Get, request.AbsoluteUri);
                    break;
                default:
                    throw new InvalidOperationException("Http method not supported");
            }

            foreach (var kvp in request.HttpHeaders)
                httpRequest.Headers.TryAddWithoutValidation(kvp.Key, kvp.Value);


            return httpRequest;
        }
    }
}

