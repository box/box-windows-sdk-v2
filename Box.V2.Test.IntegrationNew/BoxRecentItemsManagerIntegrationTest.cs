using System.Threading.Tasks;
using Box.V2.Test.IntegrationNew.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.IntegrationNew
{
    [TestClass]
    public class BoxRecentItemsManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task GetRecentItemsAsync_ForExistingItems_ShouldReturnItems()
        {
            var recentItems = await UserClient.RecentItemsManager.GetRecentItemsAsync(10);

            Assert.AreNotEqual(recentItems.Entries, 0);
        }
    }
}
