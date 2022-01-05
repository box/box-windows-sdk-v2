using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxSharedItemsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task SharedLink_LiveSession()
        {
            const string SharedLink = "https://app.box.com/s/70pecdxd6pvnd285rs4hqdp7zphgyqva";
            const string Password = "demo1234";
            const string ExpectedId = "16894946307";

            var sharedItem = await Client.SharedItemsManager.SharedItemsAsync(SharedLink, sharedLinkPassword: Password);

            Assert.AreEqual(ExpectedId, sharedItem.Id);
        }
    }
}
