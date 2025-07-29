using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class TransferManagerTests {
        public BoxClient client { get; }

        public TransferManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestTransferUserContent() {
            string sourceUserName = Utils.GetUUID();
            UserFull sourceUser = await client.Users.CreateUserAsync(requestBody: new CreateUserRequestBody(name: sourceUserName) { IsPlatformAccessOnly = true });
            UserFull targetUser = await client.Users.GetUserMeAsync();
            FolderFull transferredFolder = await client.Transfer.TransferOwnedFolderAsync(userId: sourceUser.Id, requestBody: new TransferOwnedFolderRequestBody(ownedBy: new TransferOwnedFolderRequestBodyOwnedByField(id: targetUser.Id)), queryParams: new TransferOwnedFolderQueryParams() { Notify = false });
            Assert.IsTrue(NullableUtils.Unwrap(transferredFolder.OwnedBy).Id == targetUser.Id);
            await client.Folders.DeleteFolderByIdAsync(folderId: transferredFolder.Id, queryParams: new DeleteFolderByIdQueryParams() { Recursive = true });
            await client.Users.DeleteUserByIdAsync(userId: sourceUser.Id, queryParams: new DeleteUserByIdQueryParams() { Notify = false, Force = true });
        }

    }
}