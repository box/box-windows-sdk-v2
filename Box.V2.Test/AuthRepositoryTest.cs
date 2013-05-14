using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Box.V2.Services;
using Box.V2.Web;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Contracts;
using System.Collections.Generic;

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
            _parser = new JsonResponseParser();
            _handler = new Mock<IRequestHandler>();
            _service = new BoxService(_parser, _handler.Object);
            _boxConfig = new Mock<IBoxConfig>();

            _authRepository = new AuthRepository(_boxConfig.Object, _service);
        }

        [TestMethod]
        public async Task Authenticate_ValidResponse_ValidSession()
        {
            // Arrange
            _handler.Setup(h => h.Execute(It.IsAny<IBoxRequest>()))
                .Returns(Task<string>.Factory.StartNew(() => 
                    "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"));

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
            _handler.Setup(h => h.Execute(It.IsAny<IBoxRequest>()))
                .Returns(Task<string>.Factory.StartNew(() =>
                    "{\"error\": \"invalid_grant\",\"error_description\": \"Invalid user credentials\"}"));

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
            _handler.Setup(h => h.Execute(It.IsAny<IBoxRequest>()))
                .Returns(Task<string>.Factory.StartNew(() =>
                    "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"));

            // Act
            OAuthSession session = await _authRepository.RefreshAccessToken("fakeaccesstoken");

            // Assert
            Assert.AreEqual(session.AccessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
            Assert.AreEqual(session.ExpiresIn, 3600);
            Assert.AreEqual(session.RefreshToken, "J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR");
            Assert.AreEqual(session.TokenType, "bearer");
        }


        //[TestMethod]
        //public async Task RefreshSession_MultipleThreadsSameAccessToken_ValidSession()
        //{
        //    int counter = 0;
        //    // Arrange
        //    _handler.Setup(h => h.Execute(It.IsAny<IBoxRequest>()))
        //        .Returns(Task<string>.Factory.StartNew(() =>
        //            "{\"access_token\": \"" + counter++ + "\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"));

        //    List<string> accessTokens = new List<string>();
        //    for (int i = 0; i < 10; i++)
        //        accessTokens.Add("fakeAccesToken");

        //    // Act
        //    Parallel.ForEach(accessTokens, async (token) =>
        //        {
        //            OAuthSession session = await _authRepository.RefreshAccessToken(token);

        //            // Assert
        //            Assert.AreEqual(session.AccessToken, "1");
        //            Assert.AreEqual(session.ExpiresIn, 3600);
        //            Assert.AreEqual(session.RefreshToken, "J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR");
        //            Assert.AreEqual(session.TokenType, "bearer");
        //        });

        //}
    }
}
