using Box.V2.Models;
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
            // Get 3 recents items w/o auto paging
            var recentItems = await _client.RecentItemsManager.GetRecentItemsAsync(limit:3);
            Assert.AreEqual(recentItems.Limit, 3);

            // Get next page if possible
            if (!string.IsNullOrEmpty(recentItems.NextMarker))
            {
                recentItems = await _client.RecentItemsManager.GetRecentItemsAsync(limit: 3, marker: recentItems.NextMarker);
                Assert.AreEqual(recentItems.Limit, 3);
            }

            // Get all the recent items.
            recentItems = await _client.RecentItemsManager.GetRecentItemsAsync(limit:3, autoPaginate:true);
            Assert.AreEqual(recentItems.Order.By, BoxSortBy.interacted_at);
        }
    }
}
