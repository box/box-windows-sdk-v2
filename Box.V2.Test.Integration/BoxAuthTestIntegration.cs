using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxAuthTestIntegration : BoxResourceManagerTestIntegration
    {
        public new const string ClientId = "YOUR_CLIENT_ID";
        public new const string ClientSecret = "YOUR_CLIENT_SECRET";
        public const string EnterpriseId = "YOUR_ENTERPRISE_ID";
        public const string PublicKeyID = "YOUR_PUBLIC_KEY_ID";
        public const string PrivateKey = "-----BEGIN ENCRYPTED PRIVATE KEY-----\nYOUR_PRIVATE_KEY\n-----END ENCRYPTED PRIVATE KEY-----\n";
        public const string Passphrase = "YOUR_PASSPHRASE";

        [TestMethod]
        public async Task RetriesWithNewJWTAssertionOnErrorResponseAndSucceeds()
        {
            var config = new BoxConfigBuilder(ClientId, ClientSecret, EnterpriseId, PrivateKey, Passphrase, PublicKeyID)
                .Build();
            var session = new BoxJWTAuth(config);
            var adminToken = await session.AdminTokenAsync();
            AdminClient = session.AdminClient(adminToken);
        }
    }
}
