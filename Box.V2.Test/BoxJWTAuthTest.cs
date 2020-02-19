using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.JWTAuth;
using Box.V2.Request;
using Box.V2.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxJWTAuthTest : BoxResourceManagerTest
    {
        private readonly IBoxConverter _converter;
        private readonly Mock<IRequestHandler> _handler;
        private readonly IBoxService _service;
        private readonly Mock<IBoxConfig> _boxConfig;
        private readonly BoxJWTAuth _jwtAuth;

        public BoxJWTAuthTest()
        {
            // Initial Setup
            _handler = new Mock<IRequestHandler>();
            _service = new BoxService(_handler.Object);
            _boxConfig = new Mock<IBoxConfig>();
            _boxConfig.SetupGet(x => x.EnterpriseId).Returns("12345");
            _jwtAuth = new BoxJWTAuth(_boxConfig.Object, _service);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public void GetToken_ValidSession()
        {
            // Arrange
            _handler.Setup(h => h.ExecuteAsyncWithoutRetry<OAuthSession>(It.IsAny<BoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\":\"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\":3600,\"restricted_to\":[],\"token_type\":\"bearer\"}"
                })); ;

            // Act
            String accessToken = _jwtAuth.AdminToken();

            // Assert
            Assert.AreEqual(accessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        [ExpectedException(typeof(BoxException))]
        public void GetToken_MaxRetries_Exception()
        {
            // Arrange
            _handler.SetupSequence(h => h.ExecuteAsyncWithoutRetry<OAuthSession>(It.IsAny<BoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.TooManyRequests,
                    StatusCode = HttpRequestHandler.TooManyRequests
                }))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.TooManyRequests,
                    StatusCode = HttpRequestHandler.TooManyRequests
                }))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.TooManyRequests,
                    StatusCode = HttpRequestHandler.TooManyRequests
                }))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.TooManyRequests,
                    StatusCode = HttpRequestHandler.TooManyRequests
                }))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.TooManyRequests,
                    StatusCode = HttpRequestHandler.TooManyRequests
                }))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.TooManyRequests,
                    StatusCode = HttpRequestHandler.TooManyRequests
                }))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\":\"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\":3600,\"restricted_to\":[],\"token_type\":\"bearer\"}"
                }));

            // Act
            String accessToken = _jwtAuth.AdminToken();
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public void GetToken_Retries_ValidSession()
        {
            // Arrange
            _handler.SetupSequence(h => h.ExecuteAsyncWithoutRetry<OAuthSession>(It.IsAny<BoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.TooManyRequests,
                    StatusCode = HttpRequestHandler.TooManyRequests
                }))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.TooManyRequests,
                    StatusCode = HttpRequestHandler.TooManyRequests
                }))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\":\"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\":3600,\"restricted_to\":[],\"token_type\":\"bearer\"}"
                }));

            // Act
            String accessToken = _jwtAuth.AdminToken();

            // Assert
            Assert.AreEqual(accessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
        }
    }
}
