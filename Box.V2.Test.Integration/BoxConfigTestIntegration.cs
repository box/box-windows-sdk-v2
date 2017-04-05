using Box.V2.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxConfigTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task BoxConfig_CreateFromString()
        {
            const string jsonString = 
@"{
  'boxAppSettings': {
    'clientID': 'cid-123',
    'clientSecret': 'cre-123',
    'appAuth': {
      'keyID': 'kid-123',
      'privateKey': 'DUMMY',
      'passphrase': 'password'
    },
    'developerEnterprise': {
      'enterpriseID': 'eid-123'
    },
    'webhooks': {}
  }
}";
            var config = BoxConfig.CreateFromJsonString(jsonString);

            Assert.AreEqual(config.ClientId, "cid-123");
        }
    }
}
