using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System.Linq;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class HubCollaborationsManagerTests {
        public BoxClient client { get; }

        public HubCollaborationsManagerTests() {
            client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCrudHubCollaboration() {
            HubsV2025R0 hubs = await client.Hubs.GetHubsV2025R0Async(queryParams: new GetHubsV2025R0QueryParams() { Scope = "all", Sort = "name", Direction = GetHubsV2025R0QueryParamsDirectionField.Asc });
            HubV2025R0 hub = NullableUtils.Unwrap(hubs.Entries).ElementAt(0);
            string userName = Utils.GetUUID();
            string userLogin = string.Concat(Utils.GetUUID(), "@gmail.com");
            UserFull user = await client.Users.CreateUserAsync(requestBody: new CreateUserRequestBody(name: userName) { Login = userLogin, IsPlatformAccessOnly = true });
            HubCollaborationV2025R0 createdCollaboration = await client.HubCollaborations.CreateHubCollaborationV2025R0Async(requestBody: new HubCollaborationCreateRequestV2025R0(hub: new HubCollaborationCreateRequestV2025R0HubField(id: hub.Id), accessibleBy: new HubCollaborationCreateRequestV2025R0AccessibleByField(type: "user") { Id = user.Id }, role: "viewer"));
            Assert.IsTrue(createdCollaboration.Id != "");
            Assert.IsTrue(StringUtils.ToStringRepresentation(createdCollaboration.Type?.Value) == "hub_collaboration");
            Assert.IsTrue(NullableUtils.Unwrap(createdCollaboration.Hub).Id == hub.Id);
            Assert.IsTrue(createdCollaboration.Role == "viewer");
            HubCollaborationV2025R0 updatedCollaboration = await client.HubCollaborations.UpdateHubCollaborationByIdV2025R0Async(hubCollaborationId: createdCollaboration.Id, requestBody: new HubCollaborationUpdateRequestV2025R0() { Role = "editor" });
            Assert.IsTrue(updatedCollaboration.Id != "");
            Assert.IsTrue(StringUtils.ToStringRepresentation(updatedCollaboration.Type?.Value) == "hub_collaboration");
            Assert.IsTrue(NullableUtils.Unwrap(updatedCollaboration.Hub).Id == hub.Id);
            Assert.IsTrue(updatedCollaboration.Role == "editor");
            HubCollaborationsV2025R0 collaborations = await client.HubCollaborations.GetHubCollaborationsV2025R0Async(queryParams: new GetHubCollaborationsV2025R0QueryParams(hubId: hub.Id));
            Assert.IsTrue(NullableUtils.Unwrap(collaborations.Entries).Count >= 1);
            HubCollaborationV2025R0 retrievedCollaboration = await client.HubCollaborations.GetHubCollaborationByIdV2025R0Async(hubCollaborationId: createdCollaboration.Id);
            Assert.IsTrue(retrievedCollaboration.Id == createdCollaboration.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(retrievedCollaboration.Type?.Value) == "hub_collaboration");
            Assert.IsTrue(NullableUtils.Unwrap(retrievedCollaboration.Hub).Id == hub.Id);
            Assert.IsTrue(retrievedCollaboration.Role == "editor");
            await client.HubCollaborations.DeleteHubCollaborationByIdV2025R0Async(hubCollaborationId: createdCollaboration.Id);
            await client.Users.DeleteUserByIdAsync(userId: user.Id);
        }

    }
}