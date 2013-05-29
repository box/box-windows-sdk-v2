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
            Folder f = await _client.FoldersManager.GetItemsAsync("0", 50);
        }

        [TestMethod]
        public async Task FolderWorkflow_LiveSession_ValidResponse()
        {
            string testName = GetUniqueName();

            // Test Create Folder
            BoxFolderRequest folderReq = new BoxFolderRequest() {
                Name = testName,
                Parent = new BoxRequestEntity() { Id = "0" }
            };

            Folder f = await _client.FoldersManager.CreateAsync(folderReq);

            Assert.AreEqual(testName, f.Name);

            // Test Get Information
            Folder fi = await _client.FoldersManager.GetInformationAsync(f.Id);

            Assert.AreEqual(f.Id, fi.Id);
            Assert.AreEqual(testName, fi.Name);

            // Test Create Shared Link
            BoxSharedLinkRequest sharedLinkReq = new BoxSharedLinkRequest() {
                Access = BoxSharedLinkAccessType.open
            };

            Folder fsl = await _client.FoldersManager.CreateSharedLinkAsync(f.Id, sharedLinkReq);

            Assert.AreEqual(BoxSharedLinkAccessType.open, fsl.SharedLink.Access);

            // Test Update Folder Information
            string newTestname = GetUniqueName();
            BoxFolderRequest updateReq = new BoxFolderRequest()
            {
                Id = f.Id,
                Name = newTestname,
                SyncState = BoxSyncStateType.not_synced
            };

            Folder uf = await _client.FoldersManager.UpdateInformationAsync(updateReq);

            Assert.AreEqual(newTestname, uf.Name);

            // Test Copy Folder
            string copyTestName = GetUniqueName();
            BoxFolderRequest copyReq = new BoxFolderRequest()
            {
                Id = f.Id,
                Parent = new BoxRequestEntity() { Id = "0" },
                Name = copyTestName
            };

            Folder f2 = await _client.FoldersManager.CopyAsync(copyReq);

            Assert.AreEqual(copyTestName, f2.Name);

            // Test Delete Folder
            await _client.FoldersManager.DeleteAsync(f.Id, true);
            await _client.FoldersManager.DeleteAsync(f2.Id, true);
        }

        private string GetUniqueName()
        {
            return string.Format("test{0}", Guid.NewGuid().ToString());
        }
    }
}
