using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Exceptions;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxConfigTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task BoxConfig_SetWrongURIFails()
        {

            var config = new BoxConfig("CLIENET_ID", "CLIENT_SECRET", new System.Uri("http://localhost:3000"));
            var oAuthSession = new OAuthSession("ACCESS_TOKEN", "", 3600, "bearer");
            var client = new BoxClient(config, oAuthSession);
            config.BoxApiUri = new System.Uri("https://example.com/2.0/");

            try
            {
                BoxUser user = await client.UsersManager.GetCurrentUserInformationAsync();
            }
            catch (BoxException e)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, e.StatusCode);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public async Task BoxConfig_SetUriString()
        {
            const string jsonString =
            @"{
              'boxAppSettings': {
                'clientID': 'cid-123',
                'clientSecret': 'cre-123',
                'appAuth': {
                  'publicKeyID': 'kid-123',
                  'privateKey': 'DUMMY',
                  'passphrase': 'password'
                },
              },
              'webhooks': {},
              'enterpriseID': 'eid-123'
            }";
            var config = BoxConfig.CreateFromJsonString(jsonString);
            Assert.AreEqual(config.BoxApiUri, new System.Uri(Constants.BoxApiUriString));

            System.Uri exampleUri = new System.Uri("https://example.com/");
            config.BoxApiUri = exampleUri;
            Assert.AreEqual(config.BoxApiUri, exampleUri);
        }

        [TestMethod]
        public async Task BoxConfig_CreateFromString()
        {
            const string jsonString =
            @"{
              'boxAppSettings': {
                'clientID': 'cid-123',
                'clientSecret': 'cre-123',
                'appAuth': {
                  'publicKeyID': 'kid-123',
                  'privateKey': 'DUMMY',
                  'passphrase': 'password'
                },
              },
              'webhooks': {},
              'enterpriseID': 'eid-123'
            }";
            var config = BoxConfig.CreateFromJsonString(jsonString);

            Assert.AreEqual(config.ClientId, "cid-123");
            Assert.AreEqual(config.ClientSecret, "cre-123");
            Assert.AreEqual(config.JWTPublicKeyId, "kid-123");
            Assert.AreEqual(config.JWTPrivateKey, "DUMMY");
            Assert.AreEqual(config.EnterpriseId, "eid-123");
        }
    }
}
