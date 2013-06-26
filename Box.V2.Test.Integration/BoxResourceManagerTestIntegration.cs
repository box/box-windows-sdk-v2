using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Auth;
using Box.V2.Services;
using System.Threading.Tasks;
using Box.V2.Request;
using Box.V2.Config;
using Box.V2.Converter;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public abstract class BoxResourceManagerTestIntegration
    {
        // Keys on Live
        public const string ClientId = "YOUR_CLIENT_ID";
        public const string ClientSecret = "YOUR_CLIENT_SECRET";

        public const string RedirectUri = "http://localhost";


        protected OAuthSession _auth;
        protected BoxClient _client;
        protected IBoxConfig _config;
        protected IRequestHandler _handler;
        protected IBoxConverter _parser;

        public BoxResourceManagerTestIntegration()
        {
            _auth = new OAuthSession("X5OQiaUIc88XAdJTi9C5JSIKyoz", "8w8CyvZuWY7lkYQp8kESj4IOSeaW3HmYRd6zEvZgNDjRltiQ5w", 3600, "bearer");

            _handler = new HttpRequestHandler();
            _parser = new BoxJsonConverter();
            _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            _client = new BoxClient(_config, _auth);
        }

        [TestMethod]
        public async Task RefreshTokens_LiveSession_ValidResponse()
        {
            OAuthSession auth = await _client.Auth.RefreshAccessTokenAsync(_auth.AccessToken);
            var accesstoken = auth.AccessToken;
            var refreshToken = auth.RefreshToken;
        }

        protected string GetUniqueName()
        {
            return string.Format("test{0}", Guid.NewGuid().ToString());
        }


        #region Test Properties

        private string _testFolderId = "0";
        public string TestFolderId
        {
            get { return _testFolderId; }
            set { _testFolderId = value; }
        }

        private string _testFileId = "7869094982";
        public string TestFileId
        {
            get { return _testFileId; }
            set { _testFileId = value; }
        }


        #endregion
    }
}
