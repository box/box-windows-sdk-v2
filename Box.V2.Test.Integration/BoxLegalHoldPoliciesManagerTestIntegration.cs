using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.V2.Models;
using Box.V2.Exceptions;
using System.Linq;

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
            Assert.AreEqual(newPolicyName, gLegalHolds.Entries.Single(lh => lh.PolicyName == newPolicyName).PolicyName);

            // Create assignment
            var fileId = "102438629524";
            var legalHoldAssignment = await _client.LegalHoldPoliciesManager.CreateAssignmentAsync(new BoxLegalHoldPolicyAssignmentRequest() {
                PolicyId = legalHold.Id,
                AssignTo = new BoxRequestEntity() {
                    Id = fileId,
                    Type = BoxType.file
                }
            });

            Assert.IsNotNull(legalHoldAssignment.Id);

            // Get assignment
            var gLegalHoldAssignment = await _client.LegalHoldPoliciesManager.GetAssignmentAsync(legalHoldAssignment.Id);

            Assert.AreEqual(legalHoldAssignment.Id, gLegalHoldAssignment.Id);

            // Get assignments
            var gLegalHoldAssignments = await _client.LegalHoldPoliciesManager.GetAssignmentsAsync(legalHold.Id);

            Assert.AreEqual(legalHoldAssignment.Id, gLegalHoldAssignments.Entries.Single(lha => lha.Id == gLegalHoldAssignment.Id).Id);

            // Get file version legal holds
            var fileVersionLegalHolds = await _client.LegalHoldPoliciesManager.GetFileVersionLegalHoldsAsync(legalHold.Id);

            if (fileVersionLegalHolds.Entries.Count > 0) {
                var fileVersionLegalHoldId = fileVersionLegalHolds.Entries[0].Id;

                // Get file version legal hold
                var fileVersionLegalHold = await _client.LegalHoldPoliciesManager.GetFileVersionLegalHoldAsync(fileVersionLegalHoldId);

                Assert.AreEqual(fileVersionLegalHoldId, fileVersionLegalHold.Id);
            }

            // Delete assignment
            try {
                var deleted1 = await _client.LegalHoldPoliciesManager.DeleteAssignmentAsync(legalHoldAssignment.Id);
                Assert.IsTrue(deleted1);

                // Delete
                var deleted2 = await _client.LegalHoldPoliciesManager.DeleteLegalHoldPolicyAsync(legalHold.Id);
                Assert.IsTrue(deleted2);
            }
            catch (BoxConflictException<BoxLegalHoldPolicyAssignment> exp) {
                // 409 will cause exception
            }
        }
    }
}
