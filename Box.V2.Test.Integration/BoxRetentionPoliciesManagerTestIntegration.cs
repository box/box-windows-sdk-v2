using Box.V2.Models;
using Box.V2.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            var retentionPolicy = await _client.RetentionPoliciesManager.CreateRetentionPolicyAsync(new BoxRetentionPolicyRequest() {
                PolicyName = policyName,
                PolicyType = "finite",
                RetentionLength = 365,
                DispositionAction = "remove_retention"
            });

            Assert.IsNotNull(retentionPolicy.Id);
            Assert.AreEqual(policyName, retentionPolicy.PolicyName);

            // Get a retention policy
            var gRp = await _client.RetentionPoliciesManager.GetRetentionPolicyAsync(retentionPolicy.Id);

            Assert.AreEqual(retentionPolicy.PolicyName, gRp.PolicyName);

            // Get retention policies
            var gRps = await _client.RetentionPoliciesManager.GetRetentionPoliciesAsync();

            Assert.AreEqual(retentionPolicy.PolicyName, gRps.Entries.Single(rp => rp.PolicyName == policyName).PolicyName);


            // Create retention policy assignment
            var rpAssignment = await _client.RetentionPoliciesManager.CreateRetentionPolicyAssignmentAsync(new BoxRetentionPolicyAssignmentRequest() {
                PolicyId = retentionPolicy.Id,
                AssignTo = new BoxRequestEntity() {
                    Id = "12046572539",
                    Type = BoxType.folder
                }
            });

            Assert.IsNotNull(rpAssignment.Id);
            Assert.AreEqual("12046572539", rpAssignment.AssignedTo.Id);

            // Get a retention policy assignment
            var gRpa = await _client.RetentionPoliciesManager.GetRetentionPolicyAssignmentAsync(rpAssignment.Id);

            Assert.AreEqual("12046572539", gRpa.AssignedTo.Id);

            // Get retention policy assignments
            var gRpas = await _client.RetentionPoliciesManager.GetRetentionPolicyAssignmentsAsync(retentionPolicy.Id);

            Assert.AreEqual(rpAssignment.Id, gRpas.Entries.Single(rpa => rpa.Id == rpAssignment.Id).Id);

            // Get file version retention policies
            var fvRPs = await _client.RetentionPoliciesManager.GetFileVersionRetentionsAsync();

            // Get a file version retention policy
            var fvRP = await _client.RetentionPoliciesManager.GetFileVersionRetentionAsync(fvRPs.Entries[0].Id);

            Assert.IsNotNull(fvRP.Id);

            // Update a retention policy
            var uRp = await _client.RetentionPoliciesManager.UpdateRetentionPolicyAsync(retentionPolicy.Id, new BoxRetentionPolicyRequest() {
                Status = "retired"
            });

            Assert.AreEqual("retired", uRp.Status);
        }
    }
}
