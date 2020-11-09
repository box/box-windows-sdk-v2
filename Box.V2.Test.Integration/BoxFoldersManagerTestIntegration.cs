using Box.V2.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxFoldersManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task RestoreFolder_ValidResponse()
        {
            const string folderId = "44086997331";
            BoxFolderRequest folderRequest = new BoxFolderRequest()
            {
                Id = folderId
            };

            var restoredFolder = await _client.FoldersManager.RestoreTrashedFolderAsync(folderRequest);
        }

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
        public async Task GetFolderInformation_Fields_ValidResponse()
        {
            const string folderId = "39105922916";
            var folder = await _client.FoldersManager.GetInformationAsync(folderId, fields: new List<string> { "allowed_shared_link_access_levels", "can_non_owners_invite", "is_externally_owned" });

            Assert.AreEqual(folder.CanNonOwnersInvite, true);
            Assert.AreEqual(folder.IsExternallyOwned, false);
            Assert.AreEqual(folder.AllowedSharedLinkAccessLevels[0], "collaborators", "shared link access levels could not be retrieved");
            Assert.AreEqual(folder.AllowedSharedLinkAccessLevels[1], "open", "shared link access levels could not be retrieved");
            Assert.AreEqual(folder.AllowedSharedLinkAccessLevels[2], "company", "shared link access levels could not be retrieved");
        }

        [TestMethod]
        public async Task GetFolderInformation_Fields_Metadata_ValidResponse()
        {
            const string folderId = "1927307787";
            var folder = await _client.FoldersManager.GetInformationAsync(folderId, fields: new List<string> { "metadata.enterprise_440385.testtemplate" });

            Assert.AreEqual(folderId, folder.Id, "Incorrect folder id");
            Assert.IsNotNull(folder.Metadata, "Metadata is null");
            Assert.IsNotNull(folder.Metadata["enterprise_440385"], "Scope could not be found");

            folder = await _client.FoldersManager.GetInformationAsync(folderId, fields: new List<string> { "metadata.enterprise.testtemplate" });

            Assert.AreEqual(folderId, folder.Id, "Incorrect folder id");
            Assert.IsNotNull(folder.Metadata, "Metadata is null");
            Assert.IsNotNull(folder.Metadata["enterprise"], "Scope could not be found");
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        [ExpectedException(typeof(TimeoutException))]
        public async Task UpdateFolderInformation_ValidRequest_Timeout()
        {
            BoxFolderRequest folderReq = new BoxFolderRequest()
            {
                Id = "0"
            };
            var timeout = new TimeSpan(0, 0, 0, 0, 1); // 1ms timeout, should always cancel the request
            BoxFolder f = await _client.FoldersManager.UpdateInformationAsync(folderRequest: folderReq, timeout: timeout);
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task FolderGetTrashItems_LiveSession_ValidResponse()
        {
            var results = await _client.FoldersManager.GetTrashItemsAsync(10);
            Assert.IsNotNull(results);
        }

        [TestMethod]
        [TestCategory("CI-APP-USER")]
        public async Task Watermark_Folders_CRUD()
        {
            string testName = GetUniqueName();

            // Create Folder
            BoxFolderRequest folderReq = new BoxFolderRequest()
            {
                Name = testName,
                Parent = new BoxRequestEntity() { Id = "0" }
            };

            var newFolder = await _client.FoldersManager.CreateAsync(folderReq);

            Assert.IsNotNull(newFolder, "Folder was not created");

            var folderId = newFolder.Id;

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
        [TestCategory("CI-APP-USER")]
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
        [TestCategory("CI-APP-USER")]
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
