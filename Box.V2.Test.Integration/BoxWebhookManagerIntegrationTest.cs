using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Box.V2.Exceptions;
using Box.V2.Models;
using Box.V2.Test.Integration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxWebhookManagerIntegrationTest : TestInFolder
    {
        [TestMethod]
        public async Task CreateWebhookAsync_ForCorrectWebhookRequest_ShouldCreateNewWebhook()
        {
            var webhookRequest = new BoxWebhookRequest()
            {
                Target = new BoxRequestEntity()
                {
                    Id = FolderId,
                    Type = BoxType.folder
                },
                Address = "https://example.com/webhook",
                Triggers = new List<string>()
                {
                    "FILE.UPLOADED"
                }
            };
            var webhook = await UserClient.WebhooksManager.CreateWebhookAsync(webhookRequest);
            Assert.IsNotNull(webhook.Id);
            Assert.AreEqual(webhookRequest.Address, webhook.Address);
            Assert.AreEqual(webhookRequest.Triggers[0], webhook.Triggers[0]);
            Assert.AreEqual(webhookRequest.Target.Id, webhook.Target.Id);

            await UserClient.WebhooksManager.DeleteWebhookAsync(webhook.Id);
        }

        [TestMethod]
        public async Task GetWebhookAsync_ForExistingWebhook_ShouldReturnWebhook()
        {
            var webhook = await CreateWebhook();

            var fetchedWebhook = await UserClient.WebhooksManager.GetWebhookAsync(webhook.Id);

            Assert.AreEqual(webhook.Id, fetchedWebhook.Id);
        }

        [TestMethod]
        public async Task UpdateWebhookAsync_ForExistingWebhook_ShouldUpdateWebhook()
        {
            var webhook = await CreateWebhook();

            var newUrl = "https://example2.com/webhook";
            var updateWebhookRequest = new BoxWebhookRequest() { Id = webhook.Id, Address = newUrl };

            var updatedWebhook = await UserClient.WebhooksManager.UpdateWebhookAsync(updateWebhookRequest);

            Assert.AreEqual(updatedWebhook.Address, newUrl);
        }

        [TestMethod]
        public async Task DeleteWebhookAsync_ForExistingWebhook_ShouldDeleteWebhookAndExceptionShouldBeThrown()
        {
            var webhookRequest = new BoxWebhookRequest()
            {
                Target = new BoxRequestEntity()
                {
                    Id = FolderId,
                    Type = BoxType.folder
                },
                Address = "https://example.com/webhook",
                Triggers = new List<string>()
                {
                    "FILE.UPLOADED"
                }
            };
            var webhook = await UserClient.WebhooksManager.CreateWebhookAsync(webhookRequest);

            var result = await UserClient.WebhooksManager.DeleteWebhookAsync(webhook.Id);

            await Assert.ThrowsExceptionAsync<BoxAPIException>(async () => { _ = await UserClient.WebhooksManager.GetWebhookAsync(webhook.Id); });
        }

        [TestMethod]
        public async Task AddWebhook_ForSignRequest_ShouldCreateSuccess()
        {
            var signRequest = await CreateSignRequest("sdk_integration_test@boxdemo.com", FolderId);
            var signFileId = signRequest.SignFiles.Files[0].Id;
            var webhookRequest = new BoxWebhookRequest()
            {
                Target = new BoxRequestEntity()
                {
                    Id = signFileId,
                    Type = BoxType.file
                },
                Address = "https://example.com/webhook",
                Triggers = new List<string>()
                {
                    "SIGN_REQUEST.COMPLETED",
                    "SIGN_REQUEST.DECLINED",
                    "SIGN_REQUEST.EXPIRED"
                }
            };
            var webhook = await UserClient.WebhooksManager.CreateWebhookAsync(webhookRequest);
            Assert.IsNotNull(webhook.Id);
            await UserClient.WebhooksManager.DeleteWebhookAsync(webhook.Id);
        }
    }
}
