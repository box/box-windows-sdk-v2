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
        public async Task CreateRetentionPolicyAssignmentAsync_ForRetentionPolicyAssignmentRequest_ShouldSuccess()
        {
            var adminFolder = await CreateFolderAsAdmin("0");
            var retentionPolicy = await CreateRetentionPolicy();
            var policyAssignmentReq = new BoxRetentionPolicyAssignmentRequest()
            {
                PolicyId = retentionPolicy.Id,
                AssignTo = new BoxRequestEntity()
                {
                    Id = adminFolder.Id,
                    Type = BoxType.folder
                }
            };
            var policyAssignment = await AdminClient.RetentionPoliciesManager.CreateRetentionPolicyAssignmentAsync(policyAssignmentReq);
            Assert.AreEqual(retentionPolicy.Id, policyAssignment.RetentionPolicy.Id);
            Assert.AreEqual(adminFolder.Id, policyAssignment.AssignedTo.Id);

            var result = await AdminClient.RetentionPoliciesManager.DeleteRetentionPolicyAssignmentAsync(policyAssignment.Id);
            Assert.IsTrue(result);
        }
    }
}
