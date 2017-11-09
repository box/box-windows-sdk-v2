using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Box.V2.Auth;
using Box.V2.Request;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.JWTAuth;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        protected static string jsonConfig;
        protected static BoxClient userClient;
        protected static BoxClient adminClient;
        protected static string userId;
        protected static string userToken;

        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            jsonConfig = Environment.GetEnvironmentVariable("JSON_CONFIG");

            if (string.IsNullOrEmpty(jsonConfig))
            {
                Debug.WriteLine("No json config found!");
            }
            else
            {
                Debug.WriteLine("json config content length : " + jsonConfig.Length);

                var config = BoxConfig.CreateFromJsonString(jsonConfig);
                var session = new BoxJWTAuth(config);

                // create a new app user
                // client with permissions to manage application users
                var adminToken = session.AdminToken();
                adminClient = session.AdminClient(adminToken);

                var user = CreateNewUser(adminClient).Result;

                userId = user.Id;

                Debug.WriteLine("New app user created : " + userId);

                // user client with access to user's data (folders, files, etc)
                userToken = session.UserToken(userId);
                userClient = session.UserClient(userToken, userId);
            }
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            // Delete the app user if we created one
            if (userId != null)
            {
                try
                {
                    var task = adminClient.UsersManager.DeleteEnterpriseUserAsync(userId, false, true);
                    task.Wait();
                }
                catch (Exception exp)
                {
                    // Delete will fail if there are content in the user
                    Debug.Print(exp.StackTrace);
                }
            }
        }

        protected static bool IsUnderCI()
        {
            // if we have adminClient, we are under the CI env.
            return adminClient == null;
        }

        public BoxResourceManagerTestIntegration()
        {
            _handler = new HttpRequestHandler();
            _parser = new BoxJsonConverter();

            if (userToken == null)
            {
                // Legacy way of getting the token
                _auth = new OAuthSession("YOUR_ACCESS_TOKEN", "YOUR_REFRESH_TOKEN", 3600, "bearer");

                _config = new BoxConfig(ClientId, ClientSecret, RedirectUri);
                _client = new BoxClient(_config, _auth);
            }
            else
            {
                _config = BoxConfig.CreateFromJsonString(jsonConfig);

                _client = userClient;
                _auth = new OAuthSession(userToken, "", 3600, "bearer");
            }
        }

        protected static Task<BoxUser> CreateNewUser(BoxClient client)
        {
            var userRequest = new BoxUserRequest
            {
                Name = "CI App User " + DateTime.Now.ToString("dd-MM-yyyy"), // mark with date
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
