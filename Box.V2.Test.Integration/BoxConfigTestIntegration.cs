using Box.V2.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxConfigTestIntegration : BoxResourceManagerTestIntegration
    {
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
