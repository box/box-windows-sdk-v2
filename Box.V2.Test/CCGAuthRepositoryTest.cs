using System;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.CCGAuth;
using Box.V2.Config;
using Box.V2.Request;
using Box.V2.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace Box.V2.Test
{
    [TestClass]
    public class CCGAuthRepositoryTest : BoxResourceManagerTest
    {
        private readonly CCGAuthRepository _userAuthRepository;
        private readonly CCGAuthRepository _adminAuthRepository;
        private readonly Mock<IBoxConfig> _boxConfig;
        private readonly Mock<IRequestHandler> _handler;
        private readonly IBoxService _service;
        private readonly BoxCCGAuth _ccgAuth;

        private readonly string _userId = "22222";

        public CCGAuthRepositoryTest()
        {
            // Initial Setup
            _handler = new Mock<IRequestHandler>();
            _service = new BoxService(_handler.Object);
            _boxConfig = new Mock<IBoxConfig>();
            _boxConfig.SetupGet(x => x.EnterpriseId).Returns("12345");
            _boxConfig.SetupGet(x => x.ClientId).Returns("123");
            _boxConfig.SetupGet(x => x.ClientSecret).Returns("SECRET");
            _boxConfig.SetupGet(x => x.BoxApiHostUri).Returns(new Uri(Constants.BoxApiHostUriString));
            _ccgAuth = new BoxCCGAuth(_boxConfig.Object, _service);
            _userAuthRepository = new CCGAuthRepository(null, _ccgAuth, _userId);
            _adminAuthRepository = new CCGAuthRepository(null, _ccgAuth, _userId);
        }

        [TestMethod]
        public async Task RefreshAccessTokenAsync_ForUser_ReturnsUserSession()
        {
            // Arrange
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<BoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\":\"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\":3600,\"restricted_to\":[],\"token_type\":\"bearer\"}"
                }));

            // Act
            var session = await _userAuthRepository.RefreshAccessTokenAsync(null);

            // Assert
            Assert.AreEqual(session.AccessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
            Assert.AreEqual(session.TokenType, "bearer");
            Assert.AreEqual(session.RefreshToken, null);
            Assert.AreEqual(session.ExpiresIn, 3600);
        }

        [TestMethod]
        public async Task RefreshAccessTokenAsync_ForAdmin_ReturnsAdminSession()
        {
            // Arrange
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<BoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\":\"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\":3600,\"restricted_to\":[],\"token_type\":\"bearer\"}"
                }));

            // Act
            var session = await _adminAuthRepository.RefreshAccessTokenAsync(null);

            // Assert
            Assert.AreEqual(session.AccessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
            Assert.AreEqual(session.TokenType, "bearer");
            Assert.AreEqual(session.RefreshToken, null);
            Assert.AreEqual(session.ExpiresIn, 3600);
        }

        [TestMethod]
        public void LogoutAsync_ThrowsException()
        {
            // Act & Assert
            Assert.ThrowsExceptionAsync<NotImplementedException>(() => _adminAuthRepository.LogoutAsync());
        }

        [TestMethod]
        public void AuthenticateAsync_ThrowsException()
        {
            // Act & Assert
            Assert.ThrowsExceptionAsync<NotImplementedException>(() => _adminAuthRepository.AuthenticateAsync(null));
        }
    }
}
