using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.JWTAuth;
using Box.V2.Models;
using Box.V2.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public abstract class BoxResourceManagerTestIntegration
    {
        public const string ClientId = "YOUR_CLIENT_ID";
        public const string ClientSecret = "YOUR_CLIENT_SECRET";

        public Uri RedirectUri = new Uri("http://boxsdk");

        protected OAuthSession Auth;
        protected BoxClient Client;
        protected IBoxConfig Config;
        protected IRequestHandler Handler;
        protected IBoxConverter Parser;

        protected static string JsonConfig;
        protected static BoxClient UserClient;
        protected static BoxClient AdminClient;
        protected static string UserId;
        protected static string UserToken;

        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        {
            JsonConfig = Environment.GetEnvironmentVariable("JSON_CONFIG");

            if (string.IsNullOrEmpty(JsonConfig))
            {
                Debug.WriteLine("No json config found!");
            }
            else
            {
                Debug.WriteLine("json config content length : " + JsonConfig.Length);

                var config = BoxConfigBuilder.CreateFromJsonString(JsonConfig)
                    .Build();
                var session = new BoxJWTAuth(config);

                // create a new app user
                // client with permissions to manage application users
                var adminToken = session.AdminTokenAsync().Result;
                AdminClient = session.AdminClient(adminToken);

                var user = CreateNewUser(AdminClient).Result;

                UserId = user.Id;

                Debug.WriteLine("New app user created : " + UserId);

                // user client with access to user's data (folders, files, etc)
                UserToken = session.UserTokenAsync(UserId).Result;
                UserClient = session.UserClient(UserToken, UserId);
            }
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            // Delete the app user if we created one
            if (UserId != null)
            {
                try
                {
                    var task = AdminClient.UsersManager.DeleteEnterpriseUserAsync(UserId, false, true);
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
            return AdminClient == null;
        }

        public BoxResourceManagerTestIntegration()
        {
            Handler = new HttpRequestHandler();
            Parser = new BoxJsonConverter();

            if (UserToken == null)
            {
                // Legacy way of getting the token
                Auth = new OAuthSession("YOUR_ACCESS_TOKEN", "YOUR_REFRESH_TOKEN", 3600, "bearer");

                Config = new BoxConfigBuilder(ClientId, ClientSecret, RedirectUri)
                    .Build();
                Client = new BoxClient(Config, Auth);
            }
            else
            {
                Config = BoxConfigBuilder.CreateFromJsonString(JsonConfig)
                    .Build();

                Client = UserClient;
                Auth = new OAuthSession(UserToken, "", 3600, "bearer");
            }
        }

        protected static Task<BoxUser> CreateNewUser(BoxClient client)
        {
            var userRequest = new BoxUserRequest
            {
                Name = "CI App User " + DateTimeOffset.Now.ToString("dd-MM-yyyy"), // mark with date
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
