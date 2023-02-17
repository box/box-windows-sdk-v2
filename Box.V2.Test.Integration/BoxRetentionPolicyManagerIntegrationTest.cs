using System;
using System.Collections.Generic;
using System.Linq;
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

        [TestMethod]
        public async Task CreateRetentionPolicyAsync_WithDescription_ShouldSucceed()
        {
            var retentionPolicyRequest = new BoxRetentionPolicyRequest
            {
                PolicyName = GetUniqueName("policy"),
                PolicyType = "finite",
                RetentionLength = 1,
                DispositionAction = DispositionAction.permanently_delete.ToString(),
                RetentionType = BoxRetentionType.modifiable,
                Description = "Policy to retain all reports for at least one month"
            };
            var policy = await AdminClient.RetentionPoliciesManager.CreateRetentionPolicyAsync(retentionPolicyRequest);

            Assert.AreEqual(retentionPolicyRequest.PolicyName, policy.PolicyName);
            Assert.AreEqual(retentionPolicyRequest.Description, policy.Description);

            var policyRetire = new BoxRetentionPolicyRequest
            {
                Status = "retired"
            };

            var retireRes = await AdminClient.RetentionPoliciesManager.UpdateRetentionPolicyAsync(policy.Id, policyRetire);
            Assert.AreEqual(retireRes.PolicyName, retentionPolicyRequest.PolicyName);
        }

        [TestMethod]
        public async Task CreateRetentionPolicyAssignmentAsync_WithUploadedDateStartField_ShouldSuccess()
        {
            var retentionPolicy = await CreateRetentionPolicy();
            var metadataTemplate = await CreateMetadataTemplate();
            var policyAssignmentReq = new BoxRetentionPolicyAssignmentRequest()
            {
                PolicyId = retentionPolicy.Id,
                AssignTo = new BoxRequestEntity()
                {
                    Id = metadataTemplate.Id,
                    Type = BoxType.metadata_template
                },
                StartDateField = "upload_date"
            };

            var policyAssignment = await AdminClient.RetentionPoliciesManager.CreateRetentionPolicyAssignmentAsync(policyAssignmentReq);
            Assert.AreEqual(policyAssignmentReq.StartDateField, policyAssignment.StartDateField);

            var result = await AdminClient.RetentionPoliciesManager.DeleteRetentionPolicyAssignmentAsync(policyAssignment.Id);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CreateRetentionPolicyAssignmentAsync_WithCustomStartDateField_ShouldSuccess()
        {
            var retentionPolicy = await CreateRetentionPolicy();
            var customFieldName = "custom_field";
            var metadataFields = new List<BoxMetadataTemplateField>()
            {
                new BoxMetadataTemplateField
                {
                    Key = customFieldName,
                    DisplayName = customFieldName,
                    Type = "date"
                }
            };

            var metadataTemplate = await CreateMetadataTemplate(metadataFields);

            var policyAssignmentReq = new BoxRetentionPolicyAssignmentRequest()
            {
                PolicyId = retentionPolicy.Id,
                AssignTo = new BoxRequestEntity()
                {
                    Id = metadataTemplate.Id,
                    Type = BoxType.metadata_template
                },
                StartDateField = metadataTemplate.Fields.First(x => x.Key == customFieldName).Id
            };

            var policyAssignment = await AdminClient.RetentionPoliciesManager.CreateRetentionPolicyAssignmentAsync(policyAssignmentReq);
            Assert.AreEqual(policyAssignmentReq.StartDateField, policyAssignment.StartDateField);

            var result = await AdminClient.RetentionPoliciesManager.DeleteRetentionPolicyAssignmentAsync(policyAssignment.Id);
            Assert.IsTrue(result);
        }
    }
}
