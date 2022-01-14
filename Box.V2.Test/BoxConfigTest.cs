using Box.V2.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxConfigTest : BoxResourceManagerTest
    {
        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public void BoxConfig_SetUriString()
        {
            const string JsonString =
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
            var config = BoxConfigBuilder
                .CreateFromJsonString(JsonString)
                .Build();
            Assert.AreEqual(config.BoxApiUri, new System.Uri(Constants.BoxApiUriString));

            var exampleUri = new System.Uri("https://example.com/");
            config = BoxConfigBuilder.CreateFromJsonString(JsonString)
                .SetBoxApiUri(exampleUri)
                .Build();
            Assert.AreEqual(config.BoxApiUri, exampleUri);
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public void BoxConfig_CreateFromString()
        {
            const string JsonString =
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
            var config = BoxConfigBuilder.CreateFromJsonString(JsonString)
                .Build();

            Assert.AreEqual(config.ClientId, "cid-123");
            Assert.AreEqual(config.ClientSecret, "cre-123");
            Assert.AreEqual(config.JWTPublicKeyId, "kid-123");
            Assert.AreEqual(config.JWTPrivateKey, "DUMMY");
            Assert.AreEqual(config.EnterpriseId, "eid-123");
        }

        [TestMethod]
        [TestCategory("CI-UNIT-TEST")]
        public void BoxConfig_SetTokenUriString()
        {
            var boxConfig = new BoxConfigBuilder("", "", "", "", "", "")
                .Build();
            Assert.AreEqual(boxConfig.BoxTokenApiUri, new System.Uri(Constants.BoxTokenUriString));

            var exampleUri = new System.Uri("https://example.com/");
            var newConfig = new BoxConfigBuilder("", "", "", "", "", "")
                .SetBoxTokenApiUri(exampleUri)
                .Build();
            Assert.AreEqual(newConfig.BoxTokenApiUri, exampleUri);
        }
    }
}
