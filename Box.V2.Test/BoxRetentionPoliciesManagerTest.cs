using Box.V2.Managers;
using Box.V2.Models;
using Box.V2.Models.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Box.V2.Test
{
    [TestClass]
    public class BoxRetentionPoliciesManagerTest : BoxResourceManagerTest
    {
        protected BoxRetentionPoliciesManager _retentionPoliciesManager;

        public BoxRetentionPoliciesManagerTest()
        {
            _retentionPoliciesManager = new BoxRetentionPoliciesManager(Config.Object, Service, Converter, AuthRepository);
        }

        [TestMethod]
        public async Task CreateRetentionPolicy_OptionalParams_Success()
        {
            /*** Arrange ***/
            string policyName = "Tax Documents";
            int retentionLength = 365;
            string policyType = "finite";
            string policyAction = "permanently_delete";
            string notifiedUserID = "12345";
            string responseString = "{"
                + "\"type\": \"retention_policy\","
                + "\"id\": \"123456789\","
                + "\"policy_name\": \"" + policyName + "\","
                + "\"policy_type\": \"" + policyType + "\","
                + "\"retention_length\": " + retentionLength + ","
                + "\"disposition_action\": \"" + policyAction + "\","
                + "\"status\": \"active\","
                + "\"created_by\": {"
                + " \"type\": \"user\","
                + " \"id\": \"11993747\","
                + " \"name\": \"Sean\","
                + " \"login\": \"sean@box.com\""
                + "},"
                + "\"created_at\": \"2015-05-01T11:12:54-07:00\","
                + "\"modified_at\": null,"
                + "\"can_owner_extend_retention\": true,"
                + "\"are_owners_notified\": true,"
                + "\"custom_notification_recipients\": ["
                + "  {"
                + "    \"type\": \"user\","
                + "    \"id\": \"" + notifiedUserID + "\""
                + "  }"
                + "]"
                + "}";
            Handler.Setup(h => h.ExecuteAsync<BoxRetentionPolicy>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxRetentionPolicy>>(new BoxResponse<BoxRetentionPolicy>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxRetentionPolicyRequest requestParams = new BoxRetentionPolicyRequest();
            requestParams.AreOwnersNotified = true;
            requestParams.CanOwnerExtendRetention = true;
            BoxRequestEntity notifiedUser = new BoxRequestEntity();
            notifiedUser.Type = BoxType.user;
            notifiedUser.Id = notifiedUserID;
            requestParams.CustomNotificationRecipients = new List<BoxRequestEntity>() { notifiedUser };
            requestParams.PolicyName = policyName;
            requestParams.PolicyType = policyType;
            requestParams.RetentionLength = retentionLength;
            requestParams.DispositionAction = policyAction;
            BoxRetentionPolicy results = await _retentionPoliciesManager.CreateRetentionPolicyAsync(requestParams);

            /*** Assert ***/
            Assert.AreEqual(policyAction, results.DispositionAction);
            Assert.AreEqual(policyName, results.PolicyName);
            Assert.AreEqual(policyType, results.PolicyType);
            Assert.AreEqual(retentionLength.ToString(), results.RetentionLength);
            Assert.AreEqual(true, results.CanOwnerExtendRetention);
            Assert.AreEqual(true, results.AreOwnersNotified);
            Assert.IsNotNull(results.CustomNotificationRecipients);
            Assert.AreEqual(1, results.CustomNotificationRecipients.Count);
            Assert.AreEqual(notifiedUserID, results.CustomNotificationRecipients[0].Id);
        }

        [TestMethod]
        public async Task AssignPolicyToMetadataTemplate_OptionalParams_Success()
        {
            /*** Arrange ***/
            string responseString = "{"
              + "\"type\": \"retention_policy_assignment\","     
              + "\"id\": \"3233225\","     
              + "\"retention_policy\": {"
              + "  \"type\": \"retention_policy\","         
              + "  \"id\": \"32131\","         
              + "  \"policy_name\": \"TaxDocuments\""
              + "},"
              + "\"assigned_to\": {"
              + "  \"type\": \"metadata_template\","         
              + "  \"id\": \"enterprise.my_template\""
              + "},"     
              + "\"assigned_by\": {"
              + "  \"type\": \"user\","        
              + "  \"id\": \"123456789\","        
              + "  \"name\": \"Sean\","        
              + "  \"login\": \"sean@box.com\""
              + "},"    
              + "\"assigned_at\": \"2015-07-20T14:28:09-07:00\","
              + "\"filter_fields\": ["
              + "  {"
              + "    \"field\": \"foo\","
              + "    \"value\": \"bar\""
              + "  },"
              + "  {"
              + "    \"field\": \"baz\","
              + "    \"value\": 42"
              + "  }"
              + "]"
              + "}";

            Handler.Setup(h => h.ExecuteAsync<BoxRetentionPolicyAssignment>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxRetentionPolicyAssignment>>(new BoxResponse<BoxRetentionPolicyAssignment>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }));

            /*** Act ***/
            BoxRetentionPolicyAssignmentRequest assignmentParams = new BoxRetentionPolicyAssignmentRequest();
            assignmentParams.AssignTo = new BoxRequestEntity();
            assignmentParams.AssignTo.Type = BoxType.metadata_template;
            assignmentParams.AssignTo.Id = "enterprise.my_template";
            assignmentParams.FilterFields = new List<object>
            {
                new
                {
                    field = "foo",
                    value = "bar"
                },
                new
                {
                    field = "baz",
                    value = 42
                }
            };
            BoxRetentionPolicyAssignment result = await _retentionPoliciesManager.CreateRetentionPolicyAssignmentAsync(assignmentParams);

            /*** Assert ***/
            Assert.IsInstanceOfType(result.AssignedTo, typeof(BoxMetadataTemplate));
            Assert.AreEqual("enterprise.my_template", result.AssignedTo.Id);
            Assert.AreEqual("foo", result.FilterFields[0].Field);
            Assert.AreEqual("bar", result.FilterFields[0].Value);
            Assert.AreEqual("baz", result.FilterFields[1].Field);
            Assert.AreEqual(42.ToString(), result.FilterFields[1].Value);
        }

        [TestMethod]
        public async Task GetFileVersionRetentions_OptionalParams_Success()
        {
            /*** Arrange ***/
            string fileId = "12345";
            string dispositionAfterString = "2010-08-18T00:00:00-05:00";
            DateTime dispositionAfter = new DateTime(2010, 8, 18);
            DispositionAction dispositionAction = DispositionAction.permanently_delete;
            string responseString = "{ \"entries\": [ {\"id\": \"11446498\", \"type\": \"file_version_retention\",\"file_version\": {\"id\": \"12345\",\"type\": \"file_version\", \"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\"},\"file\": {\"id\": \"12345\",\"etag\": \"1\", \"type\": \"file\",\"sequence_id\": \"3\",\"name\": \"Contract.pdf\", \"sha1\": \"85136C79CBF9FE36BB9D05D0639C70C265C18D37\", \"file_version\": {\"id\": \"12345\",\"type\": \"file_version\",\"sha1\": \"134b65991ed521fcfe4724b7d814ab8ded5185dc\" }},\"applied_at\": \"2012-12-12T10:53:43-08:00\", \"disposition_at\": \"2012-12-12T10:53:43-08:00\", \"winning_retention_policy\": { \"id\": \"12345\", \"type\": \"retention_policy\",\"policy_name\": \"Some Policy Name\", \"retention_length\": \"365\",\"disposition_action\": \"permanently_delete\"}}],\"limit\": 100,\"next_marker\": \"\",\"prev_marker\": \"\"}";
            IBoxRequest boxRequest = null;
            Handler.Setup(h => h.ExecuteAsync<BoxCollectionMarkerBased<BoxFileVersionRetention>>(It.IsAny<IBoxRequest>()))
                .Returns(Task.FromResult<IBoxResponse<BoxCollectionMarkerBased<BoxFileVersionRetention>>>(new BoxResponse<BoxCollectionMarkerBased<BoxFileVersionRetention>>()
                {
                    Status = ResponseStatus.Success,
                    ContentString = responseString
                }))
                .Callback<IBoxRequest>(r => boxRequest = r);

            /*** Act ***/
            BoxCollectionMarkerBased<BoxFileVersionRetention> results = await _retentionPoliciesManager.GetFileVersionRetentionsAsync(fileId: fileId, dispositionAfter: dispositionAfter, dispositionAction: dispositionAction);

            /*** Assert ***/

            // Request check
            Assert.IsNotNull(boxRequest);
            Assert.AreEqual(RequestMethod.Get, boxRequest.Method);
            Assert.AreEqual(fileId, boxRequest.Parameters["file_id"]);
            Assert.AreEqual(dispositionAfterString, boxRequest.Parameters["disposition_after"]);
            Assert.AreEqual(dispositionAction.ToString(), boxRequest.Parameters["disposition_action"]);

            // Response check
            Assert.AreEqual("11446498", results.Entries[0].Id);
            Assert.AreEqual("Contract.pdf", results.Entries[0].File.Name);
            Assert.AreEqual("12345", results.Entries[0].WinningRetentionPolicy.Id);
        }
    }
}
