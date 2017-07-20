using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxRecentItemsManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task RecentItemsTests_LiveSession()
        {
            // Get all the recent items.
            var recentItems = await _client.RecentItemsManager.GetRecentItemsAsync(limit:3, autoPaginate:true);

            Assert.IsTrue(recentItems.Entries.Count > 0);

            // Verify the auto paging

            // Assert.IsTrue(result, "Failed to delete webhook");
        }
    }
}
