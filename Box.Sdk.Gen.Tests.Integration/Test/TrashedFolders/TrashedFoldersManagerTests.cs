using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class TrashedFoldersManagerTests {
        public BoxClient client { get; }

        public TrashedFoldersManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestTrashedFolders() {
            FolderFull folder = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: Utils.GetUUID(), parent: new CreateFolderRequestBodyParentField(id: "0")));
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
            TrashFolder fromTrash = await client.TrashedFolders.GetTrashedFolderByIdAsync(folderId: folder.Id);
            Assert.IsTrue(fromTrash.Id == folder.Id);
            Assert.IsTrue(fromTrash.Name == folder.Name);
            await Assert.That.IsExceptionAsync(async() => await client.Folders.GetFolderByIdAsync(folderId: folder.Id));
            TrashFolderRestored restoredFolder = await client.TrashedFolders.RestoreFolderFromTrashAsync(folderId: folder.Id);
            FolderFull fromApi = await client.Folders.GetFolderByIdAsync(folderId: folder.Id);
            Assert.IsTrue(restoredFolder.Id == fromApi.Id);
            Assert.IsTrue(restoredFolder.Name == fromApi.Name);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
            await client.TrashedFolders.DeleteTrashedFolderByIdAsync(folderId: folder.Id);
            await Assert.That.IsExceptionAsync(async() => await client.TrashedFolders.GetTrashedFolderByIdAsync(folderId: folder.Id));
        }

    }
}