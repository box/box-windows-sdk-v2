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
                .SetBoxApiHostUri(exampleUri)
                .Build();
            Assert.AreEqual(config.BoxApiUri, exampleUri + "2.0/");
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
        public void BoxConfig_SetBoxApiHostUri()
        {
            var exampleUri = new Uri("https://example.com/base");
            var newConfig = new BoxConfigBuilder("", "")
                .SetBoxApiHostUri(exampleUri)
                .Build();

            Assert.AreEqual(newConfig.BoxApiHostUri.ToString(), exampleUri + "/");
        }

        [TestMethod]
        public void BoxConfig_SetBoxAccountApiHostUri()
        {
            var exampleUri = new Uri("https://example.com/account");
            var newConfig = new BoxConfigBuilder("", "")
                .SetBoxAccountApiHostUri(exampleUri)
                .Build();

            Assert.AreEqual(newConfig.BoxAccountApiHostUri.ToString(), exampleUri + "/");
            Assert.AreEqual(newConfig.AuthCodeBaseUri.ToString(), exampleUri + "/" + "oauth2/authorize");
        }

        [TestMethod]
        public void BoxConfig_DefaultJWTAudience()
        {
            var exampleUri = new Uri("https://example.com/account");
            var newConfig = new BoxConfigBuilder("", "")
                .SetBoxApiHostUri(exampleUri)
                .Build();

            Assert.AreEqual(newConfig.JWTAudience, "https://api.box.com/oauth2/token");
        }

        [TestMethod]
        public void BoxConfig_SetJWTAudience()
        {
            var exampleUri = new Uri("https://example.com/account");
            var customAudience = "custom_audience/oauth2/token";
            var newConfig = new BoxConfigBuilder("", "")
                .SetBoxApiHostUri(exampleUri)
                .SetJWTAudience(customAudience)
                .Build();

            Assert.AreEqual(newConfig.BoxApiHostUri.ToString(), exampleUri + "/");
            Assert.AreEqual(newConfig.JWTAudience, customAudience);
        }
    }
}
