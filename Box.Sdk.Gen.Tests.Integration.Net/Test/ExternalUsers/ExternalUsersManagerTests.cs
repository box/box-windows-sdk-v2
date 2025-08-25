using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class ExternalUsersManagerTests {
        public BoxClient client { get; }

        public ExternalUsersManagerTests() {
            client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestSubmitJobToDeleteExternalUsers() {
            FileFull file = await new CommonsManager().UploadNewFileAsync();
            Collaboration fileCollaboration = await client.UserCollaborations.CreateCollaborationAsync(requestBody: new CreateCollaborationRequestBody(item: new CreateCollaborationRequestBodyItemField() { Type = CreateCollaborationRequestBodyItemTypeField.File, Id = file.Id }, accessibleBy: new CreateCollaborationRequestBodyAccessibleByField(type: CreateCollaborationRequestBodyAccessibleByTypeField.User) { Id = Utils.GetEnvVar(name: "BOX_EXTERNAL_USER_ID") }, role: CreateCollaborationRequestBodyRoleField.Editor));
            ExternalUsersSubmitDeleteJobResponseV2025R0 externalUsersJobDeleteResponse = await client.ExternalUsers.SubmitJobToDeleteExternalUsersV2025R0Async(requestBody: new ExternalUsersSubmitDeleteJobRequestV2025R0(externalUsers: Array.AsReadOnly(new [] {new UserReferenceV2025R0(id: Utils.GetEnvVar(name: "BOX_EXTERNAL_USER_ID"))})));
            Assert.IsTrue(externalUsersJobDeleteResponse.Entries.Count == 1);
            Assert.IsTrue(externalUsersJobDeleteResponse.Entries[0].Status == 202);
            await client.Files.DeleteFileByIdAsync(fileId: file.Id);
        }

    }
}