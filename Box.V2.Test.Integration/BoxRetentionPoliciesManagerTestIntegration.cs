using System;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using Box.V2.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxRetentionPoliciesManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task RetentionPoliciesTests_LiveSession()
        {
            var policyName = "PN-" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);

            // Create retention policy
            var retentionPolicy = await Client.RetentionPoliciesManager.CreateRetentionPolicyAsync(new BoxRetentionPolicyRequest()
            {
                PolicyName = policyName,
                PolicyType = "finite",
                RetentionLength = 365,
                DispositionAction = "remove_retention"
            });

            Assert.IsNotNull(retentionPolicy.Id);
            Assert.AreEqual(policyName, retentionPolicy.PolicyName);

            // Get a retention policy
            var gRp = await Client.RetentionPoliciesManager.GetRetentionPolicyAsync(retentionPolicy.Id);

            Assert.AreEqual(retentionPolicy.PolicyName, gRp.PolicyName);

            // Get retention policies
            var gRps = await Client.RetentionPoliciesManager.GetRetentionPoliciesAsync();

            Assert.AreEqual(retentionPolicy.PolicyName, gRps.Entries.Single(rp => rp.PolicyName == policyName).PolicyName);


            // Create retention policy assignment
            var rpAssignment = await Client.RetentionPoliciesManager.CreateRetentionPolicyAssignmentAsync(new BoxRetentionPolicyAssignmentRequest()
            {
                PolicyId = retentionPolicy.Id,
                AssignTo = new BoxRequestEntity()
                {
                    Id = "12046572539",
                    Type = BoxType.folder
                }
            });

            Assert.IsNotNull(rpAssignment.Id);
            Assert.AreEqual("12046572539", rpAssignment.AssignedTo.Id);

            // Get a retention policy assignment
            var gRpa = await Client.RetentionPoliciesManager.GetRetentionPolicyAssignmentAsync(rpAssignment.Id);

            Assert.AreEqual("12046572539", gRpa.AssignedTo.Id);

            // Get retention policy assignments
            var gRpas = await Client.RetentionPoliciesManager.GetRetentionPolicyAssignmentsAsync(retentionPolicy.Id);

            Assert.AreEqual(rpAssignment.Id, gRpas.Entries.Single(rpa => rpa.Id == rpAssignment.Id).Id);

            // Get files under retention policies for assignment
            var fRPs = await Client.RetentionPoliciesManager.GetFilesUnderRetentionForAssignmentAsync(gRpas.Entries[0].Id);

            Assert.IsNotNull(fRPs.Entries[0].Id);

            // Get file version retention policies for assignment
            var fvRPs = await Client.RetentionPoliciesManager.GetFileVersionsUnderRetentionForAssignmentAsync(gRpas.Entries[0].Id);

            // Update a retention policy
            var uRp = await Client.RetentionPoliciesManager.UpdateRetentionPolicyAsync(retentionPolicy.Id, new BoxRetentionPolicyRequest()
            {
                Status = "retired"
            });

            Assert.AreEqual("retired", uRp.Status);
        }
    }
}
