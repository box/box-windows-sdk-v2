using System;
using Box.V2.Auth;
using Box.V2.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
            const int totalCount = 11;
            const int numFiles = 9;
            const int numFolders = 2;

            BoxCollection<BoxItem> c = await boxClient.FoldersManager.GetFolderItemsAsync("0", 3, 0, new List<string>() { 
                BoxItem.FieldName, 
                BoxItem.FieldSize, 
                BoxFolder.FieldItemCollection
             }, autoPaginate: true);

            Assert.AreEqual(totalCount, c.TotalCount, "Incorrect total count");
            Assert.AreEqual(totalCount, c.Entries.Count, "Incorrect number if items returned");

            Assert.AreEqual(numFolders, c.Entries.Count(item => item is BoxFolder), "Wrong number of Folders");
            Assert.AreEqual(numFiles, c.Entries.Count(item => item is BoxFile), "Wrong number of Files");
        }

        [TestMethod]
        public async Task FolderGetTrashItems_LiveSession_ValidResponse()
        {
            var results = await _client.FoldersManager.GetTrashItemsAsync(10);
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task Watermark_Folders_CRUD()
        {
            const string folderId = "1927308583";

            var mylist = new List<string>(new string[] { "watermark_info" });
            var folder = await _client.FoldersManager.GetInformationAsync(folderId, mylist);

            Assert.IsFalse(folder.WatermarkInfo.IsWatermarked);

            var watermark = await _client.FoldersManager.ApplyWatermarkAsync(folderId);
            Assert.IsNotNull(watermark, "Failed to apply watermark to folder");

            folder = await _client.FoldersManager.GetInformationAsync(folderId, mylist);
            Assert.IsTrue(folder.WatermarkInfo.IsWatermarked);

            var fetchedWatermark = await _client.FoldersManager.GetWatermarkAsync(folderId);
            Assert.IsNotNull(fetchedWatermark, "Failed to fetch watermark of folder");

            var result = await _client.FoldersManager.RemoveWatermarkAsync(folderId);
            Assert.IsTrue(result, "Failed to remove watermark from folder");

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

            Assert.IsNotNull(f, "Folder was not created");
            Assert.AreEqual(testName, f.Name, "Folder with incorrect name was created");

            // Test Get Information
            BoxFolder fi = await _client.FoldersManager.GetInformationAsync(f.Id);

            Assert.AreEqual(f.Id, fi.Id, "Folder Ids are not identical");
            Assert.AreEqual(testName, fi.Name, "folder name is incorrect");

            // Test Create Shared Link
            BoxSharedLinkRequest sharedLinkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };

            BoxFolder fsl = await _client.FoldersManager.CreateSharedLinkAsync(f.Id, sharedLinkReq);

            Assert.AreEqual(BoxSharedLinkAccessType.open, fsl.SharedLink.Access, "Shared link Access is not correct");

            // Test Update Folder Information
            string newTestname = GetUniqueName();
            BoxFolderRequest updateReq = new BoxFolderRequest()
            {
                Id = f.Id,
                Name = newTestname,
                SyncState = BoxSyncStateType.not_synced,
                FolderUploadEmail = new BoxEmailRequest { Access = "open" }
            };

            BoxFolder uf = await _client.FoldersManager.UpdateInformationAsync(updateReq);

            Assert.AreEqual(newTestname, uf.Name, "New folder name is not correct");

            // Test Copy Folder
            string copyTestName = GetUniqueName();
            BoxFolderRequest copyReq = new BoxFolderRequest()
            {
                Id = f.Id,
                Parent = new BoxRequestEntity() { Id = "0" },
                Name = copyTestName
            };

            BoxFolder f2 = await _client.FoldersManager.CopyAsync(copyReq);

            Assert.AreEqual(copyTestName, f2.Name, "Copied file does not have correct name");

            //Clean up - Delete Test Folders
            await _client.FoldersManager.DeleteAsync(f.Id, true);
            await _client.FoldersManager.DeleteAsync(f2.Id, true);
        }

        [TestMethod]
        public async Task FolderSharedLink_CreateAndDelete_ValidResponse()
        {
            string testName = GetUniqueName();

            // Test Create Folder
            BoxFolderRequest folderReq = new BoxFolderRequest()
            {
                Name = testName,
                Parent = new BoxRequestEntity() { Id = "0" }
            };

            BoxFolder f = await _client.FoldersManager.CreateAsync(folderReq);
            Assert.IsNotNull(f, "Folder was not created");
            Assert.AreEqual(testName, f.Name, "Folder with incorrect name was created");

            BoxSharedLinkRequest sharedLinkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open,
                Permissions = new BoxPermissionsRequest
                {
                    Download = true,
                }
            };

            BoxFolder sl = await _client.FoldersManager.CreateSharedLinkAsync(f.Id, sharedLinkReq);
            Assert.AreEqual(sl.Id, f.Id);
            Assert.IsNotNull(sl.SharedLink);
            Assert.AreEqual(sl.SharedLink.Access, BoxSharedLinkAccessType.open);
            Assert.IsNotNull(sl.SharedLink.Permissions);
            Assert.AreEqual(sl.SharedLink.Permissions.CanDownload, true);


            sharedLinkReq = new BoxSharedLinkRequest()
            {
                Access = null,
                Permissions = new BoxPermissionsRequest
                {
                    Download = false,
                }
            };

            sl = await _client.FoldersManager.CreateSharedLinkAsync(f.Id, sharedLinkReq);
            Assert.AreEqual(sl.Id, f.Id);
            Assert.IsNotNull(sl.SharedLink);
            Assert.AreEqual(sl.SharedLink.Access, BoxSharedLinkAccessType.open);
            Assert.IsNotNull(sl.SharedLink.Permissions);
            Assert.AreEqual(sl.SharedLink.Permissions.CanDownload, false);

            sl = await _client.FoldersManager.DeleteSharedLinkAsync(f.Id);
            Assert.AreEqual(sl.Id, f.Id);
            Assert.IsNull(sl.SharedLink);

            //Clean up - Delete Test Folders
            await _client.FoldersManager.DeleteAsync(f.Id, true);
        }
    }

}
