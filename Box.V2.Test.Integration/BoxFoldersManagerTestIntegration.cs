using System;
using Box.V2.Auth;
using Box.V2.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxFoldersManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task GetFolder_LiveSession_ValidResponse()
        {
            await AssertFolderContents(_client);
        }

        [TestMethod]
        public async Task GetFolder_LiveSession_ValidResponse_GzipCompression()
        {
            var boxConfig = new BoxConfig(ClientId, ClientSecret, RedirectUri){AcceptEncoding = CompressionType.gzip};
            var boxClient = new BoxClient(boxConfig, _auth);
            await AssertFolderContents(boxClient);
        }

        [TestMethod]
        public async Task GetFolder_LiveSession_ValidResponse_DeflateCompression()
        {
            var boxConfig = new BoxConfig(ClientId, ClientSecret, RedirectUri) { AcceptEncoding = CompressionType.deflate };
            var boxClient = new BoxClient(boxConfig, _auth);
            await AssertFolderContents(boxClient);
        }

        private static async Task AssertFolderContents(BoxClient boxClient)
        {
            BoxFolder f = await boxClient.FoldersManager.GetItemsAsync("0", 100, 0, new List<string>()
            {
                BoxFolder.FieldName,
                BoxFolder.FieldSize,
                BoxFolder.FieldModifiedAt,
                BoxFolder.FieldModifiedBy,
                BoxFolder.FieldItemCollection,
                BoxFolder.FieldHasCollaborations,
                BoxFile.FieldCommentCount
            });
            BoxCollection<BoxItem> c = await boxClient.FoldersManager.GetFolderItemsAsync("0", 100, 0, new List<string>()
            {
                BoxItem.FieldName,
                BoxItem.FieldSize,
                BoxItem.FieldModifiedAt,
                BoxItem.FieldModifiedBy,
                BoxFolder.FieldItemCollection
            });

            Assert.AreEqual(f.ItemCollection.TotalCount, c.TotalCount);
            Assert.AreEqual(f.ItemCollection.Entries.Count, c.Entries.Count);
            for (int i = 0; i < f.ItemCollection.TotalCount; i++)
            {
                Assert.AreEqual(f.ItemCollection.Entries[i].Type, c.Entries[i].Type);
                Assert.AreEqual(f.ItemCollection.Entries[i].Id, c.Entries[i].Id);
                Assert.AreEqual(f.ItemCollection.Entries[i].Name, c.Entries[i].Name);
                Assert.AreEqual(f.ItemCollection.Entries[i].Size, c.Entries[i].Size);
                Assert.AreEqual(f.ItemCollection.Entries[i].ModifiedAt, c.Entries[i].ModifiedAt);
                Assert.AreEqual(f.ItemCollection.Entries[i].CreatedAt, c.Entries[i].CreatedAt);
            }
        }

        [TestMethod]
        public async Task FolderGetTrashItems_LiveSession_ValidResponse()
        {
            var results = await _client.FoldersManager.GetTrashItemsAsync(10);
            Assert.IsNotNull(results);
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

            BoxFolder f = await _client.FoldersManager.CreateAsync(folderReq);

            Assert.AreEqual(testName, f.Name);

            // Test Get Information
            BoxFolder fi = await _client.FoldersManager.GetInformationAsync(f.Id);

            Assert.AreEqual(f.Id, fi.Id);
            Assert.AreEqual(testName, fi.Name);

            // Test Create Shared Link
            BoxSharedLinkRequest sharedLinkReq = new BoxSharedLinkRequest() {
                Access = BoxSharedLinkAccessType.open
            };

            BoxFolder fsl = await _client.FoldersManager.CreateSharedLinkAsync(f.Id, sharedLinkReq);

            Assert.AreEqual(BoxSharedLinkAccessType.open, fsl.SharedLink.Access);

            // Test Update Folder Information
            string newTestname = GetUniqueName();
            BoxFolderRequest updateReq = new BoxFolderRequest()
            {
                Id = f.Id,
                Name = newTestname,
                SyncState = BoxSyncStateType.not_synced,
                FolderUploadEmail = new BoxEmailRequest {  Access = "open" }
            };

            BoxFolder uf = await _client.FoldersManager.UpdateInformationAsync(updateReq);

            Assert.AreEqual(newTestname, uf.Name);

            // Test Copy Folder
            string copyTestName = GetUniqueName();
            BoxFolderRequest copyReq = new BoxFolderRequest()
            {
                Id = f.Id,
                Parent = new BoxRequestEntity() { Id = "0" },
                Name = copyTestName
            };

            BoxFolder f2 = await _client.FoldersManager.CopyAsync(copyReq);

            Assert.AreEqual(copyTestName, f2.Name);

            // Test Delete Folder
            await _client.FoldersManager.DeleteAsync(f.Id, true);
            await _client.FoldersManager.DeleteAsync(f2.Id, true);
        }
    }
}
