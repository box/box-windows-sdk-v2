using Box.Sdk.Gen.Schemas;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Box.Sdk.Gen.Internal
{
    class BoxNetworkClient : INetworkClient
    {
        static IHttpClientFactory _clientFactory;

        private static ProxyClient? _proxyClient;

        static BoxNetworkClient()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddHttpClient("DefaultHttpClient")
                .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                };
            });

            var httpClientFactory = serviceCollection.BuildServiceProvider().GetService<IHttpClientFactory>();
            if (httpClientFactory == null)
            {
                throw new BoxSdkException("Unable to create HttpClient. Cannot get an IHttpClientFactory instance from a ServiceProvider.");
            }
            _clientFactory = httpClientFactory;
        }

        /// <summary>
        /// Executes http/s request.
        /// </summary>
        /// <param name="options">Request options of a request.</param>
        /// <returns>A http/s Response as a FetchResponse.</returns>
        async Task<FetchResponse> INetworkClient.FetchAsync(FetchOptions options)
        {
            var networkSession = options.NetworkSession ?? new NetworkSession();

            var client = GetOrCreateHttpClient(networkSession);

            var attempt = 1;
            var cancellationToken = options.CancellationToken ?? default(System.Threading.CancellationToken);

            bool isStreamResponse = options.ResponseFormat == ResponseFormat.Binary;

            Stream? seekableStream = null;
            if (options.FileStream != null && options.ContentType == ContentTypes.OctetStream)
            {
                // buffer the stream in memory in case we cannot rewind it
                seekableStream = options.FileStream.CanSeek ? options.FileStream : await ToMemoryStreamAsync(options.FileStream).ConfigureAwait(false);
            }

            while (true)
            {
                var request = await BuildHttpRequest(options, seekableStream).ConfigureAwait(false);
                var result = await ExecuteRequest(client, request, isStreamResponse, cancellationToken).ConfigureAwait(false);

                if (result.IsSuccess)
                {
                    var response = result.Value!;

                    var url = response.RequestMessage?.RequestUri?.ToString() ?? request.RequestUri?.ToString() ?? options.Url;
                    var statusCode = (int)response.StatusCode;
                    var isRetryAfterPresent = response.Headers.Contains("retry-after");
                    var isRetryAfterWithAcceptedPresent = isRetryAfterPresent && statusCode == 202;

                    if (response.IsSuccessStatusCode && (!isRetryAfterWithAcceptedPresent || attempt >= networkSession.RetryAttempts))
                    {
                        seekableStream?.Dispose();
                        return await ReadResponse(isStreamResponse, response, statusCode, url, cancellationToken).ConfigureAwait(false);
                    }

                    if (attempt >= networkSession.RetryAttempts)
                    {
                        seekableStream?.Dispose();
                        throw new BoxSdkException($"Max retry attempts excedeed.", DateTimeOffset.UtcNow);
                    }

                    if (statusCode >= 300 & statusCode < 400)
                    {
                        if (options.FollowRedirects == false)
                        {
                            seekableStream?.Dispose();
                            return await ReadResponse(isStreamResponse, response, statusCode, url, cancellationToken).ConfigureAwait(false);
                        }

                        var locationHeader = response.Headers.FirstOrDefault(
                            h => string.Equals(h.Key, "location", StringComparison.OrdinalIgnoreCase));

                        if (locationHeader.Key == null)
                        {
                            throw new BoxSdkException($"Redirect response missing Location header for: {options.Url}");
                        }

                        var originUri = new Uri(url);
                        var redirectUri = new Uri(locationHeader.Value.First());
                        var sameOrigin = originUri.Host == redirectUri.Host && originUri.Port == redirectUri.Port && originUri.Scheme == redirectUri.Scheme;
                        return await ((INetworkClient)this).FetchAsync(new FetchOptions(locationHeader.Value.First(), "GET", options.ContentType, options.ResponseFormat)
                        { Auth = sameOrigin ? options.Auth : null, NetworkSession = networkSession }).ConfigureAwait(false);
                    }

                    if (statusCode == 401)
                    {
                        if (options.Auth != null)
                        {
                            await options.Auth.RefreshTokenAsync(networkSession).ConfigureAwait(false);
                        }
                    }
                    else if (statusCode == 429 || statusCode >= 500 || isRetryAfterWithAcceptedPresent)
                    {
                        var retryTimeout = isRetryAfterPresent ?
                          int.Parse(response.Headers.GetValues("retry-after").First()) :
                          networkSession.RetryStrategy.GetRetryTimeout(attempt);

                        await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(retryTimeout)).ConfigureAwait(false);
                    }
                    else if (statusCode == 407)
                    {
                        throw new BoxSdkException($"Proxy authorization required. Check provided credentials in proxy configuration.", DateTimeOffset.UtcNow);
                    }
                    else
                    {
                        seekableStream?.Dispose();
                        throw await BuildApiException(request, response, options, statusCode, cancellationToken).ConfigureAwait(false);
                    }

                    response?.Dispose();
                }
                else
                {
                    if (!result.IsRetryable)
                    {
                        seekableStream?.Dispose();
                        throw new BoxSdkException($"Request was not retried. Inner exception: {result.Exception?.ToString()}", DateTimeOffset.UtcNow);
                    }
                    else if (attempt >= networkSession.RetryAttempts)
                    {
                        seekableStream?.Dispose();
                        throw new BoxSdkException($"Network error. Max retry attempts excedeed. {result.Exception?.ToString()}", DateTimeOffset.UtcNow);
                    }

                    var retryTimeout = networkSession.RetryStrategy.GetRetryTimeout(attempt);
                    await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(retryTimeout)).ConfigureAwait(false);

                    // reauth in case of terminated connection by the peer
                    if (options.Auth != null)
                    {
                        await options.Auth.RefreshTokenAsync(networkSession).ConfigureAwait(false);
                    }
                }

                attempt++;
            }

        }

        private static HttpClient GetOrCreateHttpClient(NetworkSession networkSession)
        {
            if (networkSession.proxyConfig == null)
            {
                return _clientFactory.CreateClient("DefaultHttpClient");
            }
            else
            {
                //To handle proxy clients with different configurations better, we could use ConcurrentDictionary instead
                var proxyKey = GenerateProxyKey(networkSession.proxyConfig);
                if (_proxyClient == null || _proxyClient.ProxyKey != proxyKey)
                {
                    var newClient = new ProxyClient(proxyKey, networkSession.proxyConfig);
                    _proxyClient = newClient;
                    return newClient.HttpClient;
                }
                return _proxyClient.HttpClient!;
            }
        }

        private static HttpClient CreateProxyClient(ProxyConfig proxyConfig)
        {
            var webProxy = new WebProxy(proxyConfig.Url)
            {
                UseDefaultCredentials = proxyConfig.UseDefaultCredentials
            };

            if (!proxyConfig.UseDefaultCredentials)
            {
                webProxy.Credentials = new NetworkCredential(
                    proxyConfig.Username,
                    proxyConfig.Password,
                    proxyConfig.Domain);
            }

            var handler = new HttpClientHandler
            {
                Proxy = webProxy,
                UseProxy = true,
                PreAuthenticate = true,
                AllowAutoRedirect = false
            };

            return new HttpClient(handler, disposeHandler: true);
        }

        private static HttpMethod MapHttpMethod(string method)
        {
            if (!httpMethodsMap.TryGetValue(method.ToUpper(), out var tempMethod))
            {
                throw new ArgumentException($"Provided HTTP method '{method}' is not supported.", nameof(method));
            }

            return tempMethod;
        }

        private static string GenerateProxyKey(ProxyConfig proxyConfig)
        {
            return $"{proxyConfig.Url}_{proxyConfig.Domain}_{proxyConfig.Username}_{proxyConfig.Password}";
        }

        private static async Task<Exception> BuildApiException(HttpRequestMessage request, HttpResponseMessage? response, FetchOptions options,
            int statusCode, System.Threading.CancellationToken cancellationToken)
        {
            string responseContent;
            Dictionary<string, string> responseHeaders;

            if (response != null)
            {
                responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                responseHeaders = response.Headers.Where(x => x.Value.Any()).ToDictionary(x => x.Key, x => x.Value.First());
            }
            else
            {
                responseContent = "empty";
                responseHeaders = new Dictionary<string, string>();
            }

            response?.Dispose();

            var requestHeaders = request.Headers
                .Where(x => x.Value.Any())
                .ToDictionary(x => x.Key, x => x.Value.First());

            var requestInfo = new RequestInfo(request.Method.ToString(), request.RequestUri?.ToString(), options.Parameters, requestHeaders);

            var responseAsSerializedData = JsonUtils.JsonToSerializedData(responseContent);
            var errorDetails = SimpleJsonSerializer.DeserializeWithoutRawJson<BoxApiExceptionDetails>(responseAsSerializedData);

            var responseInfo = new ResponseInfo(statusCode, responseHeaders, responseAsSerializedData, responseContent, errorDetails.Code, errorDetails.ContextInfo,
                errorDetails.RequestId, errorDetails.HelpUrl);

            var dataSanitizer = options.NetworkSession?.DataSanitizer ?? new DataSanitizer();

            return new BoxApiException(responseContent, DateTimeOffset.UtcNow, requestInfo, responseInfo) {DataSanitizer = dataSanitizer};
        }

        private static async Task<HttpRequestMessage> BuildHttpRequest(FetchOptions options, Stream? stream)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = MapHttpMethod(options.Method),
                RequestUri = HttpUtils.BuildUri(options.Url, options.Parameters),
                Content = BuildHttpContent(options, stream)
            };

            if (options.NetworkSession != null)
            {
                foreach (var header in options.NetworkSession.AdditionalHeaders)
                {
                    httpRequest.Headers.Add(header.Key, header.Value);
                }
            }

            if (options.Headers != null)
            {
                foreach (var header in options.Headers)
                {
                    //TODO make it more generic
                    if (header.Key == "content-range")
                    {
                        var headValue = header.Value;
                        httpRequest.Content!.Headers.Add(header.Key, headValue);
                    }
                    else
                    {
                        httpRequest.Headers.Add(header.Key, header.Value);
                    }
                }
            }

            if (options.ContentType != null && httpRequest.Content is StringContent)
            {
                httpRequest.Content!.Headers.ContentType = new MediaTypeHeaderValue(options.ContentType);
            }

            foreach (var keyValuePair in SdkConsts.AnalyticsHeaders)
            {
                httpRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
            }

            if (options.Auth != null)
            {
                var authHeaderValue = await options.Auth.RetrieveAuthorizationHeaderAsync(options.NetworkSession).ConfigureAwait(false);
                httpRequest.Headers.Add("Authorization", authHeaderValue);
            }

            return httpRequest;
        }

        private static async Task<Result<HttpResponseMessage>> ExecuteRequest(HttpClient client, HttpRequestMessage httpRequestMessage, bool isStreamResponse,
           CancellationToken cancellationToken)
        {
            try
            {
                HttpCompletionOption completionOption = isStreamResponse ?
                    HttpCompletionOption.ResponseHeadersRead :
                    HttpCompletionOption.ResponseContentRead;

                var response = await client.SendAsync(httpRequestMessage, completionOption, cancellationToken).ConfigureAwait(false);
                return Result<HttpResponseMessage>.Ok(response);
            }
            catch (TaskCanceledException ex)
            {
                return Result<HttpResponseMessage>.Fail(ex, isRetryable: false);
            }
            catch (HttpRequestException ex)
            {
                string pattern = @"status code\s*'(\d+)'";
                Match match = Regex.Match(ex.Message, pattern);
                if (match.Success)
                {
                    string statusCode = match.Groups[1].Value;

                    if (statusCode == "407")
                    {
                        return Result<HttpResponseMessage>.Fail(ex, false);
                    }
                }
                return Result<HttpResponseMessage>.Fail(ex);
            }
            catch (Exception ex)
            {
                return Result<HttpResponseMessage>.Fail(ex);
            }
        }

        private static HttpContent? BuildHttpContent(FetchOptions options, Stream? stream)
        {
            HttpContent? httpContent;

            if (options.ContentType == ContentTypes.MultipartFormData)
            {
                var multipartContent = new MultipartFormDataContent();

                if (options.MultipartData == null)
                {
                    throw new BoxSdkException("Could not upload file. MultipartData on FetchOptions is null");
                }

                foreach (var part in options.MultipartData)
                {
                    HttpContent partContent = part.FileStream != null ?
                        new ReusableContent(part.FileStream) :
                        part.Data != null ?
                            new StringContent(JsonUtils.SdToJson(part.Data)) :
                            throw new BoxSdkException($"HttpContent for MultipartData {part} not found");

                    // for avatar upload
                    if (part.ContentType != null && part.FileName != null)
                    {
                        partContent.Headers.ContentType = new MediaTypeHeaderValue(part.ContentType);
                        partContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "pic",
                            FileName = part.FileName
                        };
                    }

                    if (part.FileStream != null)
                    {
                        //TODO workaround for empty part.FileName
                        multipartContent.Add(partContent, part.PartName, "file");
                    }
                    else
                    {
                        multipartContent.Add(partContent, part.PartName);
                    }
                }

                httpContent = multipartContent;
            }
            else if (options.ContentType == ContentTypes.FormUrlEncoded && options.Data != null)
            {
                var deserialized = SimpleJsonSerializer.DeserializeWithoutRawJson<Dictionary<string, string>>(options.Data);
                httpContent = new FormUrlEncodedContent(deserialized);
            }
            else if (options.ContentType == ContentTypes.OctetStream && stream != null)
            {
                httpContent = new ReusableContent(stream);
            }
            else
            {
                httpContent = options.Data != null ? new StringContent(JsonUtils.SdToJson(options.Data)) : default;
            }

            return httpContent;
        }

        private static async Task<MemoryStream> ToMemoryStreamAsync(Stream inputStream)
        {
            var memoryStream = new MemoryStream();
            await inputStream.CopyToAsync(memoryStream).ConfigureAwait(false);
            memoryStream.Position = 0;
            return memoryStream;
        }

        private class ProxyClient
        {
            public string ProxyKey { get; }
            public HttpClient HttpClient { get; }

            public ProxyClient(string proxyKey, ProxyConfig proxyConfig)
            {
                ProxyKey = proxyKey;
                HttpClient = CreateProxyClient(proxyConfig);
            }
        }

        private static Dictionary<string, HttpMethod> httpMethodsMap = new Dictionary<string, HttpMethod>() {
            { "GET", HttpMethod.Get },
            { "POST", HttpMethod.Post },
            { "PUT", HttpMethod.Put },
            { "PATCH", HttpMethod.Patch },
            { "DELETE", HttpMethod.Delete },
            { "OPTIONS", HttpMethod.Options },
            { "HEAD", HttpMethod.Head },
            { "TRACE", HttpMethod.Trace },
        };

        private static async Task<FetchResponse> ReadResponse(bool isStreamResponse, HttpResponseMessage response, int statusCode, string url, CancellationToken cancellationToken)
        {
            return isStreamResponse ?
                new FetchResponse(status: statusCode, headers: response.Headers.ToDictionary(x => x.Key, x => x.Value.First())) { Url = url, Content = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false) } :
                new FetchResponse(status: statusCode, headers: response.Headers.ToDictionary(x => x.Key, x => x.Value.First())) { Url = url, Data = JsonUtils.JsonToSerializedData(await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false)) };
        }
    }
}
