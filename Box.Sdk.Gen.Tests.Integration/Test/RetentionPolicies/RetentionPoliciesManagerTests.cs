using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;
using Box.Sdk.Gen;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class RetentionPoliciesManagerTests {
        public BoxClient client { get; }

        public RetentionPoliciesManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateUpdateGetDeleteRetentionPolicy() {
            string retentionPolicyName = Utils.GetUUID();
            const string retentionDescription = "test description";
            RetentionPolicy retentionPolicy = await client.RetentionPolicies.CreateRetentionPolicyAsync(requestBody: new CreateRetentionPolicyRequestBody(policyName: retentionPolicyName, policyType: CreateRetentionPolicyRequestBodyPolicyTypeField.Finite, dispositionAction: CreateRetentionPolicyRequestBodyDispositionActionField.RemoveRetention) { AreOwnersNotified = true, CanOwnerExtendRetention = true, Description = retentionDescription, RetentionLength = "1", RetentionType = CreateRetentionPolicyRequestBodyRetentionTypeField.Modifiable });
            Assert.IsTrue(retentionPolicy.PolicyName == retentionPolicyName);
            Assert.IsTrue(retentionPolicy.Description == retentionDescription);
            RetentionPolicy retentionPolicyById = await client.RetentionPolicies.GetRetentionPolicyByIdAsync(retentionPolicyId: retentionPolicy.Id);
            Assert.IsTrue(retentionPolicyById.Id == retentionPolicy.Id);
            RetentionPolicies retentionPolicies = await client.RetentionPolicies.GetRetentionPoliciesAsync();
            Assert.IsTrue(NullableUtils.Unwrap(retentionPolicies.Entries).Count > 0);
            string updatedRetentionPolicyName = Utils.GetUUID();
            RetentionPolicy updatedRetentionPolicy = await client.RetentionPolicies.UpdateRetentionPolicyByIdAsync(retentionPolicyId: retentionPolicy.Id, requestBody: new UpdateRetentionPolicyByIdRequestBody() { PolicyName = updatedRetentionPolicyName });
            Assert.IsTrue(updatedRetentionPolicy.PolicyName == updatedRetentionPolicyName);
            await client.RetentionPolicies.DeleteRetentionPolicyByIdAsync(retentionPolicyId: retentionPolicy.Id);
        }

    }
}