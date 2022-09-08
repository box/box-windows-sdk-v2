using System;
using System.Threading.Tasks;
using Box.V2.Exceptions;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Test.Integration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxRetentionPolicyManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task CreateRetentionPolicyAsync_ForRetentionPolicyRequest_ShouldCreateNewRetentionPolicy()
        {
            var retentionPolicyName = GetUniqueName("RetentionPolicy");
            var policyType = "finite";
            var retentionType = "modifiable";
            var retentionPolicyReq = new BoxRetentionPolicyRequest()
            {
                PolicyName = retentionPolicyName,
                RetentionLength = 1,
                RetentionType = retentionType,
                PolicyType = policyType,
                DispositionAction = "permanently_delete",
            };
            var policy = await UserClient.RetentionPoliciesManager.CreateRetentionPolicyAsync(retentionPolicyReq);
            Assert.AreEqual(retentionPolicyName, policy.PolicyName);
            Assert.AreEqual(1, policy.RetentionLength);
            Assert.AreEqual(retentionType, policy.RetentionType);
            Assert.AreEqual(policyType, policy.PolicyType);
            Assert.AreEqual("active", policy.Status);

            await UserClient.RetentionPoliciesManager.UpdateRetentionPolicyAsync(policy.Id, new BoxRetentionPolicyRequest() { Status = "retired" });
        }

        [TestMethod]
        public async Task CreateRetentionPolicyAssignmentAsync_ForRetentionPolicyAssignmentRequest_ShouldSuccess()
        {
            var adminFolder = await CreateFolderAsAdmin("0");
            var uploadedFile = await CreateSmallFileAsAdmin(adminFolder.Id);
            var retentionPolicy = await CreateRetentionPolicy(adminFolder.Id);

            var policyAssignmentReq = new BoxRetentionPolicyAssignmentRequest()
            {
                PolicyId = retentionPolicy.Id,
                AssignTo = new BoxRequestEntity() { Id = adminFolder.Id }
            };
            var policyAssignment = await UserClient.RetentionPoliciesManager.CreateRetentionPolicyAssignmentAsync(policyAssignmentReq);
            Assert.AreEqual(retentionPolicy.Id, policyAssignment.RetentionPolicy.Id);
            Assert.AreEqual(adminFolder.Id, policyAssignment.AssignedTo.Id);

            var result = await UserClient.RetentionPoliciesManager.DeleteRetentionPolicyAssignmentAsync(policyAssignment.Id);
            Assert.IsTrue(result);
        }
    }
}
