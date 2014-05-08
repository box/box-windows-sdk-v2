﻿using System;
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
            const int totalCount = 11;
            const int numFiles = 9;
            const int numFolders = 2;

            BoxCollection<BoxItem> c = await _client.FoldersManager.GetFolderItemsAsync("0", 50, 0, new List<string>() { 
                BoxItem.FieldName, 
                BoxItem.FieldSize, 
                BoxItem.FieldModifiedAt, 
                BoxItem.FieldModifiedBy,
                BoxFolder.FieldItemCollection
            });

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
    }
}
