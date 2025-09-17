using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class FolderLocksManagerTests {
        public BoxClient client { get; }

        public FolderLocksManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestFolderLocks() {
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            FolderLocks folderLocks = await client.FolderLocks.GetFolderLocksAsync(queryParams: new GetFolderLocksQueryParams(folderId: folder.Id));
            Assert.IsTrue(NullableUtils.Unwrap(folderLocks.Entries).Count == 0);
            FolderLock folderLock = await client.FolderLocks.CreateFolderLockAsync(requestBody: new CreateFolderLockRequestBody(folder: new CreateFolderLockRequestBodyFolderField(id: folder.Id, type: "folder")) { LockedOperations = new CreateFolderLockRequestBodyLockedOperationsField(move: true, delete: true) });
            Assert.IsTrue(NullableUtils.Unwrap(folderLock.Folder).Id == folder.Id);
            Assert.IsTrue(NullableUtils.Unwrap(folderLock.LockedOperations).Move == true);
            Assert.IsTrue(NullableUtils.Unwrap(folderLock.LockedOperations).Delete == true);
            await client.FolderLocks.DeleteFolderLockByIdAsync(folderLockId: NullableUtils.Unwrap(folderLock.Id));
            await Assert.That.IsExceptionAsync(async() => await client.FolderLocks.DeleteFolderLockByIdAsync(folderLockId: NullableUtils.Unwrap(folderLock.Id)));
            FolderLocks newFolderLocks = await client.FolderLocks.GetFolderLocksAsync(queryParams: new GetFolderLocksQueryParams(folderId: folder.Id));
            Assert.IsTrue(NullableUtils.Unwrap(newFolderLocks.Entries).Count == 0);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

    }
}