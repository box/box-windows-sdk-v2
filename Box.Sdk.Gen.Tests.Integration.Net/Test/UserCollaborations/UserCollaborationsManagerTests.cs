using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class UserCollaborationsManagerTests {
        public BoxClient client { get; }

        public UserCollaborationsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestUserCollaborations() {
            string userName = Utils.GetUUID();
            string userLogin = string.Concat(Utils.GetUUID(), "@gmail.com");
            UserFull user = await client.Users.CreateUserAsync(requestBody: new CreateUserRequestBody(name: userName) { Login = userLogin, IsPlatformAccessOnly = true });
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            Collaboration collaboration = await client.UserCollaborations.CreateCollaborationAsync(requestBody: new CreateCollaborationRequestBody(item: new CreateCollaborationRequestBodyItemField() { Type = CreateCollaborationRequestBodyItemTypeField.Folder, Id = folder.Id }, accessibleBy: new CreateCollaborationRequestBodyAccessibleByField(type: CreateCollaborationRequestBodyAccessibleByTypeField.User) { Id = user.Id }, role: CreateCollaborationRequestBodyRoleField.Editor));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(collaboration.Role)) == "editor");
            string collaborationId = collaboration.Id;
            Collaboration collaborationFromApi = await client.UserCollaborations.GetCollaborationByIdAsync(collaborationId: collaborationId);
            Assert.IsTrue(collaborationId == collaborationFromApi.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(collaborationFromApi.Status)) == "accepted");
            Assert.IsTrue(StringUtils.ToStringRepresentation(collaborationFromApi.Type?.Value) == "collaboration");
            Assert.IsTrue(collaborationFromApi.InviteEmail == null);
            Collaboration? updatedCollaboration = await client.UserCollaborations.UpdateCollaborationByIdAsync(collaborationId: collaborationId, requestBody: new UpdateCollaborationByIdRequestBody(role: UpdateCollaborationByIdRequestBodyRoleField.Viewer));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(updatedCollaboration).Role)) == "viewer");
            await client.UserCollaborations.DeleteCollaborationByIdAsync(collaborationId: collaborationId);
            await Assert.That.IsExceptionAsync(async() => await client.UserCollaborations.GetCollaborationByIdAsync(collaborationId: collaborationId));
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
            await client.Users.DeleteUserByIdAsync(userId: user.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestConvertingUserCollaborationToOwnership() {
            string userName = Utils.GetUUID();
            string userLogin = string.Concat(Utils.GetUUID(), "@gmail.com");
            UserFull user = await client.Users.CreateUserAsync(requestBody: new CreateUserRequestBody(name: userName) { Login = userLogin, IsPlatformAccessOnly = true });
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            Collaboration collaboration = await client.UserCollaborations.CreateCollaborationAsync(requestBody: new CreateCollaborationRequestBody(item: new CreateCollaborationRequestBodyItemField() { Type = CreateCollaborationRequestBodyItemTypeField.Folder, Id = folder.Id }, accessibleBy: new CreateCollaborationRequestBodyAccessibleByField(type: CreateCollaborationRequestBodyAccessibleByTypeField.User) { Id = user.Id }, role: CreateCollaborationRequestBodyRoleField.Editor));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(collaboration.Role)) == "editor");
            Collaboration? ownerCollaboration = await client.UserCollaborations.UpdateCollaborationByIdAsync(collaborationId: collaboration.Id, requestBody: new UpdateCollaborationByIdRequestBody(role: UpdateCollaborationByIdRequestBodyRoleField.Owner));
            Assert.IsTrue(ownerCollaboration == null);
            Collaborations folderCollaborations = await client.ListCollaborations.GetFolderCollaborationsAsync(folderId: folder.Id);
            Collaboration folderCollaboration = NullableUtils.Unwrap(folderCollaborations.Entries)[0];
            await client.UserCollaborations.DeleteCollaborationByIdAsync(collaborationId: folderCollaboration.Id);
            BoxClient userClient = client.WithAsUserHeader(userId: user.Id);
            await userClient.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
            await userClient.TrashedFolders.DeleteTrashedFolderByIdAsync(folderId: folder.Id);
            await client.Users.DeleteUserByIdAsync(userId: user.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestExternalUserCollaborations() {
            string userName = Utils.GetUUID();
            string userLogin = string.Concat(Utils.GetUUID(), "@boxdemo.com");
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            Collaboration collaboration = await client.UserCollaborations.CreateCollaborationAsync(requestBody: new CreateCollaborationRequestBody(item: new CreateCollaborationRequestBodyItemField() { Type = CreateCollaborationRequestBodyItemTypeField.Folder, Id = folder.Id }, accessibleBy: new CreateCollaborationRequestBodyAccessibleByField(type: CreateCollaborationRequestBodyAccessibleByTypeField.User) { Login = userLogin }, role: CreateCollaborationRequestBodyRoleField.Editor));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(collaboration.Role)) == "editor");
            string collaborationId = collaboration.Id;
            Collaboration collaborationFromApi = await client.UserCollaborations.GetCollaborationByIdAsync(collaborationId: collaborationId);
            Assert.IsTrue(collaborationId == collaborationFromApi.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(collaborationFromApi.Status)) == "pending");
            Assert.IsTrue(StringUtils.ToStringRepresentation(collaborationFromApi.Type?.Value) == "collaboration");
            Assert.IsTrue(collaborationFromApi.InviteEmail == userLogin);
            Collaboration? updatedCollaboration = await client.UserCollaborations.UpdateCollaborationByIdAsync(collaborationId: collaborationId, requestBody: new UpdateCollaborationByIdRequestBody(role: UpdateCollaborationByIdRequestBodyRoleField.Viewer));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(NullableUtils.Unwrap(updatedCollaboration).Role)) == "viewer");
            await client.UserCollaborations.DeleteCollaborationByIdAsync(collaborationId: collaborationId);
            await Assert.That.IsExceptionAsync(async() => await client.UserCollaborations.GetCollaborationByIdAsync(collaborationId: collaborationId));
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

    }
}