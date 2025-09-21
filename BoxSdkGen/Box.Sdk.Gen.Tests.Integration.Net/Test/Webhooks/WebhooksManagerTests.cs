using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen.Tests.Integration {
    [TestClass]
    public class WebhooksManagerTests {
        public BoxClient client { get; }

        public WebhooksManagerTests() {
            client = new CommonsManager().GetDefaultClient();
        }
        [RetryableTest]
        public async System.Threading.Tasks.Task TestWebhooksCrud() {
            FolderFull folder = await client.Folders.CreateFolderAsync(requestBody: new CreateFolderRequestBody(name: Utils.GetUUID(), parent: new CreateFolderRequestBodyParentField(id: "0")));
            Webhook webhook = await client.Webhooks.CreateWebhookAsync(requestBody: new CreateWebhookRequestBody(target: new CreateWebhookRequestBodyTargetField() { Id = folder.Id, Type = CreateWebhookRequestBodyTargetTypeField.Folder }, address: "https://example.com/new-webhook", triggers: Array.AsReadOnly(new [] {new StringEnum<CreateWebhookRequestBodyTriggersField>(CreateWebhookRequestBodyTriggersField.FileUploaded)})));
            Assert.IsTrue(NullableUtils.Unwrap(webhook.Target).Id == folder.Id);
            Assert.IsTrue(StringUtils.ToStringRepresentation(NullableUtils.Unwrap(webhook.Target).Type) == "folder");
            Assert.IsTrue(NullableUtils.Unwrap(webhook.Triggers).Count == Array.AsReadOnly(new [] {"FILE.UPLOADED"}).Count);
            Assert.IsTrue(webhook.Address == "https://example.com/new-webhook");
            Webhooks webhooks = await client.Webhooks.GetWebhooksAsync();
            Assert.IsTrue(NullableUtils.Unwrap(webhooks.Entries).Count > 0);
            Webhook webhookFromApi = await client.Webhooks.GetWebhookByIdAsync(webhookId: NullableUtils.Unwrap(webhook.Id));
            Assert.IsTrue(webhook.Id == webhookFromApi.Id);
            Assert.IsTrue(NullableUtils.Unwrap(webhook.Target).Id == NullableUtils.Unwrap(webhookFromApi.Target).Id);
            Assert.IsTrue(webhook.Address == webhookFromApi.Address);
            Webhook updatedWebhook = await client.Webhooks.UpdateWebhookByIdAsync(webhookId: NullableUtils.Unwrap(webhook.Id), requestBody: new UpdateWebhookByIdRequestBody() { Address = "https://example.com/updated-webhook" });
            Assert.IsTrue(updatedWebhook.Id == webhook.Id);
            Assert.IsTrue(updatedWebhook.Address == "https://example.com/updated-webhook");
            await client.Webhooks.DeleteWebhookByIdAsync(webhookId: NullableUtils.Unwrap(webhook.Id));
            await Assert.That.IsExceptionAsync(async() => await client.Webhooks.DeleteWebhookByIdAsync(webhookId: NullableUtils.Unwrap(webhook.Id)));
            await client.Folders.DeleteFolderByIdAsync(folderId: folder.Id);
        }

    }
}