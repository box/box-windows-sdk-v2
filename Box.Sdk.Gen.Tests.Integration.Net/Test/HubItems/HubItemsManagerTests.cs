using Microsoft.VisualStudio.TestTools.UnitTesting;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class HubItemsManagerTests {
        public BoxClient client { get; }

        public HubItemsManagerTests() {
            client = new CommonsManager().GetDefaultClientWithUserSubject(userId: Utils.GetEnvVar(name: "USER_ID"));
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestCreateDeleteGetHubItems() {
            string hubTitle = Utils.GetUUID();
            FolderFull folder = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: Utils.GetUUID(), parent: new CreateFolderRequestBodyParentField(id: "0")));
            HubV2025R0 createdHub = await client.Hubs.CreateHubV2025R0Async(requestBody: new HubCreateRequestV2025R0(title: hubTitle));
            HubItemsV2025R0 hubItemsBeforeAdd = await client.HubItems.GetHubItemsV2025R0Async(queryParams: new GetHubItemsV2025R0QueryParams(hubId: createdHub.Id));
            Assert.IsTrue(NullableUtils.Unwrap(hubItemsBeforeAdd.Entries).Count == 0);
            HubItemsManageResponseV2025R0 addedHubItems = await client.HubItems.ManageHubItemsV2025R0Async(hubId: createdHub.Id, requestBody: new HubItemsManageRequestV2025R0() { Operations = Array.AsReadOnly(new [] {new HubItemOperationV2025R0(action: HubItemOperationV2025R0ActionField.Add, item: new FolderReferenceV2025R0(id: folder.Id))}) });
            HubItemOperationResultV2025R0 addedHubItem = addedHubItems.Operations[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(addedHubItem.Action)) == "add");
            Assert.IsTrue(NullableUtils.Unwrap(addedHubItem.Status) == 200);
            HubItemsV2025R0 hubItemsAfterAdd = await client.HubItems.GetHubItemsV2025R0Async(queryParams: new GetHubItemsV2025R0QueryParams(hubId: createdHub.Id));
            Assert.IsTrue(NullableUtils.Unwrap(hubItemsAfterAdd.Entries).Count == 1);
            HubItemsManageResponseV2025R0 removedHubItems = await client.HubItems.ManageHubItemsV2025R0Async(hubId: createdHub.Id, requestBody: new HubItemsManageRequestV2025R0() { Operations = Array.AsReadOnly(new [] {new HubItemOperationV2025R0(action: HubItemOperationV2025R0ActionField.Remove, item: new FolderReferenceV2025R0(id: folder.Id))}) });
            HubItemOperationResultV2025R0 removedHubItem = removedHubItems.Operations[0];
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(removedHubItem.Action)) == "remove");
            Assert.IsTrue(NullableUtils.Unwrap(removedHubItem.Status) == 200);
            HubItemsV2025R0 hubItemsAfterRemove = await client.HubItems.GetHubItemsV2025R0Async(queryParams: new GetHubItemsV2025R0QueryParams(hubId: createdHub.Id));
            Assert.IsTrue(NullableUtils.Unwrap(hubItemsAfterRemove.Entries).Count == 0);
            await client.Hubs.DeleteHubByIdV2025R0Async(hubId: createdHub.Id);
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

    }
}