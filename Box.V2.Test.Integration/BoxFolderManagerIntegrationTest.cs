using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Test.Integration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxFolderManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public async Task UpdateFolderInformationAsync_ValidRequest_Timeout()
        {
            var folderReq = new BoxFolderRequest()
            {
                Id = FolderId
            };
            var timeout = new TimeSpan(0, 0, 0, 0, 1); // 1ms timeout, should always cancel the request
            _ = await UserClient.FoldersManager.UpdateInformationAsync(folderRequest: folderReq, timeout: timeout);
        }

        [TestMethod]
        public async Task GetTrashItemsAsync_ForTrashedFolder_ShouldReturnTrashedFolder()
        {
            var createFolderRequest = new BoxFolderRequest
            {
                Name = GetUniqueName("folder"),
                Parent = new BoxRequestEntity { Id = FolderId }
            };
            var newFolder = await UserClient.FoldersManager.CreateAsync(createFolderRequest);
            await UserClient.FoldersManager.DeleteAsync(newFolder.Id, true);

            var response = await UserClient.FoldersManager.GetTrashItemsAsync(100);

            var trashedFolderFound = false;
            foreach (var item in response.Entries)
            {
                if (item is BoxFolder folder)
                {
                    if (item.Id == newFolder.Id)
                        trashedFolderFound = true;
                }
            }

            Assert.IsTrue(trashedFolderFound);

            await UserClient.FoldersManager.PurgeTrashedFolderAsync(newFolder.Id);
        }

        [TestMethod]
        public async Task CreateAsync_ForCorrectBoxFolderRequest_ShouldCreateNewFolder()
        {
            var folderName = GetUniqueName("Folder");
            var folderReq = new BoxFolderRequest()
            {
                Name = folderName,
                Parent = new BoxRequestEntity() { Id = FolderId }
            };

            BoxFolder folder = await UserClient.FoldersManager.CreateAsync(folderReq);

            Assert.AreEqual(folder.Name, folderName);
        }

        [TestMethod]
        public async Task GetInformationAsync_ForExistingFolder_ShouldReturnInformationRelatedToThisFolder()
        {
            var createdFolder = await CreateFolder(FolderId);

            BoxFolder folder = await UserClient.FoldersManager.GetInformationAsync(createdFolder.Id);

            Assert.AreEqual(folder.Id, createdFolder.Id);
        }

        [TestMethod]
        public async Task GetInformationAsync_ForFolderWithSharedLink_ShouldReturnInformationRelatedToThisFolder()
        {
            var createdFolder = await CreateFolderAsAdmin();

            var password = "SuperSecret123";
            var sharedLinkRequest = new BoxSharedLinkRequest
            {
                Access = BoxSharedLinkAccessType.open,
                Password = password
            };
            var sharedLink = await AdminClient.FoldersManager.CreateSharedLinkAsync(createdFolder.Id, sharedLinkRequest);

            var sharedItems = await UserClient.SharedItemsManager.SharedItemsAsync(sharedLink.SharedLink.Url, password);

            BoxFolder folder = await UserClient.FoldersManager.GetInformationAsync(sharedItems.Id, sharedLink: sharedLink.SharedLink.Url, sharedLinkPassword: password);

            Assert.AreEqual(folder.Id, createdFolder.Id);
        }

        [TestMethod]
        public async Task CreateSharedLinkAsync_ForExistingFolder_ShouldCreateSharedLinkToThatFolder()
        {
            var createdFolder = await CreateFolder(FolderId);

            var sharedLinkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };

            BoxFolder folder = await UserClient.FoldersManager.CreateSharedLinkAsync(createdFolder.Id, sharedLinkReq);

            Assert.AreEqual(BoxSharedLinkAccessType.open, folder.SharedLink.Access);
        }

        [TestMethod]
        public async Task DeleteSharedLinkAsync_ForExistingSharedLink_ShouldDeleteThisSharedLink()
        {
            var createdFolder = await CreateFolder(FolderId);
            var sharedLinkReq = new BoxSharedLinkRequest()
            {
                Access = BoxSharedLinkAccessType.open
            };
            await UserClient.FoldersManager.CreateSharedLinkAsync(createdFolder.Id, sharedLinkReq);

            var sharedLink = await UserClient.FoldersManager.DeleteSharedLinkAsync(createdFolder.Id);

            Assert.IsNull(sharedLink.SharedLink);
        }

        [TestMethod]
        public async Task UpdateInformationAsync_ForExistingFolder_ShouldCorrectlyUpdateThisFolderName()
        {
            var createdFolder = await CreateFolder(FolderId);

            var newFolderName = GetUniqueName("new_folder_name");
            var updateRequest = new BoxFolderRequest()
            {
                Id = createdFolder.Id,
                Name = newFolderName,
            };

            BoxFolder folder = await UserClient.FoldersManager.UpdateInformationAsync(updateRequest);

            Assert.AreEqual(folder.Name, newFolderName);
        }

        [TestMethod]
        public async Task CopyAsync_ForExistingFolder_ShouldCopyThisFolder()
        {
            var createdFolder = await CreateFolder(FolderId);

            var copiedFolderName = GetUniqueName("copied_folder");
            var copyRequest = new BoxFolderRequest()
            {
                Id = createdFolder.Id,
                Parent = new BoxRequestEntity() { Id = "0" },
                Name = copiedFolderName
            };

            BoxFolder copiedFolder = await UserClient.FoldersManager.CopyAsync(copyRequest);

            Assert.AreEqual(copiedFolder.Name, copiedFolderName);
            Assert.AreNotEqual(copiedFolder.Id, createdFolder.Id);

            await DeleteFolder(copiedFolder.Id);
        }

        [TestMethod]
        public async Task DeleteAsync_ForExistingFolder_ShouldDeleteThisFolder()
        {
            var createFolderRequest = new BoxFolderRequest
            {
                Name = GetUniqueName("folder"),
                Parent = new BoxRequestEntity { Id = FolderId }
            };
            var newFolder = await UserClient.FoldersManager.CreateAsync(createFolderRequest);

            await UserClient.FoldersManager.DeleteAsync(newFolder.Id, true);

            var folderItems = await UserClient.FoldersManager.GetFolderItemsAsync(FolderId, 100);

            Assert.IsFalse(folderItems.Entries.Any(x => x.Id == newFolder.Id));

            await UserClient.FoldersManager.PurgeTrashedFolderAsync(newFolder.Id);
        }

        [TestMethod]
        public async Task ApplyWatermark_ForExistingFolder_ShouldCorrectlyApplyWatermarkOnFolder()
        {
            var folder = await CreateFolder(FolderId);

            await UserClient.FoldersManager.ApplyWatermarkAsync(folder.Id);

            var fieldList = new List<string>(new string[] { "watermark_info" });
            var folderInfo = await UserClient.FoldersManager.GetInformationAsync(folder.Id, fieldList);

            Assert.IsTrue(folderInfo.WatermarkInfo.IsWatermarked);
        }

        [TestMethod]
        public async Task GetWatermarkAsync_ForExistingFolder_ShouldCorrectlyApplyWatermarkOnFolder()
        {
            var folder = await CreateFolder(FolderId);
            await UserClient.FoldersManager.ApplyWatermarkAsync(folder.Id);

            var watermark = await UserClient.FoldersManager.GetWatermarkAsync(folder.Id);

            Assert.IsNotNull(watermark);
        }

        [TestMethod]
        public async Task RemoveWatermarkAsync_ForExistingFolder_ShouldCorrectlyRemoveWatermarkFromFolder()
        {
            var folder = await CreateFolder(FolderId);
            await UserClient.FoldersManager.ApplyWatermarkAsync(folder.Id);

            var result = await UserClient.FoldersManager.RemoveWatermarkAsync(folder.Id);

            var fieldList = new List<string>(new string[] { "watermark_info" });
            var folderInfo = await UserClient.FoldersManager.GetInformationAsync(folder.Id, fieldList);
            Assert.IsFalse(folderInfo.WatermarkInfo.IsWatermarked);
        }

        [TestMethod]
        public async Task GetFolderItemsAsync_ForFolderWithSharedLink_ShouldReturnAllFolderItems()
        {
            var folder = await CreateFolderAsAdmin();
            var file = await CreateSmallFileAsAdmin(folder.Id);

            var password = "SuperSecret123";
            var sharedLinkRequest = new BoxSharedLinkRequest
            {
                Access = BoxSharedLinkAccessType.open,
                Password = password
            };
            var sharedLink = await AdminClient.FoldersManager.CreateSharedLinkAsync(folder.Id, sharedLinkRequest);

            var sharedItems = await UserClient.SharedItemsManager.SharedItemsAsync(sharedLink.SharedLink.Url, password);
            var items = await UserClient.FoldersManager.GetFolderItemsAsync(sharedItems.Id, 100, sharedLink: sharedLink.SharedLink.Url,
                sharedLinkPassword: password);


            Assert.AreEqual(items.TotalCount, 1);
            Assert.AreEqual(items.Entries[0].Id, file.Id);
        }

        [TestMethod]
        public async Task GetFolderItemsAsync_WithOffsetPagination_ShouldReturnCorrectNumberOfFolderItems()
        {
            var folder = await CreateFolder();
            await CreateSmallFile(folder.Id);
            await CreateSmallFile(folder.Id);

            var items = await UserClient.FoldersManager.GetFolderItemsAsync(folder.Id, 1, 0);

            Assert.AreEqual(items.Entries.Count, 1);
            Assert.AreEqual(items.TotalCount, 2);

            items = await UserClient.FoldersManager.GetFolderItemsAsync(folder.Id, 1, 1);

            Assert.AreEqual(items.Entries.Count, 1);

            items = await UserClient.FoldersManager.GetFolderItemsAsync(folder.Id, 1, 2);
            Assert.AreEqual(items.Entries.Count, 0);
        }

        [TestMethod]
        public async Task GetFolderItemsMarkerBasedAsync_WithMarkerPagination_ShouldReturnCorrectNumberOfFolderItems()
        {
            var folder = await CreateFolder();
            await CreateSmallFile(folder.Id);
            await CreateSmallFile(folder.Id);

            var items = await UserClient.FoldersManager.GetFolderItemsMarkerBasedAsync(folder.Id, 1);

            Assert.AreEqual(items.Entries.Count, 1);
            Assert.IsNotNull(items.NextMarker);

            var nextMarker = items.NextMarker;

            items = await UserClient.FoldersManager.GetFolderItemsMarkerBasedAsync(folder.Id, 1, marker: nextMarker);
            Assert.AreEqual(items.Entries.Count, 1);
            Assert.IsNull(items.NextMarker);
        }
    }
}
