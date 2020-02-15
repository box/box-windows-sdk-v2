using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Box.V2.Services;
using System.Threading.Tasks;
using Box.V2.Auth;
using System.Collections.Generic;
using Box.V2.Exceptions;
using System.Linq;
using Box.V2.Request;
using Box.V2.Config;

namespace Box.V2.Test
{
    [TestClass]
    public class AuthRepositoryTest : BoxResourceManagerTest
    {

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        [ExpectedException(typeof(BoxException))]
        public async Task AuthenticateLive_InvalidAuthCode_Exception()
        {
            // Arrange
            IRequestHandler handler = new HttpRequestHandler();
            IBoxService service = new BoxService(handler);
            IBoxConfig config = new BoxConfig(null, null, null);

            IAuthRepository authRepository = new AuthRepository(config, service, Converter);

            // Act
            OAuthSession response = await authRepository.AuthenticateAsync("fakeAuthorizationCode");
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task Authenticate_ValidResponse_ValidSession()
        {
            // Arrange
            Handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IBoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                }));

            // Act
            OAuthSession session = await AuthRepository.AuthenticateAsync("sampleauthorizationcode");

            // Assert
            Assert.AreEqual(session.AccessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
            Assert.AreEqual(session.ExpiresIn, 3600);
            Assert.AreEqual(session.RefreshToken, "J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR");
            Assert.AreEqual(session.TokenType, "bearer");
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        [ExpectedException(typeof(BoxException))]
        public async Task Authenticate_ErrorResponse_Exception()
        {
            // Arrange
            Handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IBoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Error,
                    ContentString = "{\"error\": \"invalid_grant\",\"error_description\": \"Invalid user credentials\"}"
                }));

            // Act
            OAuthSession session = await AuthRepository.AuthenticateAsync("fakeauthorizationcode");
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task RefreshSession_ValidResponse_ValidSession()
        {
            // Arrange
            Handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IBoxRequest>()))
                .Returns(Task<IBoxResponse<OAuthSession>>.Factory.StartNew(() => new BoxResponse<OAuthSession>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \"T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                }));

            // Act
            OAuthSession session = await AuthRepository.RefreshAccessTokenAsync("fakeaccesstoken");

            // Assert
            Assert.AreEqual(session.AccessToken, "T9cE5asGnuyYCCqIZFoWjFHvNbvVqHjl");
            Assert.AreEqual(session.ExpiresIn, 3600);
            Assert.AreEqual(session.RefreshToken, "J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR");
            Assert.AreEqual(session.TokenType, "bearer");
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public async Task RefreshSession_MultipleThreadsSameAccessToken_SameSession()
        {

            /*** Arrange ***/
            int numTasks = 1000;

            int count = 0; 

            // Increments the access token each time a call is made to the API
            Handler.Setup(h => h.ExecuteAsync<OAuthSession>(It.IsAny<IBoxRequest>()))
                .Returns(() => Task.FromResult<IBoxResponse<OAuthSession>>(new BoxResponse<OAuthSession>() 
                {
                    Status = ResponseStatus.Success,
                    ContentString = "{\"access_token\": \""+ count + "\",\"expires_in\": 3600,\"token_type\": \"bearer\",\"refresh_token\": \"J7rxTiWOHMoSC1isKZKBZWizoRXjkQzig5C6jFgCVJ9bUnsUfGMinKBDLZWP9BgR\"}"
                })).Callback(() => System.Threading.Interlocked.Increment(ref count));

            /*** Act ***/
            List<Task<OAuthSession>> tasks = new List<Task<OAuthSession>>();

            for (int i = 0; i < numTasks; i++)
                tasks.Add(AuthRepository.RefreshAccessTokenAsync("fakeAccessToken")); // Refresh with the same access token each time

            await Task.WhenAll(tasks);

            /*** Assert ***/
            var exceptions = tasks.Where(t => t.Status == TaskStatus.Faulted).Select(t => t.Exception);
            Assert.AreEqual(exceptions.Count(), 0);
            var completions = tasks.Where(t => t.Status == TaskStatus.RanToCompletion).Select(t => t.Result);
            Assert.AreEqual(completions.Count(), numTasks);
            var results = tasks.Where(t => t.Result.AccessToken == "0").Select(t => t.Result);
            Assert.AreEqual(results.Count(), numTasks);
        }
    }
}
