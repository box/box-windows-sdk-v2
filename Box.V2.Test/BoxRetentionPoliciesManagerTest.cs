using Box.V2.Managers;
using Box.V2.Models;
using Box.V2.Config;
using Box.V2.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Box.V2.Models.Request;

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
    }
}
