using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Test.IntegrationNew.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.IntegrationNew
{
    [TestClass]
    public class BoxSearchManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task SearchAsync_ForNonExistentKeyword_ShouldReturnNoResults()
        {
            const string Keyword = "NonExistentKeyWord";

            BoxCollection<BoxItem> results = await UserClient.SearchManager.SearchAsync(Keyword, 200);

            Assert.IsNotNull(results);
            Assert.AreEqual(0, results.Entries.Count);
        }
    }
}
