using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxSearchManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task SearchKeyword_LiveSession_ValidResponse()
        {
            // Test adding a new comment
            const string keyword = "test";

            BoxCollection<BoxItem> results = await _client.SearchManager.SearchAsync(keyword, 10);

            Assert.IsNotNull(results);
        }
    }
}
