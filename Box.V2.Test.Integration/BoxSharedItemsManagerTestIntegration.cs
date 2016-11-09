using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxSharedItemsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task SharedLink_LiveSession()
        {
            const string sharedLink = "https://app.box.com/s/70pecdxd6pvnd285rs4hqdp7zphgyqva";
            const string password = "demo1234";
            const string expectedId = "16894946307";

            var sharedItem = await _client.SharedItemsManager.SharedItemsAsync(sharedLink, sharedLinkPassword: password);

            Assert.AreEqual(expectedId, sharedItem.Id);
        }
    }
}
