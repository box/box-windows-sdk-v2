using Box.V2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Box.V2.Test.Integration
{
    [TestClass]
    public class BoxWebhooksManagerTestIntegration : BoxResourceManagerTestIntegration
    {
        [TestMethod]
        public async Task WebhookTests_LiveSession()
        {
            const string TRIGGER1 = "FILE.PREVIEWED";
            const string TRIGGER2 = "FILE.DOWNLOADED";
            const string ADDRESS1 = "https://example1.com";
            const string ADDRESS2 = "https://example2.com";

            //first remove any dangling webhooks from previous failed tests
            var existingWebhooks = await _client.WebhooksManager.GetWebhooksAsync(autoPaginate:true);
            foreach (var wh in existingWebhooks.Entries)
            {
                await _client.WebhooksManager.DeleteWebhookAsync(wh.Id);
            }

            //create a new webhook on a file
            BoxRequestEntity target = new BoxRequestEntity() { Id = "16894937051", Type = BoxType.file };
            var triggers = new List<string>() { TRIGGER1 };
            BoxWebhookRequest whr = new BoxWebhookRequest() { Target = target, Address = ADDRESS1, Triggers = triggers };
            var webhook = await _client.WebhooksManager.CreateWebhookAsync(whr);
            Assert.IsNotNull(webhook, "Failed to create webhook");
            Assert.AreEqual(TRIGGER1, webhook.Triggers.First(), "Webhook trigger does not match");
            Assert.AreEqual(ADDRESS1, webhook.Address, "Webhook address does not match");

            //get a webhook
            var fetchedWebhook = await _client.WebhooksManager.GetWebhookAsync(webhook.Id);
            Assert.AreEqual(fetchedWebhook.Id, webhook.Id, "Failed to get webhook");

            //update a webhook
            triggers = new List<string>() { TRIGGER1, TRIGGER2 };
            whr = new BoxWebhookRequest() { Id = webhook.Id, Address = ADDRESS2, Triggers = triggers };
            var updatedWebhook = await _client.WebhooksManager.UpdateWebhookAsync(whr);
            Assert.IsTrue(updatedWebhook.Triggers.Contains(TRIGGER1), "Webhook trigger does not match");
            Assert.IsTrue(updatedWebhook.Triggers.Contains(TRIGGER2), "Webhook trigger does not match");
            Assert.AreEqual(ADDRESS2, updatedWebhook.Address, "Webhook address does not match");

            //delete a webhook
            var result = await _client.WebhooksManager.DeleteWebhookAsync(webhook.Id);
            Assert.IsTrue(result, "Failed to delete webhook");
        }
    }
}
