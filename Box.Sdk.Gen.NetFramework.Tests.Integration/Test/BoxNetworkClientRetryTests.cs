using Box.Sdk.Gen.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Box.Sdk.Gen.Tests.Integration
{
    [TestClass]
    public class BoxNetworkClientRetryTests
    {
        [TestMethod]
        public async Task NetworkExceptionRetryRegeneratesRequestData()
        {
            await WithServerAsync(
                new ServerPlan { AbortFirstResponse = true },
                async (url, bodies) =>
                {
                    var dataFactory = new CountingDataFactory();
                    await ((INetworkClient)new BoxNetworkClient()).FetchAsync(
                        CreateOptions(
                            url,
                            JsonUtils.JsonToSerializedData("{\"attempt\":\"D1\"}"),
                            dataFactory,
                            new NetworkSession
                            {
                                RetryStrategy = RetryWhen((options, response, attempt) => response.Status == 0 && attempt < 2)
                            })).ConfigureAwait(false);

                    CollectionAssert.AreEqual(new[] { "attempt=D1", "attempt=D2" }, bodies);
                    Assert.AreEqual(1, dataFactory.Calls);
                }).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task FiveHundredRetryRegeneratesRequestData()
        {
            await WithServerAsync(
                new ServerPlan { Statuses = new Queue<int>(new[] { 500, 200 }) },
                async (url, bodies) =>
                {
                    var dataFactory = new CountingDataFactory();
                    await ((INetworkClient)new BoxNetworkClient()).FetchAsync(
                        CreateOptions(
                            url,
                            JsonUtils.JsonToSerializedData("{\"attempt\":\"D1\"}"),
                            dataFactory,
                            new NetworkSession
                            {
                                RetryStrategy = RetryWhen((options, response, attempt) => response.Status == 500)
                            })).ConfigureAwait(false);

                    CollectionAssert.AreEqual(new[] { "attempt=D1", "attempt=D2" }, bodies);
                    Assert.AreEqual(1, dataFactory.Calls);
                }).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task RetryStrategyReceivesRegeneratedRequestData()
        {
            await WithServerAsync(
                new ServerPlan { Statuses = new Queue<int>(new[] { 500, 500, 200 }) },
                async (url, _) =>
                {
                    var retryBodies = new List<string?>();
                    var dataFactory = new CountingDataFactory();
                    await ((INetworkClient)new BoxNetworkClient()).FetchAsync(
                        CreateOptions(
                            url,
                            JsonUtils.JsonToSerializedData("{\"attempt\":\"D1\"}"),
                            dataFactory,
                            new NetworkSession
                            {
                                RetryStrategy = RetryWhen((options, response, attempt) =>
                                {
                                    retryBodies.Add(JsonUtils.GetSdValueByKey(options.Data!, "attempt"));
                                    return response.Status == 500;
                                })
                            })).ConfigureAwait(false);

                    CollectionAssert.AreEqual(new[] { "D1", "D2" }, retryBodies);
                }).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task RetryWithoutDataFactoryResendsSameRequestData()
        {
            await WithServerAsync(
                new ServerPlan { Statuses = new Queue<int>(new[] { 500, 200 }) },
                async (url, bodies) =>
                {
                    await ((INetworkClient)new BoxNetworkClient()).FetchAsync(
                        CreateOptions(
                            url,
                            JsonUtils.JsonToSerializedData("{\"attempt\":\"D1\"}"),
                            networkSession: new NetworkSession
                            {
                                RetryStrategy = RetryWhen((options, response, attempt) => response.Status == 500)
                            })).ConfigureAwait(false);

                    CollectionAssert.AreEqual(new[] { "attempt=D1", "attempt=D1" }, bodies);
                }).ConfigureAwait(false);
        }

        [TestMethod]
        public void WithDataCopiesFetchOptionsExceptData()
        {
            var dataFactory = new CountingDataFactory();
            var networkSession = new NetworkSession();
            var options = CreateOptions(
                "https://example.com",
                JsonUtils.JsonToSerializedData("{\"attempt\":\"D1\"}"),
                dataFactory,
                networkSession,
                new MemoryStream(Encoding.UTF8.GetBytes("file")),
                followRedirects: false);

            var withData = typeof(BoxNetworkClient).GetMethod("WithData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            Assert.IsNotNull(withData);
            var copied = (FetchOptions)withData!.Invoke(null, new object[] { options, JsonUtils.JsonToSerializedData("{\"attempt\":\"D2\"}") })!;

            foreach (var property in typeof(FetchOptions).GetProperties())
            {
                if (property.Name == nameof(FetchOptions.Data))
                {
                    Assert.AreEqual("D2", JsonUtils.GetSdValueByKey(copied.Data!, "attempt"));
                }
                else
                {
                    Assert.AreEqual(property.GetValue(options), property.GetValue(copied), $"FetchOptions.{property.Name}");
                }
            }
        }

        [TestMethod]
        public async Task BoxJWTAuthRegeneratesAssertionOnRetry()
        {
            await WithServerAsync(
                new ServerPlan { Statuses = new Queue<int>(new[] { 500, 200 }), DelaySeconds = 1 },
                async (url, bodies) =>
                {
                    using var rsa = RSA.Create();
                    rsa.KeySize = 2048;
                    var config = new JwtConfig(
                        clientId: "client-id",
                        clientSecret: "client-secret",
                        jwtKeyId: "jwt-key-id",
                        privateKey: "unused",
                        privateKeyPassphrase: "passphrase",
                        privateKeyDecryptor: new FixedPrivateKeyDecryptor(rsa))
                    {
                        EnterpriseId = "enterprise-id"
                    };

                    await new BoxJwtAuth(config).RefreshTokenAsync(
                        new NetworkSession(baseUrls: new BaseUrls(url.TrimEnd('/'), url, url))
                        {
                            NetworkClient = new BoxNetworkClient(),
                            RetryStrategy = RetryWhen((options, response, attempt) => response.Status == 500, delaySeconds: 1)
                        }).ConfigureAwait(false);

                    var firstPayload = DecodePayload(AssertionFrom(bodies[0]));
                    var secondPayload = DecodePayload(AssertionFrom(bodies[1]));
                    Assert.AreNotEqual(firstPayload.Jti, secondPayload.Jti);
                    Assert.IsTrue(secondPayload.Exp > firstPayload.Exp);
                }).ConfigureAwait(false);
        }

        private static FetchOptions CreateOptions(
            string url,
            SerializedData data,
            IRequestDataFactory? dataFactory = null,
            NetworkSession? networkSession = null,
            Stream? fileStream = null,
            bool followRedirects = true)
        {
            return new FetchOptions(url: url, method: "POST", contentType: "application/x-www-form-urlencoded", responseFormat: ResponseFormat.Json)
            {
                Data = data,
                DataFactory = dataFactory,
                FileStream = fileStream,
                NetworkSession = networkSession,
                FollowRedirects = followRedirects
            };
        }

        private static IRetryStrategy RetryWhen(RetryPredicate predicate, double delaySeconds = 0)
        {
            return new TestRetryStrategy(predicate, delaySeconds);
        }

        private static async Task WithServerAsync(ServerPlan plan, Func<string, List<string>, Task> test)
        {
            var listener = new HttpListener();
            var prefix = $"http://127.0.0.1:{GetOpenPort()}/";
            var bodies = new List<string>();
            listener.Prefixes.Add(prefix);
            listener.Start();
            using var cts = new CancellationTokenSource();
            var serverTask = Task.Run(() => ServeAsync(listener, plan, bodies, cts.Token));

            try
            {
                await test(prefix, bodies).ConfigureAwait(false);
            }
            finally
            {
                cts.Cancel();
                listener.Stop();
                listener.Close();
                try
                {
                    await serverTask.ConfigureAwait(false);
                }
                catch (HttpListenerException)
                {
                }
                catch (ObjectDisposedException)
                {
                }
                catch (Exception) when (cts.IsCancellationRequested)
                {
                }
            }
        }

        private static async Task ServeAsync(HttpListener listener, ServerPlan plan, List<string> bodies, CancellationToken cancellationToken)
        {
            var requestCount = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                var context = await listener.GetContextAsync().ConfigureAwait(false);
                requestCount++;
                using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    bodies.Add(await reader.ReadToEndAsync().ConfigureAwait(false));
                }

                if (plan.AbortFirstResponse && requestCount == 1)
                {
                    var partialResponseBytes = Encoding.UTF8.GetBytes("{\"partial\":");
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    context.Response.ContentLength64 = partialResponseBytes.Length + 100;
                    await context.Response.OutputStream.WriteAsync(partialResponseBytes, 0, partialResponseBytes.Length).ConfigureAwait(false);
                    context.Response.Abort();
                    continue;
                }

                var status = plan.Statuses.Count > 0 ? plan.Statuses.Dequeue() : 200;
                var responseBody = status == 200
                    ? "{\"access_token\":\"token\",\"expires_in\":3600,\"token_type\":\"bearer\"}"
                    : "{\"error\":\"retry\"}";
                var responseBytes = Encoding.UTF8.GetBytes(responseBody);
                context.Response.StatusCode = status;
                context.Response.ContentType = "application/json";
                context.Response.Headers["Retry-After"] = plan.DelaySeconds.ToString(System.Globalization.CultureInfo.InvariantCulture);
                context.Response.ContentLength64 = responseBytes.Length;
                await context.Response.OutputStream.WriteAsync(responseBytes, 0, responseBytes.Length).ConfigureAwait(false);
                context.Response.Close();
            }
        }

        private static int GetOpenPort()
        {
            var listener = new System.Net.Sockets.TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private static string AssertionFrom(string body)
        {
            return body.Split('&')
                .Select(part => part.Split(new[] { '=' }, 2))
                .Where(pair => pair.Length == 2 && pair[0] == "assertion")
                .Select(pair => WebUtility.UrlDecode(pair[1]))
                .First();
        }

        private static JwtPayload DecodePayload(string assertion)
        {
            var parts = assertion.Split('.');
            Assert.AreEqual(3, parts.Length);
            var payload = parts[1].Replace('-', '+').Replace('_', '/');
            payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
            return JsonSerializer.Deserialize<JwtPayload>(Encoding.UTF8.GetString(Convert.FromBase64String(payload)))!;
        }

        private delegate bool RetryPredicate(FetchOptions options, FetchResponse response, int attemptNumber);

        private class TestRetryStrategy : IRetryStrategy
        {
            private readonly RetryPredicate _predicate;
            private readonly double _delaySeconds;

            public TestRetryStrategy(RetryPredicate predicate, double delaySeconds)
            {
                _predicate = predicate;
                _delaySeconds = delaySeconds;
            }

            public Task<bool> ShouldRetryAsync(FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber)
            {
                return Task.FromResult(_predicate(fetchOptions, fetchResponse, attemptNumber));
            }

            public double RetryAfter(FetchOptions fetchOptions, FetchResponse fetchResponse, int attemptNumber)
            {
                return _delaySeconds;
            }
        }

        private class CountingDataFactory : IRequestDataFactory
        {
            public int Calls { get; private set; }

            public SerializedData Create()
            {
                Calls++;
                return JsonUtils.JsonToSerializedData($"{{\"attempt\":\"D{Calls + 1}\"}}");
            }
        }

        private class FixedPrivateKeyDecryptor : IPrivateKeyDecryptor
        {
            private readonly RSA _rsa;

            public FixedPrivateKeyDecryptor(RSA rsa)
            {
                _rsa = rsa;
            }

            public RSA DecryptPrivateKey(string encryptedPrivateKey, string passphrase)
            {
                return _rsa;
            }
        }

        private class ServerPlan
        {
            public bool AbortFirstResponse { get; set; }
            public Queue<int> Statuses { get; set; } = new Queue<int>();
            public double DelaySeconds { get; set; }
        }

        private class JwtPayload
        {
            [System.Text.Json.Serialization.JsonPropertyName("jti")]
            public string Jti { get; set; } = string.Empty;
            [System.Text.Json.Serialization.JsonPropertyName("exp")]
            public long Exp { get; set; }
        }
    }
}
