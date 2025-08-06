using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class IntegrationMappingsManagerTests {
        public BoxClient client { get; }

        public IntegrationMappingsManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestSlackIntegrationMappings() {
            string userId = Utils.GetEnvVar(name: "USER_ID");
            string slackAutomationUserId = Utils.GetEnvVar(name: "SLACK_AUTOMATION_USER_ID");
            string slackOrgId = Utils.GetEnvVar(name: "SLACK_ORG_ID");
            string slackPartnerItemId = Utils.GetEnvVar(name: "SLACK_PARTNER_ITEM_ID");
            BoxClient userClient = new CommonsManager().GetDefaultClientWithUserSubject(userId: userId);
            FolderFull folder = await userClient.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: Utils.GetUUID(), parent: new CreateFolderRequestBodyParentField(id: "0")));
            await userClient.UserCollaborations.CreateCollaborationAsync(requestBody: new CreateCollaborationRequestBody(item: new CreateCollaborationRequestBodyItemField() { Type = CreateCollaborationRequestBodyItemTypeField.Folder, Id = folder.Id }, accessibleBy: new CreateCollaborationRequestBodyAccessibleByField(type: CreateCollaborationRequestBodyAccessibleByTypeField.User) { Id = slackAutomationUserId }, role: CreateCollaborationRequestBodyRoleField.CoOwner));
            IntegrationMappings slackIntegrations = await userClient.IntegrationMappings.GetSlackIntegrationMappingAsync();
            if (NullableUtils.Unwrap(slackIntegrations.Entries).Count == 0) {
                await userClient.IntegrationMappings.CreateSlackIntegrationMappingAsync(requestBody: new IntegrationMappingSlackCreateRequest(partnerItem: new IntegrationMappingPartnerItemSlack(id: slackPartnerItemId) { SlackOrgId = slackOrgId }, boxItem: new IntegrationMappingBoxItemSlack(id: folder.Id)));
            }
            IntegrationMappings slackMappings = await userClient.IntegrationMappings.GetSlackIntegrationMappingAsync();
            Assert.IsTrue(NullableUtils.Unwrap(slackMappings.Entries).Count >= 1);
            IntegrationMapping slackIntegrationMapping = NullableUtils.Unwrap(slackMappings.Entries)[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(slackIntegrationMapping.IntegrationType) == "slack");
            Assert.IsTrue(StringUtils.ToStringRepresentation(slackIntegrationMapping.Type?.Value) == "integration_mapping");
            Assert.IsTrue(StringUtils.ToStringRepresentation(slackIntegrationMapping.BoxItem.Type?.Value) == "folder");
            IntegrationMapping updatedSlackMapping = await userClient.IntegrationMappings.UpdateSlackIntegrationMappingByIdAsync(integrationMappingId: slackIntegrationMapping.Id, requestBody: new UpdateSlackIntegrationMappingByIdRequestBody() { BoxItem = new IntegrationMappingBoxItemSlack(id: folder.Id) });
            Assert.IsTrue(StringUtils.ToStringRepresentation(updatedSlackMapping.BoxItem.Type?.Value) == "folder");
            Assert.IsTrue(updatedSlackMapping.BoxItem.Id == folder.Id);
            if (NullableUtils.Unwrap(slackMappings.Entries).Count > 2) {
                await userClient.IntegrationMappings.DeleteSlackIntegrationMappingByIdAsync(integrationMappingId: slackIntegrationMapping.Id);
            }
            await userClient.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task TestTeamsIntegrationMappings() {
            FolderFull folder = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: Utils.GetUUID(), parent: new CreateFolderRequestBodyParentField(id: "0")));
            const string tenantId = "1";
            const string teamId = "2";
            const string partnerItemId = "3";
            string userId = Utils.GetEnvVar(name: "USER_ID");
            BoxClient userClient = new CommonsManager().GetDefaultClientWithUserSubject(userId: userId);
            await Assert.That.IsExceptionAsync(async() => await userClient.IntegrationMappings.CreateTeamsIntegrationMappingAsync(requestBody: new IntegrationMappingTeamsCreateRequest(partnerItem: new IntegrationMappingPartnerItemTeamsCreateRequest(type: IntegrationMappingPartnerItemTeamsCreateRequestTypeField.Channel, id: partnerItemId, tenantId: tenantId, teamId: teamId), boxItem: new FolderReference(id: folder.Id))));
            await Assert.That.IsExceptionAsync(async() => await userClient.IntegrationMappings.GetTeamsIntegrationMappingAsync());
            const string integrationMappingId = "123456";
            await Assert.That.IsExceptionAsync(async() => await userClient.IntegrationMappings.UpdateTeamsIntegrationMappingByIdAsync(integrationMappingId: integrationMappingId, requestBody: new UpdateTeamsIntegrationMappingByIdRequestBody() { BoxItem = new FolderReference(id: "1234567") }));
            await Assert.That.IsExceptionAsync(async() => await userClient.IntegrationMappings.DeleteTeamsIntegrationMappingByIdAsync(integrationMappingId: integrationMappingId));
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

    }
}