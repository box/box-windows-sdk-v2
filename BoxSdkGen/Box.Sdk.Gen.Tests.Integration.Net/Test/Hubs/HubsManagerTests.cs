using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class HubsManagerTests {
        public BoxClient client { get; }

        public HubsManagerTests() {
            client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateUpdateGetAndDeleteHubs() {
            string hubTitle = Utils.GetUUID();
            const string hubDescription = "new Hub description";
            HubV2025R0 createdHub = await client.Hubs.CreateHubV2025R0Async(requestBody: new HubCreateRequestV2025R0(title: hubTitle) { Description = hubDescription });
            Assert.IsTrue(NullableUtils.Unwrap(createdHub.Title) == hubTitle);
            Assert.IsTrue(NullableUtils.Unwrap(createdHub.Description) == hubDescription);
            Assert.IsTrue(StringUtils.ToStringRepresentation(createdHub.Type?.Value) == "hubs");
            string hubId = createdHub.Id;
            HubsV2025R0 usersHubs = await client.Hubs.GetHubsV2025R0Async(queryParams: new GetHubsV2025R0QueryParams() { Scope = "all", Sort = "name", Direction = GetHubsV2025R0QueryParamsDirectionField.Asc });
            Assert.IsTrue(NullableUtils.Unwrap(usersHubs.Entries).Count > 0);
            HubsV2025R0 enterpriseHubs = await client.Hubs.GetEnterpriseHubsV2025R0Async(queryParams: new GetEnterpriseHubsV2025R0QueryParams() { Sort = "name", Direction = GetEnterpriseHubsV2025R0QueryParamsDirectionField.Asc });
            Assert.IsTrue(NullableUtils.Unwrap(enterpriseHubs.Entries).Count > 0);
            HubV2025R0 hubById = await client.Hubs.GetHubByIdV2025R0Async(hubId: hubId);
            Assert.IsTrue(hubById.Id == hubId);
            Assert.IsTrue(NullableUtils.Unwrap(hubById.Title) == hubTitle);
            Assert.IsTrue(NullableUtils.Unwrap(hubById.Description) == hubDescription);
            Assert.IsTrue(StringUtils.ToStringRepresentation(hubById.Type?.Value) == "hubs");
            string newHubTitle = Utils.GetUUID();
            const string newHubDescription = "updated Hub description";
            HubV2025R0 updatedHub = await client.Hubs.UpdateHubByIdV2025R0Async(hubId: hubId, requestBody: new HubUpdateRequestV2025R0() { Title = newHubTitle, Description = newHubDescription });
            Assert.IsTrue(NullableUtils.Unwrap(updatedHub.Title) == newHubTitle);
            Assert.IsTrue(NullableUtils.Unwrap(updatedHub.Description) == newHubDescription);
            await client.Hubs.DeleteHubByIdV2025R0Async(hubId: hubId);
            await Assert.That.IsExceptionAsync(async() => await client.Hubs.DeleteHubByIdV2025R0Async(hubId: hubId));
        }

        [RetryableTest]
        public async System.Threading.Tasks.Task CopyHub() {
            string hubTitle = Utils.GetUUID();
            const string hubDescription = "new Hub description";
            HubV2025R0 createdHub = await client.Hubs.CreateHubV2025R0Async(requestBody: new HubCreateRequestV2025R0(title: hubTitle) { Description = hubDescription });
            string copiedHubTitle = Utils.GetUUID();
            const string copiedHubDescription = "copied Hub description";
            HubV2025R0 copiedHub = await client.Hubs.CopyHubV2025R0Async(hubId: createdHub.Id, requestBody: new HubCopyRequestV2025R0() { Title = copiedHubTitle, Description = copiedHubDescription });
            Assert.IsTrue(copiedHub.Id != createdHub.Id);
            Assert.IsTrue(NullableUtils.Unwrap(copiedHub.Title) == copiedHubTitle);
            Assert.IsTrue(NullableUtils.Unwrap(copiedHub.Description) == copiedHubDescription);
            await client.Hubs.DeleteHubByIdV2025R0Async(hubId: createdHub.Id);
            await client.Hubs.DeleteHubByIdV2025R0Async(hubId: copiedHub.Id);
        }

    }
}