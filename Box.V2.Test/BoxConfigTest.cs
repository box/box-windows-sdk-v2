using System;
using Box.V2.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxConfigTest : BoxResourceManagerTest
    {
        [TestMethod]
        public void BoxConfig_SetUriString()
        {
            const string JsonString =
            @"{
              'boxAppSettings': {
                'clientID': 'cid-123',
                'clientSecret': 'cre-123',
                'appAuth': {
                  'publicKeyID': 'kid-123',
                  'privateKey': 'testKey',
                  'passphrase': 'password'
                },
              },
              'webhooks': {},
              'enterpriseID': 'eid-123'
            }";
            var config = BoxConfigBuilder
                .CreateFromJsonString(JsonString)
                .Build();
            Assert.AreEqual(config.BoxApiUri, new Uri(Constants.BoxApiUriString));

            var exampleUri = new Uri("https://example.com/");
            config = BoxConfigBuilder.CreateFromJsonString(JsonString)
                .SetBoxApiUri(exampleUri)
                .Build();
            Assert.AreEqual(config.BoxApiUri, exampleUri);
        }

        [TestMethod]
        public void BoxConfig_CreateFromString()
        {
            const string JsonString =
            @"{
              'boxAppSettings': {
                'clientID': 'cid-123',
                'clientSecret': 'cre-123',
                'appAuth': {
                  'publicKeyID': 'kid-123',
                  'privateKey': 'testKey',
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
            Assert.AreEqual(config.JWTPrivateKey, "testKey");
            Assert.AreEqual(config.EnterpriseId, "eid-123");
        }

        [TestMethod]
        public void BoxConfig_SetAuthTokenUriString()
        {
            var boxConfig = new BoxConfigBuilder("", "")
                .Build();
            Assert.AreEqual(boxConfig.BoxAuthTokenApiUri, new Uri(Constants.BoxAuthTokenApiUriString));

            var exampleUri = new Uri("https://example.com/token");
            var newConfig = new BoxConfigBuilder("", "")
                .SetBoxTokenApiUri(exampleUri)
                .Build();
            Assert.AreEqual(newConfig.BoxAuthTokenApiUri, exampleUri);
        }

        [TestMethod]
        public void BoxConfig_SetBoxApiHostUri()
        {
            var exampleUri = new Uri("https://example.com/base");
            var newConfig = new BoxConfigBuilder("", "")
                .SetBoxApiHostUri(exampleUri)
                .Build();

            Assert.AreEqual(newConfig.BoxApiHostUri, exampleUri);
        }

        [TestMethod]
        public void BoxConfig_SetBoxAccountApiHostUri()
        {
            var exampleUri = new Uri("https://example.com/account");
            var newConfig = new BoxConfigBuilder("", "")
                .SetBoxAccountApiHostUri(exampleUri)
                .Build();

            Assert.AreEqual(newConfig.BoxAccountApiHostUri, exampleUri);
            Assert.AreEqual(newConfig.AuthCodeBaseUri, new Uri(exampleUri, "/oauth2/authorize"));
        }
    }
}
