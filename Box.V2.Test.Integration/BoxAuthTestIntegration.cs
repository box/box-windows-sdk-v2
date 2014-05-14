using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using Box.V2.Auth;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxAuthTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task RefreshTokens_LiveSession_ValidResponse()
        {
            OAuthSession auth = await _client.Auth.RefreshAccessTokenAsync(_auth.AccessToken);
            var accesstoken = auth.AccessToken;
            var refreshToken = auth.RefreshToken;
        }

    }
}
