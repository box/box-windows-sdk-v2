using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class FileVersionRetentionsManagerTests {
        public BoxClient client { get; }

        public FileVersionRetentionsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateUpdateGetDeleteRetentionPolicy() {
            string description = Utils.GetUUID();
            RetentionPolicy retentionPolicy = await client.RetentionPolicies.CreateRetentionPolicyAsync(requestBody: new CreateRetentionPolicyRequestBody(policyName: Utils.GetUUID(), policyType: CreateRetentionPolicyRequestBodyPolicyTypeField.Finite, dispositionAction: CreateRetentionPolicyRequestBodyDispositionActionField.RemoveRetention) { RetentionLength = "1", Description = description, CanOwnerExtendRetention = false, RetentionType = CreateRetentionPolicyRequestBodyRetentionTypeField.Modifiable });
            Assert.IsTrue(retentionPolicy.Description == description);
            Assert.IsTrue(retentionPolicy.CanOwnerExtendRetention == false);
            Assert.IsTrue(StringUtils.ToStringRepresentation(retentionPolicy.RetentionType) == "modifiable");
            FolderFull folder = await new CommonsManager().CreateNewFolderAsync();
            RetentionPolicyAssignment retentionPolicyAssignment = await client.RetentionPolicyAssignments.CreateRetentionPolicyAssignmentAsync(requestBody: new CreateRetentionPolicyAssignmentRequestBody(policyId: retentionPolicy.Id, assignTo: new CreateRetentionPolicyAssignmentRequestBodyAssignToField(type: CreateRetentionPolicyAssignmentRequestBodyAssignToTypeField.Folder) { Id = folder.Id }));
            Assert.IsTrue(NullableUtils.Unwrap(retentionPolicyAssignment.RetentionPolicy).Id == retentionPolicy.Id);
            Assert.IsTrue(NullableUtils.Unwrap(retentionPolicyAssignment.AssignedTo).Id == folder.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(retentionPolicyAssignment.AssignedTo).Type) == StringUtils.ToStringRepresentation(folder.Type?.Value));
            Files files = await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: Utils.GetUUID(), parent: new UploadFileRequestBodyAttributesParentField(id: folder.Id)), file: Utils.GenerateByteStream(size: 10)));
            FileFull file = NullableUtils.Unwrap(files.Entries)[0];
            Files newFiles = await client.Uploads.UploadFileVersionAsync(fileId: file.Id, requestBody: new UploadFileVersionRequestBody(attributes: new UploadFileVersionRequestBodyAttributesField(name: NullableUtils.Unwrap(file.Name)), file: Utils.GenerateByteStream(size: 20)));
            FileFull newFile = NullableUtils.Unwrap(newFiles.Entries)[0];
            Assert.IsTrue(newFile.Id == file.Id);
            FileVersionRetentions fileVersionRetentions = await client.FileVersionRetentions.GetFileVersionRetentionsAsync();
            int fileVersionRetentionsCount = NullableUtils.Unwrap(fileVersionRetentions.Entries).Count;
            Assert.IsTrue(fileVersionRetentionsCount >= 0);
            if (fileVersionRetentionsCount == 0) {
                await client.RetentionPolicies.DeleteRetentionPolicyByIdAsync(retentionPolicyId: retentionPolicy.Id);
                await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id, queryParams: new DeleteFolderByIdQueryParams() { Recursive = true });
                return;
            }
            FileVersionRetention fileVersionRetention = NullableUtils.Unwrap(fileVersionRetentions.Entries)[0];
            FileVersionRetention fileVersionRetentionById = await client.FileVersionRetentions.GetFileVersionRetentionByIdAsync(fileVersionRetentionId: NullableUtils.Unwrap(fileVersionRetention.Id));
            Assert.IsTrue(fileVersionRetentionById.Id == fileVersionRetention.Id);
            await client.RetentionPolicies.DeleteRetentionPolicyByIdAsync(retentionPolicyId: retentionPolicy.Id);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id, queryParams: new DeleteFolderByIdQueryParams() { Recursive = true });
        }

    }
}