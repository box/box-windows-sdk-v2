using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class LegalHoldPoliciesManagerTests {
        public BoxClient client { get; }

        public LegalHoldPoliciesManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateNotOngoingLegalHoldPolicy() {
            string legalHoldPolicyName = Utils.GetUUID();
            const string legalHoldDescription = "test description";
            System.DateTimeOffset filterStartedAt = Utils.DateTimeFromString(dateTime: "2021-01-01T00:00:00-08:00");
            System.DateTimeOffset filterEndedAt = Utils.DateTimeFromString(dateTime: "2022-01-01T00:00:00-08:00");
            LegalHoldPolicy legalHoldPolicy = await client.LegalHoldPolicies.CreateLegalHoldPolicyAsync(requestBody: new CreateLegalHoldPolicyRequestBody(policyName: legalHoldPolicyName) { Description = legalHoldDescription, IsOngoing = false, FilterStartedAt = filterStartedAt, FilterEndedAt = filterEndedAt });
            Assert.IsTrue(legalHoldPolicy.PolicyName == legalHoldPolicyName);
            Assert.IsTrue(legalHoldPolicy.Description == legalHoldDescription);
            Assert.IsTrue(Utils.DateTimeToString(dateTime: NullableUtils.Unwrap(legalHoldPolicy.FilterStartedAt)) == Utils.DateTimeToString(dateTime: filterStartedAt));
            Assert.IsTrue(Utils.DateTimeToString(dateTime: NullableUtils.Unwrap(legalHoldPolicy.FilterEndedAt)) == Utils.DateTimeToString(dateTime: filterEndedAt));
            await client.LegalHoldPolicies.DeleteLegalHoldPolicyByIdAsync(legalHoldPolicyId: legalHoldPolicy.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateUpdateGetDeleteLegalHoldPolicy() {
            string legalHoldPolicyName = Utils.GetUUID();
            const string legalHoldDescription = "test description";
            LegalHoldPolicy legalHoldPolicy = await client.LegalHoldPolicies.CreateLegalHoldPolicyAsync(requestBody: new CreateLegalHoldPolicyRequestBody(policyName: legalHoldPolicyName) { Description = legalHoldDescription, IsOngoing = true });
            Assert.IsTrue(legalHoldPolicy.PolicyName == legalHoldPolicyName);
            Assert.IsTrue(legalHoldPolicy.Description == legalHoldDescription);
            string legalHoldPolicyId = legalHoldPolicy.Id;
            LegalHoldPolicy legalHoldPolicyById = await client.LegalHoldPolicies.GetLegalHoldPolicyByIdAsync(legalHoldPolicyId: legalHoldPolicyId);
            Assert.IsTrue(legalHoldPolicyById.Id == legalHoldPolicyId);
            LegalHoldPolicies legalHoldPolicies = await client.LegalHoldPolicies.GetLegalHoldPoliciesAsync();
            Assert.IsTrue(NullableUtils.Unwrap(legalHoldPolicies.Entries).Count > 0);
            string updatedLegalHoldPolicyName = Utils.GetUUID();
            LegalHoldPolicy updatedLegalHoldPolicy = await client.LegalHoldPolicies.UpdateLegalHoldPolicyByIdAsync(legalHoldPolicyId: legalHoldPolicyId, requestBody: new UpdateLegalHoldPolicyByIdRequestBody() { PolicyName = updatedLegalHoldPolicyName });
            Assert.IsTrue(updatedLegalHoldPolicy.PolicyName == updatedLegalHoldPolicyName);
            await client.LegalHoldPolicies.DeleteLegalHoldPolicyByIdAsync(legalHoldPolicyId: legalHoldPolicyId);
        }

    }
}