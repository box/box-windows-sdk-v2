using System;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.CCGAuth;
using Box.V2.Config;
using Box.V2.Request;
using Box.V2.Services;
using Box.V2.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxCCGAuthTest : BoxResourceManagerTest
    {
        private readonly Mock<IRequestHandler> _handler;
        private readonly IBoxService _service;
        private readonly Mock<IBoxConfig> _boxConfig;
        private readonly BoxCCGAuth _ccgAuth;

        public BoxCCGAuthTest()
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
        }

        [TestMethod]
        public async Task GetAdminToken_ValidSession()
        {
            // Arrange
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<BoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\":\"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\":3600,\"restricted_to\":[],\"token_type\":\"bearer\"}"
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            // Act
            var accessToken = await _ccgAuth.AdminTokenAsync();

            // Assert
            Assert.AreEqual("https://api.box.com/oauth2/token", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.IsTrue(boxRequest.PayloadParameters.ContainsKeyValue("grant_type", "client_credentials"));
            Assert.IsTrue(boxRequest.PayloadParameters.ContainsKeyValue("client_id", _boxConfig.Object.ClientId));
            Assert.IsTrue(boxRequest.PayloadParameters.ContainsKeyValue("client_secret", _boxConfig.Object.ClientSecret));
            Assert.IsTrue(boxRequest.PayloadParameters.ContainsKeyValue("box_subject_type", "enterprise"));
            Assert.IsTrue(boxRequest.PayloadParameters.ContainsKeyValue("box_subject_id", _boxConfig.Object.EnterpriseId));

            Assert.AreEqual(accessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
        }

        [TestMethod]
        public async Task GetUserToken_ValidSession()
        {
            // Arrange
            var userId = "22222";
            IBoxRequest boxRequest = null;
            _handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<BoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\":\"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\":3600,\"restricted_to\":[],\"token_type\":\"bearer\"}"
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            // Act
            var accessToken = await _ccgAuth.UserTokenAsync(userId);
            Assert.AreEqual("https://api.box.com/oauth2/token", boxRequest.AbsoluteUri.AbsoluteUri);
            Assert.AreEqual(RequestMethod.Post, boxRequest.Method);
            Assert.IsTrue(boxRequest.PayloadParameters.ContainsKeyValue("grant_type", "client_credentials"));
            Assert.IsTrue(boxRequest.PayloadParameters.ContainsKeyValue("client_id", _boxConfig.Object.ClientId));
            Assert.IsTrue(boxRequest.PayloadParameters.ContainsKeyValue("client_secret", _boxConfig.Object.ClientSecret));
            Assert.IsTrue(boxRequest.PayloadParameters.ContainsKeyValue("box_subject_type", "user"));
            Assert.IsTrue(boxRequest.PayloadParameters.ContainsKeyValue("box_subject_id", userId));

            // Assert
            Assert.AreEqual(accessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
        }

        [TestMethod]
        public void UserClient_ShouldReturnUserClientWithSession()
        {
            // Act
            var userClient = _ccgAuth.UserClient("T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl", "22222");

            // Assert
            Assert.IsInstanceOfType(userClient, typeof(BoxClient));
            Assert.IsInstanceOfType(userClient.Auth, typeof(CCGAuthRepository));
            Assert.IsNotNull(userClient.Auth.Session);
        }

        [TestMethod]
        public void UserClient_WithoutInitialToken_ShouldReturnUserClient()
        {
            // Act
            var userClient = _ccgAuth.UserClient("22222");

            // Assert
            Assert.IsInstanceOfType(userClient, typeof(BoxClient));
            Assert.IsInstanceOfType(userClient.Auth, typeof(CCGAuthRepository));
        }

        [TestMethod]
        public void AdminClient_ShouldReturnAdminClientWithSession()
        {
            // Act
            var adminClient = _ccgAuth.AdminClient("T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl", "22222", true);

            // Assert
            Assert.IsInstanceOfType(adminClient, typeof(BoxClient));
            Assert.IsInstanceOfType(adminClient.Auth, typeof(CCGAuthRepository));
            Assert.IsNotNull(adminClient.Auth.Session);
        }

        [TestMethod]
        public void AdminClient_WithoutInitialToken_ShouldReturnAdminClient()
        {
            // Act
            var adminClient = _ccgAuth.AdminClient("22222", true);

            // Assert
            Assert.IsInstanceOfType(adminClient, typeof(BoxClient));
            Assert.IsInstanceOfType(adminClient.Auth, typeof(CCGAuthRepository));
        }
    }
}
