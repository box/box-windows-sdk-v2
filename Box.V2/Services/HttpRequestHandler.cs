using System;
using System.Collections.Generic;
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

        public async Task<IBoxResponse<T>> Execute<T>(IBoxRequest request)
        {
            //HttpClientHandler handler = new HttpClientHandler();
            //client.MaxResponseContentBufferSize = 25500;

            HttpRequestMessage httpRequest = null;
            
            switch (request.Method)
            {
                case RequestMethod.PUT:
                    httpRequest = new HttpRequestMessage(HttpMethod.Put, request.Uri);
                    httpRequest.Content = new StringContent(request.GetQueryString());
                    break;
                case RequestMethod.DELETE:
                    httpRequest = new HttpRequestMessage(HttpMethod.Delete, request.Uri);
                    httpRequest.Content = new StringContent(request.GetQueryString());
                    break;
                case RequestMethod.POST:
                    httpRequest = new HttpRequestMessage(HttpMethod.Post, request.Uri);
                    //httpRequest.Content = new StringContent(request.GetQueryString(), Encoding.UTF8, "application/x-www-form-urlencoded");
                    httpRequest.Content = new FormUrlEncodedContent(request.Parameters);
                    break;
                case RequestMethod.GET:
                    httpRequest = new HttpRequestMessage(HttpMethod.Get, request.AbsoluteUri);
                    break;
                default:
                    throw new InvalidOperationException("Http method not supported");
            }
            
            HttpResponseMessage response = await _client.SendAsync(httpRequest);

            BoxResponse<T> boxResponse = new BoxResponse<T>();
            boxResponse.Status = response.IsSuccessStatusCode ?
                ResponseStatus.Success :
                ResponseStatus.Error;

            boxResponse.ContentString = await response.Content.ReadAsStringAsync();
            return boxResponse;
        }
    }
}
