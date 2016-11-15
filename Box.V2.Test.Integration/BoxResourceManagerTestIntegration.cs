using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Auth;
using Box.V2.Request;
using Box.V2.Config;
using Box.V2.Converter;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public abstract class BoxResourceManagerTestIntegration
    {
        public const string ClientId = "YOUR_CLIENT_ID";
        public const string ClientSecret = "YOUR_CLIENT_SECRET";

        public Uri RedirectUri = new Uri("http://boxsdk");

        protected OAuthSession _auth;
        protected BoxClient _client;
        protected IBoxConfig _config;
        protected IRequestHandler _handler;
        protected IBoxConverter _parser;

        public BoxResourceManagerTestIntegration()
        {
            _auth = new OAuthSession("YOUR_ACCESS_TOKEN", "YOUR_REFRESH_TOKEN", 3600, "bearer");

            _handler = new HttpRequestHandler();
            _parser = new BoxJsonConverter();
            _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            _client = new BoxClient(_config, _auth);
        }

        protected string GetUniqueName()
        {
            return string.Format("test{0}", Guid.NewGuid().ToString());
        }
    }
}
