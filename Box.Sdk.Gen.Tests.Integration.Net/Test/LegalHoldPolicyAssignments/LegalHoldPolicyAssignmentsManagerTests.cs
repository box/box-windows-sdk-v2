using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class LegalHoldPolicyAssignmentsManagerTests {
        public BoxClient client { get; }

        public LegalHoldPolicyAssignmentsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestLegalHoldPolicyAssignments() {
            string legalHoldPolicyName = Utils.GetUUID();
            const string legalHoldDescription = "test description";
            LegalHoldPolicy legalHoldPolicy = await client.LegalHoldPolicies.CreateLegalHoldPolicyAsync(requestBody: new CreateLegalHoldPolicyRequestBody(policyName: legalHoldPolicyName) { Description = legalHoldDescription, IsOngoing = true });
            string legalHoldPolicyId = legalHoldPolicy.Id;
            FileFull file = await new CommonsManager().UploadNewFileAsync();
            string fileId = file.Id;
            LegalHoldPolicyAssignment legalHoldPolicyAssignment = await client.LegalHoldPolicyAssignments.CreateLegalHoldPolicyAssignmentAsync(requestBody: new CreateLegalHoldPolicyAssignmentRequestBody(policyId: legalHoldPolicyId, assignTo: new CreateLegalHoldPolicyAssignmentRequestBodyAssignToField(type: CreateLegalHoldPolicyAssignmentRequestBodyAssignToTypeField.File, id: fileId)));
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(legalHoldPolicyAssignment.LegalHoldPolicy).Type) == "legal_hold_policy");
            string legalHoldPolicyAssignmentId = NullableUtils.Unwrap(legalHoldPolicyAssignment.Id);
            LegalHoldPolicyAssignment legalHoldPolicyAssignmentFromApi = await client.LegalHoldPolicyAssignments.GetLegalHoldPolicyAssignmentByIdAsync(legalHoldPolicyAssignmentId: legalHoldPolicyAssignmentId);
            Assert.IsTrue(NullableUtils.Unwrap(legalHoldPolicyAssignmentFromApi.Id) == legalHoldPolicyAssignmentId);
            LegalHoldPolicyAssignments legalPolicyAssignments = await client.LegalHoldPolicyAssignments.GetLegalHoldPolicyAssignmentsAsync(queryParams: new GetLegalHoldPolicyAssignmentsQueryParams(policyId: legalHoldPolicyId));
            Assert.IsTrue(NullableUtils.Unwrap(legalPolicyAssignments.Entries).Count == 1);
            FilesOnHold filesOnHold = await client.LegalHoldPolicyAssignments.GetLegalHoldPolicyAssignmentFileOnHoldAsync(legalHoldPolicyAssignmentId: legalHoldPolicyAssignmentId);
            Assert.IsTrue(NullableUtils.Unwrap(filesOnHold.Entries).Count == 1);
            Assert.IsTrue(NullableUtils.Unwrap(filesOnHold.Entries)[0].Id == fileId);
            await client.LegalHoldPolicyAssignments.DeleteLegalHoldPolicyAssignmentByIdAsync(legalHoldPolicyAssignmentId: legalHoldPolicyAssignmentId);
            await Assert.That.IsExceptionAsync(async() => await client.LegalHoldPolicyAssignments.DeleteLegalHoldPolicyAssignmentByIdAsync(legalHoldPolicyAssignmentId: legalHoldPolicyAssignmentId));
            await client.Files.DeleteFileByIdAsync(fileId: fileId);
            try {
                await client.LegalHoldPolicies.DeleteLegalHoldPolicyByIdAsync(legalHoldPolicyId: legalHoldPolicyId);
            } catch {
                Console.WriteLine(string.Concat("Could not delete Legal Policy with id: ", legalHoldPolicyId));
            }
        }

    }
}