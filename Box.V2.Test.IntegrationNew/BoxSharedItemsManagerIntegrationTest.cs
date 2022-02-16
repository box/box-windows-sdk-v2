using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Test.IntegrationNew.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.IntegrationNew
{
    [TestClass]
    public class BoxSharedItemsManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task SharedItemsAsync_ForSharedFolder_ShouldReturnSharedFolder()
        {
            var folder = await CreateFolder();
            var password = "secret";
            var sharedLinkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open,
                Password = password
            };
            var folderWithLink = await UserClient.FoldersManager.CreateSharedLinkAsync(folder.Id, sharedLinkReq);

            var sharedItem = await UserClient.SharedItemsManager.SharedItemsAsync(folderWithLink.SharedLink.Url, password);

            Assert.AreEqual(folderWithLink.Id, sharedItem.Id);
        }
    }
}

