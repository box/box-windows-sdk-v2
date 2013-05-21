using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxFoldersManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GetFolder_LiveSession_ValidResponse()
        {
            //Folder f = await _client.FoldersManager.GetItemsAsync("811565831", 10);
            Folder f = await _client.FoldersManager.GetItemsAsync("0", 10);
        }
    }
}
