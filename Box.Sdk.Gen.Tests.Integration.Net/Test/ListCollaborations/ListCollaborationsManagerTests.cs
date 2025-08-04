using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class ListCollaborationsManagerTests {
        [RetryableTest]
        public async System.Threading.Tasks.Task TestListCollaborations() {
            BoxClient client = new CommonsManager().GetDefaultClient();
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            FileFull file = await new CommonsManager().UploadNewFileAsync();
            GroupFull group = await client.Groups.CreateGroupAsync(requestBody: new CreateGroupRequestBody(name: Utils.GetUUID()));
            Collaboration groupCollaboration = await client.UserCollaborations.CreateCollaborationAsync(requestBody: new CreateCollaborationRequestBody(item: new CreateCollaborationRequestBodyItemField() { Type = CreateCollaborationRequestBodyItemTypeField.Folder, Id = folder.Id }, accessibleBy: new CreateCollaborationRequestBodyAccessibleByField(type: CreateCollaborationRequestBodyAccessibleByTypeField.Group) { Id = group.Id }, role: CreateCollaborationRequestBodyRoleField.Editor));
            Collaboration fileCollaboration = await client.UserCollaborations.CreateCollaborationAsync(requestBody: new CreateCollaborationRequestBody(item: new CreateCollaborationRequestBodyItemField() { Type = CreateCollaborationRequestBodyItemTypeField.File, Id = file.Id }, accessibleBy: new CreateCollaborationRequestBodyAccessibleByField(type: CreateCollaborationRequestBodyAccessibleByTypeField.User) { Id = Utils.GetEnvVar(name: "USER_ID") }, role: CreateCollaborationRequestBodyRoleField.Editor));
            Assert.IsTrue(StringUtils.ToStringRepresentation(groupCollaboration.Role?.Value) == "editor");
            Assert.IsTrue(StringUtils.ToStringRepresentation(groupCollaboration.Type?.Value) == "collaboration");
            Collaborations fileCollaborations = await client.ListCollaborations.GetFileCollaborationsAsync(fileId: file.Id);
            Assert.IsTrue(NullableUtils.Unwrap(fileCollaborations.Entries).Count > 0);
            Collaborations folderCollaborations = await client.ListCollaborations.GetFolderCollaborationsAsync(folderId: folder.Id);
            Assert.IsTrue(NullableUtils.Unwrap(folderCollaborations.Entries).Count > 0);
            CollaborationsOffsetPaginated pendingCollaborations = await client.ListCollaborations.GetCollaborationsAsync(queryParams: new GetCollaborationsQueryParams(status: GetCollaborationsQueryParamsStatusField.Pending));
            Assert.IsTrue(NullableUtils.Unwrap(pendingCollaborations.Entries).Count >= 0);
            CollaborationsOffsetPaginated groupCollaborations = await client.ListCollaborations.GetGroupCollaborationsAsync(groupId: group.Id);
            Assert.IsTrue(NullableUtils.Unwrap(groupCollaborations.Entries).Count > 0);
            await client.UserCollaborations.DeleteCollaborationByIdAsync(collaborationId: groupCollaboration.Id);
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
            await client.Groups.DeleteGroupByIdAsync(groupId: group.Id);
        }

    }
}