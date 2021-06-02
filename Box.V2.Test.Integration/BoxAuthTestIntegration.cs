using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using Box.V2.Auth;
using System.Diagnostics;
using Box.V2.Config;
using Box.V2.JWTAuth;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxAuthTestIntegration : BoxResourceManagerTestIntegration
    {
        public const string ClientId = "YOUR_CLIENT_ID";
        public const string ClientSecret = "YOUR_CLIENT_SECRET";
        public const string EnterpriseId = "YOUR_ENTERPRISE_ID";
        public const string publicKeyID = "YOUR_PUBLIC_KEY_ID";
        public const string privateKey = "-----BEGIN ENCRYPTED PRIVATE KEY-----\nYOUR_PRIVATE_KEY\n-----END ENCRYPTED PRIVATE KEY-----\n";
        public const string passphrase = "YOUR_PASSPHRASE";

        [TestMethod]
        public void retriesWithNewJWTAssertionOnErrorResponseAndSucceeds()
        {
            var config = new BoxConfig(ClientId, ClientSecret, EnterpriseId, privateKey, passphrase, publicKeyID);
            var session = new BoxJWTAuth(config);
            var adminToken = session.AdminToken();
            adminClient = session.AdminClient(adminToken);
        }
    }
}
