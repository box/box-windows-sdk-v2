using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class RetentionPolicyAssignmentsManagerTests {
        public BoxClient client { get; }

        public RetentionPolicyAssignmentsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateUpdateGetDeleteRetentionPolicyAssignment() {
            string retentionPolicyName = Utils.GetUUID();
            const string retentionDescription = "test description";
            RetentionPolicy retentionPolicy = await client.RetentionPolicies.CreateRetentionPolicyAsync(requestBody: new CreateRetentionPolicyRequestBody(policyName: retentionPolicyName, policyType: CreateRetentionPolicyRequestBodyPolicyTypeField.Finite, dispositionAction: CreateRetentionPolicyRequestBodyDispositionActionField.RemoveRetention) { AreOwnersNotified = true, CanOwnerExtendRetention = true, Description = retentionDescription, RetentionLength = "1", RetentionType = CreateRetentionPolicyRequestBodyRetentionTypeField.Modifiable });
            FolderFull folder = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: Utils.GetUUID(), parent: new CreateFolderRequestBodyParentField(id: "0")));
            Files files = await client.Uploads.UploadFileAsync(requestBody: new UploadFileRequestBody(attributes: new UploadFileRequestBodyAttributesField(name: Utils.GetUUID(), parent: new UploadFileRequestBodyAttributesParentField(id: folder.Id)), file: Utils.GenerateByteStream(size: 10)));
            FileFull file = NullableUtils.Unwrap(files.Entries)[0];
            Files newVersions = await client.Uploads.UploadFileVersionAsync(fileId: file.Id, requestBody: new UploadFileVersionRequestBody(attributes: new UploadFileVersionRequestBodyAttributesField(name: Utils.GetUUID()), file: Utils.GenerateByteStream(size: 20)));
            FileFull newVersion = NullableUtils.Unwrap(newVersions.Entries)[0];
            RetentionPolicyAssignment retentionPolicyAssignment = await client.RetentionPolicyAssignments.CreateRetentionPolicyAssignmentAsync(requestBody: new CreateRetentionPolicyAssignmentRequestBody(policyId: retentionPolicy.Id, assignTo: new CreateRetentionPolicyAssignmentRequestBodyAssignToField(type: CreateRetentionPolicyAssignmentRequestBodyAssignToTypeField.Folder) { Id = folder.Id }));
            Assert.IsTrue(NullableUtils.Unwrap(retentionPolicyAssignment.RetentionPolicy).Id == retentionPolicy.Id);
            Assert.IsTrue(NullableUtils.Unwrap(retentionPolicyAssignment.AssignedTo).Id == folder.Id);
            RetentionPolicyAssignment retentionPolicyAssignmentById = await client.RetentionPolicyAssignments.GetRetentionPolicyAssignmentByIdAsync(retentionPolicyAssignmentId: retentionPolicyAssignment.Id);
            Assert.IsTrue(retentionPolicyAssignmentById.Id == retentionPolicyAssignment.Id);
            RetentionPolicyAssignments retentionPolicyAssignments = await client.RetentionPolicyAssignments.GetRetentionPolicyAssignmentsAsync(retentionPolicyId: retentionPolicy.Id);
            Assert.IsTrue(NullableUtils.Unwrap(retentionPolicyAssignments.Entries).Count == 1);
            FilesUnderRetention filesUnderRetention = await client.RetentionPolicyAssignments.GetFilesUnderRetentionPolicyAssignmentAsync(retentionPolicyAssignmentId: retentionPolicyAssignment.Id);
            Assert.IsTrue(NullableUtils.Unwrap(filesUnderRetention.Entries).Count == 1);
            await client.RetentionPolicyAssignments.DeleteRetentionPolicyAssignmentByIdAsync(retentionPolicyAssignmentId: retentionPolicyAssignment.Id);
            RetentionPolicyAssignments retentionPolicyAssignmentsAfterDelete = await client.RetentionPolicyAssignments.GetRetentionPolicyAssignmentsAsync(retentionPolicyId: retentionPolicy.Id);
            Assert.IsTrue(NullableUtils.Unwrap(retentionPolicyAssignmentsAfterDelete.Entries).Count == 0);
            await client.RetentionPolicies.DeleteRetentionPolicyByIdAsync(retentionPolicyId: retentionPolicy.Id);
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

    }
}