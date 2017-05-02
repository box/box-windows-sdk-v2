using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Auth;
using Box.V2.Request;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.JWTAuth;
using System.Threading.Tasks;
using Box.V2.Models;
using System.Diagnostics;

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
            var jsonConfig = Environment.GetEnvironmentVariable("JSON_CONFIG");
            _handler = new HttpRequestHandler();
            _parser = new BoxJsonConverter();

            Debug.WriteLine(jsonConfig.Length);

            if (string.IsNullOrEmpty(jsonConfig))
            {
                _auth = new OAuthSession("YOUR_ACCESS_TOKEN", "YOUR_REFRESH_TOKEN", 3600, "bearer");

                _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
                _client = new BoxClient(_config, _auth);
            }
            else
            {
                _config = BoxConfig.CreateFromJsonString(jsonConfig);
                var session = new BoxJWTAuth(_config);

                // create a new app user
                // client with permissions to manage application users
                var adminToken = session.AdminToken();
                var client = session.AdminClient(adminToken);

                var user = CreateNewUser(client).Result;
                // Console.WriteLine("New app user created with Id = {0}", user.Id);

                // user client with access to user's data (folders, files, etc)
                var userToken = session.UserToken(user.Id);
                _client = session.UserClient(userToken, user.Id);
                _auth = new OAuthSession(userToken, "", 3600, "bearer");
            }
        }

        protected static Task<BoxUser> CreateNewUser(BoxClient client)
        {
            var userRequest = new BoxUserRequest
            {
                Name = "CI User",
                IsPlatformAccessOnly = true // creating application specific user, not a Box.com user
            };
            return client.UsersManager.CreateEnterpriseUserAsync(userRequest);
        }

        protected string GetUniqueName()
        {
            return string.Format("test{0}", Guid.NewGuid().ToString());
        }
    }
}
