using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Box.V2.Services;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Contracts;
using System.Collections.Generic;
using Box.V2.Exceptions;

namespace Box.V2.Test
{
    [TestClass]
    public class AuthRepositoryTest
    {
        IResponseParser _parser;
        Mock<IRequestHandler> _handler;
        IBoxService _service;
        Mock<IBoxConfig> _boxConfig;
        IAuthRepository _authRepository;

        public AuthRepositoryTest()
        {
            // Initial Setup
            _parser = new JsonResponseParser();
            _handler = new Mock<IRequestHandler>();
            _service = new BoxService(_parser, _handler.Object);
            _boxConfig = new Mock<IBoxConfig>();

            OAuthSession session = new OAuthSession() {
                AccessToken = "fakeAccessToken",
                ExpiresIn = 3600,
                RefreshToken = "fakeRefreshToken",
                TokenType = "bearer"
            };

            _authRepository = new AuthRepository(_boxConfig.Object, _service, session);
        }

        [TestMethod]
        [ExpectedException(typeof(BoxException))]
        public async Task AuthenticateLive_InvalidAuthCode_Exception()
        {
            // Arrange
            IRequestHandler handler = new HttpRequestHandler();
            IBoxService service = new BoxService(_parser, handler);
            IBoxConfig config = new BoxConfig(null, null, null);

            IAuthRepository authRepository = new AuthRepository(config, service);

            // Act
            OAuthSession response = await authRepository.Authenticate("fakeAuthorizationCode");
        }

        [TestMethod]
        public async Task Authenticate_ValidResponse_ValidSession()
        {
            // Arrange
            _handler.Setup(h => h.Execute<OAuthSession>(It.IsAny<IBoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                }));

            // Act
            OAuthSession session = await _authRepository.Authenticate("sampleauthorizationcode");

            // Assert
            Assert.AreEqual(session.AccessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
            Assert.AreEqual(session.ExpiresIn, 3600);
            Assert.AreEqual(session.RefreshToken, "J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR");
            Assert.AreEqual(session.TokenType, "bearer");
        }

        [TestMethod]
        public async Task Authenticate_ErrorResponse_Exception()
        {
            // Arrange
            _handler.Setup(h => h.Execute<OAuthSession>(It.IsAny<IBoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                }));

            // Act
            OAuthSession session = await _authRepository.Authenticate("fakeauthorizationcode");

            // Assert
            Assert.AreEqual(session.AccessToken, null);
            Assert.AreEqual(session.ExpiresIn, 0);
            Assert.AreEqual(session.RefreshToken, null);
            Assert.AreEqual(session.TokenType, null);
        }

        [TestMethod]
        public async Task RefreshSession_ValidResponse_ValidSession()
        {
            // Arrange
            _handler.Setup(h => h.Execute<OAuthSession>(It.IsAny<IBoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                }));

            // Act
            OAuthSession session = await _authRepository.RefreshAccessToken("fakeaccesstoken");

            // Assert
            Assert.AreEqual(session.AccessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
            Assert.AreEqual(session.ExpiresIn, 3600);
            Assert.AreEqual(session.RefreshToken, "J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR");
            Assert.AreEqual(session.TokenType, "bearer");
        }


        [TestMethod]
        public async Task RefreshSession_MultipleThreadsSameAccessToken_ValidSession()
        {

            // Arrange
            int numThreads = 1000;
            int accessToken = 0;

            _handler.Setup(h => h.Execute<OAuthSession>(It.IsAny<IBoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"" + ++accessToken + "\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                }));

            List<string> accessTokens = new List<string>();
            for (int i = 0; i < numThreads; i++)
                accessTokens.Add("fakeAccesToken");


            object mutex = new object();
            bool assertAggregate = true;
            int threadCount = 0;

            // Act
            Parallel.ForEach(accessTokens, async (token) =>
            {
                OAuthSession session = await _authRepository.RefreshAccessToken(token);

                lock (mutex)
                {
                    assertAggregate = assertAggregate && (session.AccessToken == "1");
                    System.Threading.Interlocked.Increment(ref threadCount);
                }
            });

            // Assert
            Assert.IsTrue(assertAggregate); // All refresh calls should have validated with an access token of 1
            Assert.AreEqual(numThreads, threadCount); // Ensures all threads completed successfully
        }
    }
}
