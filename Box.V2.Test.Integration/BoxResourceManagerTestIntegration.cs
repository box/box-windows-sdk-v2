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
        public const string ClientId = "YOUR_CLIENT_ID";
        public const string ClientSecret = "YOUR_CLIENT_SECRET";

        public const string EnterpriseId = "YOUR_ENTERPRISE_ID";
        public const string PrivateKey = "YOUR_PRIVATE_KEY";
        public const string PrivateKeyPassword = "YOUR_PRIVATE_KEY_PASSWORD";

        public Uri RedirectUri = new Uri("http://boxsdk");

        protected OAuthSession _auth;
        protected BoxClient _client;
        protected IBoxConfig _config;
        protected IRequestHandler _handler;
        protected IBoxConverter _parser;

        protected BoxClient _boxDeveloperEditionClient;
        protected IBoxConfig _boxDeveloperEditionConfig;

        public BoxResourceManagerTestIntegration()
        {
            _auth = new OAuthSession("YOUR_ACCESS_TOKEN", "YOUR_REFRESH_TOKEN", 3600, "bearer");

            _handler = new HttpRequestHandler();
            _parser = new BoxJsonConverter();
            _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
            _client = new BoxClient(_config, _auth);

            _boxDeveloperEditionConfig = new BoxConfig(EnterpriseId, "enterprise", ClientId, ClientSecret, PrivateKey, PrivateKeyPassword);
            _boxDeveloperEditionClient = new BoxClient(_boxDeveloperEditionConfig);
        }

        protected string GetUniqueName()
        {
            return string.Format("test{0}", Guid.NewGuid().ToString());
        }
    }
}
