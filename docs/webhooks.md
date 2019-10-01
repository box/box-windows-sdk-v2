Webhooks
========

A webhook object enables you to attach events triggers to Box files and folders. These
event triggers monitor events on Box objects and notify your application, via HTTP
requests to a URL of your choosing, when they occur.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Create a Webhook](#create-a-webhook)
- [Get All Webhooks](#get-all-webhooks)
- [Get a Webhook"s Information](#get-a-webhooks-information)
- [Validate a Webhook Message](#validate-a-webhook-message)
- [Delete a Webhook](#delete-a-webhook)
- [Update a Webhook](#update-a-webhook)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Create a Webhook
----------------

To attach a webhook to an item, call the
`WebhooksManager.CreateWebhookAsync(BoxWebhookRequest webhookRequest)`
method with the type and ID of the item, a URL to send notifications to, and a list
of triggers.


<!-- sample post_webhooks -->
```c#
var webhookParams = new BoxWebhookRequest()
{
    Target = new BoxRequestEntity()
    {
        Type = BoxType.file,
        Id = "22222"
    },
    Triggers = new List<string>()
    {
        "FILE.PREVIEWED"
    },
    Address = "https://example.com/webhook"
};
BoxWebhook webhook = await client.WebhooksManager.CreateWebhookAsync(webhookParams);
```

Similarly, webhooks can be created for folders.

<!-- sample post_webhooks for_folder -->
```c#
var webhookParams = new BoxWebhookRequest()
{
    Target = new BoxRequestEntity()
    {
        Type = BoxType.folder,
        Id = "22222"
    },
    Triggers = new List<string>()
    {
        "FILE.UPLOADED",
        "FILE.DOWNLOADED"
    },
    Address = "https://example.com/webhook
};
BoxWebhook webhook = await client.WebhooksManager.CreateWebhookAsync(webhookParams);
```

Get All Webhooks
----------------

Get a list of all webhooks for the requesting application and user by calling the
`WebhooksManager.GetWebhooksAsync (int limit = 100, string nextMarker = null, bool autoPaginate=false)`
method.  The maximum limit per page of results is 200, Box uses the default limit of 100.

<!-- sample get_webhooks -->
```c#
BoxCollectionMarkerBased<BoxWebhook> webhooks = await client.WebhooksManager.GetWebhooksAsync();
```

Get a Webhook"s Information
---------------------------

Retrieve information about a specific webhook by calling `WebhooksManager.GetWebhookAsync(string id)`
to retrieve a webhook by ID.

<!-- sample get_webhooks_id -->
```c#
BoxWebhook webhook = await client.WebhooksManager.GetWebhookAsync("12345");
```

Validate a Webhook Message
--------------------------

When you receive a webhook message from Box, you should validate it by calling
the static `WebhooksManager.VerifyWebhook(string deliveryTimestamp, string signaturePrimary, string signatureSecondary, string payload, string primaryWebhookKey, string secondaryWebhookKey)`
method with the components of the webhook message.

<!-- sample x_webhooks validate_signatures -->
```c#
using Box.V2.Managers;

var body = "{\"type\":\"webhook_event\",\"webhook\":{\"id\":\"1234567890\"},\"trigger\":\"FILE.UPLOADED\",\"source\":{\"id\":\"1234567890\",\"type\":\"file\",\"name\":\"Test.txt\"}}";
var headers = new Dictionary<string, string>()
{
    { "box-delivery-id", "f96bb54b-ee16-4fc5-aa65-8c2d9e5b546f" },
    { "box-delivery-timestamp", "2020-01-01T00:00:00-07:00" },
    { "box-signature-algorithm", "HmacSHA256" } ,
    { "box-signature-primary", "6TfeAW3A1PASkgboxxA5yqHNKOwFyMWuEXny/FPD5hI=" },
    { "box-signature-secondary", "v+1CD1Jdo3muIcbpv5lxxgPglOqMfsNHPV899xWYydo=" },
    { "box-signature-version", "1" }
};
var primaryKey = "Fd28OJrZ8oNxkgmS7TbjXNgrG8v";
var secondaryKey = "KWkROAOiof4zhYUHbAmiVn63cMj"

bool isValid = BoxWebhooksManager.VerifyWebhook(
    deliveryTimestamp: headers["box-delivery-timestamp"],
    signaturePrimary: headers["box-signature-primary"],
    signatureSecondary: headers["box-signature-secondary"],
    payload: body,
    primaryWebhookKey: primaryKey,
    secondaryWebhookKey: secondaryKey
);
```

Delete a Webhook
----------------

A file or folder's webhook can be removed by calling `WebhooksManager.DeleteWebhookAsync(string id)`
with the ID of the webhook object.

<!-- sample delete_webhooks_id -->
```c#
await client.WebhooksManager.DeleteWebhookAsync("11111");
```

Update a Webhook
----------------

Update a file or folder's webhook by calling `WebhooksManager.UpdateWebhookAsync(BoxWebhookRequest webhookRequest)`
with the fields of the webhook object to update.

<!-- sample put_webhooks_id -->
```c#
var updates = new BoxWebhookRequest()
{
    Id = "12345",
    Address = "https://example.com/webhooks/fileActions
};
BoxWebhook updatedWebhook = await client.WebhooksManager.UpdateWebhookAsync(updates);
```
