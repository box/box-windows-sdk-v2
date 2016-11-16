using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxLegalHoldPoliciesManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task LegalHoldPoliciesWorkflow_ValidRequest()
        {
            // Create
            var policyName = "PN" + Guid.NewGuid().ToString().Substring(0,4);
            var newPolicyName = "N" + policyName;
            var description = "DESC";
            var filterStarted = DateTime.Now.AddDays(-30);
            var filterEnded = DateTime.Now.AddDays(-15);

            var legalHold = await _client.LegalHoldPoliciesManager.CreateLegalHoldPolicyAsync(new BoxLegalHoldPolicyRequest() {
                PolicyName = policyName,
                Description = description,
                FilterStartedAt = filterStarted,
                FilterEndedAt = filterEnded 
            });

            Assert.IsNotNull(legalHold.Id);

            // Update
            var uLegalHold = await _client.LegalHoldPoliciesManager.UpdateLegalHoldPolicyAsync(legalHold.Id, new BoxLegalHoldPolicyRequest() {
                PolicyName = newPolicyName
            });

            Assert.AreEqual(newPolicyName, uLegalHold.PolicyName);

            // Get
            var gLegalHold = await _client.LegalHoldPoliciesManager.GetLegalHoldPolicyAsync(legalHold.Id);
            Assert.AreEqual(newPolicyName, gLegalHold.PolicyName);

            // Gets
            var gLegalHolds = await _client.LegalHoldPoliciesManager.GetListLegalHoldPoliciesAsync();
            Assert.AreEqual(1, gLegalHolds.Entries.Count);
            Assert.AreEqual(newPolicyName, gLegalHolds.Entries[0].PolicyName);

            // Delete
            var deleted = await _client.LegalHoldPoliciesManager.DeleteLegalHoldPolicyAsync(legalHold.Id);
            Assert.IsTrue(deleted);
        }
    }
}
