using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Box.V2.Services;
using Moq;
using Box.V2.Auth;
using System.Collections.Generic;
using Box.V2.Request;
using Box.V2.Converter;
using Box.V2.Config;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxServiceTest
    {
        private readonly IBoxConverter _converter;
        private readonly Mock<IRequestHandler> _handler;
        private readonly IBoxService _service;
        private readonly Mock<IBoxConfig> _boxConfig;
        private readonly IAuthRepository _authRepository;

        public BoxServiceTest()
        {
            // Initial Setup
            _converter = new BoxJsonConverter();
            _handler = new Mock<IRequestHandler>();
            _service = new BoxService(_handler.Object);
            _boxConfig = new Mock<IBoxConfig>();

            OAuthSession session = new OAuthSession("fakeAccessToken", "fakeRefreshToken", 3600, "bearer");

            _authRepository = new AuthRepository(_boxConfig.Object, _service, _converter, session);
        }

        [TestMethod]
        public async Task QueueTask_MultipleThreads_OrderedResponse()
        {
            /*** Arrange ***/
            int numTasks = 1000;

            int count = 0;

            // Increments the access token each time a call is made to the API
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<OAuthSession>>(new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"" + count + "\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                })).Callback(() => System.Threading.Interlocked.Increment(ref count));

            /*** Act ***/
            IBoxRequest request = new BoxRequest(new Uri("http://box.com"), "folders");

            List<Task<IBoxResponse<OAuthSession>>> tasks = new List<Task<IBoxResponse<OAuthSession>>>();
            for (int i = 0; i < numTasks; i++)
                tasks.Add(_service.EnqueueAsync<OAuthSession>(request));

            await Task.WhenAll(tasks);

            /*** Assert ***/
            for (int i = 0; i < numTasks; i++)
            {
                OAuthSession session = _converter.Parse<OAuthSession>(tasks[i].Result.ContentString);
                Assert.AreEqual(session.AccessToken, i.ToString());
            }
        }
    }
}
