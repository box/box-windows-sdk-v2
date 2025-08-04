using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class FoldersManagerTests {
        public BoxClient client { get; }

        public FoldersManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetFolderInfo() {
            FolderFull rootFolder = await client.Folders.GetFolderByIdAsync(folderId: "0");
            Assert.IsTrue(rootFolder.Id == "0");
            Assert.IsTrue(rootFolder.Name == "All Files");
            Assert.IsTrue(StringUtils.ToStringRepresentation(rootFolder.Type?.Value) == "folder");
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestGetFolderFullInfoWithExtraFields() {
            FolderFull rootFolder = await client.Folders.GetFolderByIdAsync(folderId: "0", queryParams: new GetFolderByIdQueryParams() { Fields = Array.AsReadOnly(new [] {"has_collaborations","tags"}) });
            Assert.IsTrue(rootFolder.Id == "0");
            Assert.IsTrue(rootFolder.HasCollaborations == false);
            int tagsLength = NullableUtils.Unwrap(rootFolder.Tags).Count;
            Assert.IsTrue(tagsLength == 0);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateAndDeleteFolder() {
            string newFolderName = Utils.GetUUID();
            FolderFull newFolder = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: newFolderName, parent: new CreateFolderRequestBodyParentField(id: "0")));
            FolderFull createdFolder = await client.Folders.GetFolderByIdAsync(folderId: newFolder.Id);
            Assert.IsTrue(createdFolder.Name == newFolderName);
            await client.Folders.DeleteFolderByIdAsync(folderId: newFolder.Id);
            await Assert.That.IsExceptionAsync(async() => await client.Folders.GetFolderByIdAsync(folderId: newFolder.Id));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestUpdateFolder() {
            string folderToUpdateName = Utils.GetUUID();
            FolderFull folderToUpdate = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: folderToUpdateName, parent: new CreateFolderRequestBodyParentField(id: "0")));
            string updatedName = Utils.GetUUID();
            FolderFull updatedFolder = await client.Folders.UpdateFolderByIdAsync(folderId: folderToUpdate.Id, requestBody: new UpdateFolderByIdRequestBody() { Name = updatedName, Description = "Updated description" });
            Assert.IsTrue(updatedFolder.Name == updatedName);
            Assert.IsTrue(updatedFolder.Description == "Updated description");
            await client.Folders.DeleteFolderByIdAsync(folderId: updatedFolder.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCopyMoveFolderAndListFolderItems() {
            string folderOriginName = Utils.GetUUID();
            FolderFull folderOrigin = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: folderOriginName, parent: new CreateFolderRequestBodyParentField(id: "0")));
            string copiedFolderName = Utils.GetUUID();
            FolderFull copiedFolder = await client.Folders.CopyFolderAsync(folderId: folderOrigin.Id, requestBody: new CopyFolderRequestBody(parent: new CopyFolderRequestBodyParentField(id: "0")) { Name = copiedFolderName });
            Assert.IsTrue(NullableUtils.Unwrap(copiedFolder.Parent).Id == "0");
            string movedFolderName = Utils.GetUUID();
            FolderFull movedFolder = await client.Folders.UpdateFolderByIdAsync(folderId: copiedFolder.Id, requestBody: new UpdateFolderByIdRequestBody() { Parent = new UpdateFolderByIdRequestBodyParentField() { Id = folderOrigin.Id }, Name = movedFolderName });
            Assert.IsTrue(NullableUtils.Unwrap(movedFolder.Parent).Id == folderOrigin.Id);
            Items folderItems = await client.Folders.GetFolderItemsAsync(folderId: folderOrigin.Id);
            await client.Folders.DeleteFolderByIdAsync(folderId: movedFolder.Id);
            await client.Folders.DeleteFolderByIdAsync(folderId: folderOrigin.Id);
        }

    }
}