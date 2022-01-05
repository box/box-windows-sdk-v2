using System;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Exceptions;
using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxLegalHoldPoliciesManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task LegalHoldPoliciesWorkflow_ValidRequest()
        {
            // Init
            var policyName = "PN" + Guid.NewGuid().ToString().Substring(0, 4);
            var newPolicyName = "N" + policyName;
            var description = "DESC";
            var filterStarted = DateTimeOffset.Now.AddDays(-30);
            var filterEnded = DateTimeOffset.Now.AddDays(-15);

            // Create with filter_date
            var legalHold = await Client.LegalHoldPoliciesManager.CreateLegalHoldPolicyAsync(new BoxLegalHoldPolicyRequest()
            {
                PolicyName = policyName,
                Description = description,
                FilterStartedAt = filterStarted,
                FilterEndedAt = filterEnded
            });

            Assert.IsNotNull(legalHold.Id);
            Assert.IsFalse(legalHold.IsOngoing);

            // Update
            var uLegalHold = await Client.LegalHoldPoliciesManager.UpdateLegalHoldPolicyAsync(legalHold.Id, new BoxLegalHoldPolicyRequest()
            {
                PolicyName = newPolicyName
            });

            Assert.AreEqual(newPolicyName, uLegalHold.PolicyName);

            // Get
            var gLegalHold = await Client.LegalHoldPoliciesManager.GetLegalHoldPolicyAsync(legalHold.Id);
            Assert.AreEqual(newPolicyName, gLegalHold.PolicyName);

            // Gets
            var gLegalHolds = await Client.LegalHoldPoliciesManager.GetListLegalHoldPoliciesAsync();
            Assert.AreEqual(newPolicyName, gLegalHolds.Entries.Single(lh => lh.PolicyName == newPolicyName).PolicyName);

            // Create assignment
            var fileId = "102438629524";
            var legalHoldAssignment = await Client.LegalHoldPoliciesManager.CreateAssignmentAsync(new BoxLegalHoldPolicyAssignmentRequest()
            {
                PolicyId = legalHold.Id,
                AssignTo = new BoxRequestEntity()
                {
                    Id = fileId,
                    Type = BoxType.file
                }
            });

            Assert.IsNotNull(legalHoldAssignment.Id);

            // Get assignment
            var gLegalHoldAssignment = await Client.LegalHoldPoliciesManager.GetAssignmentAsync(legalHoldAssignment.Id);

            Assert.AreEqual(legalHoldAssignment.Id, gLegalHoldAssignment.Id);

            // Get assignments
            var gLegalHoldAssignments = await Client.LegalHoldPoliciesManager.GetAssignmentsAsync(legalHold.Id);

            Assert.AreEqual(legalHoldAssignment.Id, gLegalHoldAssignments.Entries.Single(lha => lha.Id == gLegalHoldAssignment.Id).Id);

            // Get file version legal holds
            var fileVersionLegalHolds = await Client.LegalHoldPoliciesManager.GetFileVersionLegalHoldsAsync(legalHold.Id);

            if (fileVersionLegalHolds.Entries.Count > 0)
            {
                var fileVersionLegalHoldId = fileVersionLegalHolds.Entries[0].Id;

                // Get file version legal hold
                var fileVersionLegalHold = await Client.LegalHoldPoliciesManager.GetFileVersionLegalHoldAsync(fileVersionLegalHoldId);

                Assert.AreEqual(fileVersionLegalHoldId, fileVersionLegalHold.Id);
            }

            // Delete assignment
            try
            {
                var deleted1 = await Client.LegalHoldPoliciesManager.DeleteAssignmentAsync(legalHoldAssignment.Id);
                Assert.IsTrue(deleted1);

                // Delete
                var deleted2 = await Client.LegalHoldPoliciesManager.DeleteLegalHoldPolicyAsync(legalHold.Id);
                Assert.IsTrue(deleted2);
            }
            catch (BoxConflictException<BoxLegalHoldPolicyAssignment>)
            {
                // 409 will cause exception
            }
        }

        [TestMethod]
        public async Task LegalHoldPoliciesWorkflow_IsOngoing_ValidRequest()
        {
            // Init
            var policyName = "PN" + Guid.NewGuid().ToString().Substring(0, 4);
            _ = "N" + policyName;
            var description = "DESC";

            // Create with is_ongoing
            var legalHold = await Client.LegalHoldPoliciesManager.CreateLegalHoldPolicyAsync(new BoxLegalHoldPolicyRequest()
            {
                PolicyName = policyName,
                Description = description,
                isOngoing = true
            });

            Assert.IsNotNull(legalHold.Id);
            Assert.IsTrue(legalHold.IsOngoing);

            // Delete
            var deleted = await Client.LegalHoldPoliciesManager.DeleteLegalHoldPolicyAsync(legalHold.Id);
            Assert.IsTrue(deleted);
        }
    }
}
