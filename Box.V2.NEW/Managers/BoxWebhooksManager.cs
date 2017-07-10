using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Models;
using Box.V2.Services;
using Box.V2.Extensions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System;
using System.Linq;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents the webhooks V2 endpoints.
    /// </summary>
    public class BoxWebhooksManager : BoxResourceManager
    {
        public BoxWebhooksManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }


        /// <summary>
        /// Create a new webhook.
        /// </summary>
        /// <param name="webhookRequest">BoxWebhookRequest object.</param>
        /// <returns>Returns a webhook object if creation succeeds.</returns>
        public async Task<BoxWebhook> CreateWebhookAsync(BoxWebhookRequest webhookRequest)
        {
            BoxRequest request = new BoxRequest(_config.WebhooksUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize<BoxWebhookRequest>(webhookRequest));

            IBoxResponse<BoxWebhook> response = await ToResponseAsync<BoxWebhook>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Get a webhook.
        /// </summary>
        /// <param name="id">Webhook id.</param>
        /// <returns>Returns a webhook object.</returns>
        public async Task<BoxWebhook> GetWebhookAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.WebhooksUri, id);

            IBoxResponse<BoxWebhook> response = await ToResponseAsync<BoxWebhook>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Update a webhook.
        /// </summary>
        /// <param name="webhookRequest">BoxWebhookRequest object.</param>
        /// <returns>Returns the updated webhook object.</returns>
        public async Task<BoxWebhook> UpdateWebhookAsync(BoxWebhookRequest webhookRequest)
        {
            webhookRequest.ThrowIfNull("webhookRequest")
                .Id.ThrowIfNullOrWhiteSpace("webhookRequest.Id");

            BoxRequest request = new BoxRequest(_config.WebhooksUri, webhookRequest.Id)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize<BoxWebhookRequest>(webhookRequest));

            IBoxResponse<BoxWebhook> response = await ToResponseAsync<BoxWebhook>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Delete a webhook.
        /// </summary>
        /// <param name="id">Webhook id.</param>
        /// <returns>Returns true if deleted successfully.</returns>
        public async Task<bool> DeleteWebhookAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.WebhooksUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxWebhook> response = await ToResponseAsync<BoxWebhook>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Fetch all defined webhooks for the requesting application and user.
        /// </summary>
        /// <param name="limit">Optional. Defaults to 100, max of 200.</param>
        /// <param name="nextMarker">Optional. Used to indicate starting point for next batch of webhooks.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns all defined webhooks for the requesting application and user, up to the limit.</returns>
        public async Task<BoxCollectionMarkerBased<BoxWebhook>> GetWebhooksAsync (int limit = 100, string nextMarker = null, bool autoPaginate=false)
        {
            BoxRequest request = new BoxRequest(_config.WebhooksUri)
                .Param("limit", limit.ToString())
                .Param("marker", nextMarker);

            if (autoPaginate)
            {
                return await AutoPaginateMarker<BoxWebhook>(request, limit);
            }
            else
            {
                IBoxResponse<BoxCollectionMarkerBased<BoxWebhook>> response = await ToResponseAsync<BoxCollectionMarkerBased<BoxWebhook>>(request).ConfigureAwait(false);
                return response.ResponseObject;
            }
        }

        /// <summary>
        /// Convenience method to automatically fetch all webhooks for the requesting application and user using auto-pagination.
        /// </summary>
        /// <returns>Returns all defined webhooks for the requesting application and user.</returns>
        [Obsolete("This method is deprecated. Use GetWebhooksAsync and specify autoPaginate=true instead.",true)]
        public async Task<List<BoxWebhook>> GetAllWebhooksAsync()
        {
            string nextMarker = null;
            var webhooks = new List<BoxWebhook>();

            do
            {
                var nextWebhooks = await GetWebhooksAsync(limit: 200, nextMarker: nextMarker);
                webhooks.AddRange(nextWebhooks.Entries);
                nextMarker = nextWebhooks.NextMarker;

            } while (!string.IsNullOrWhiteSpace(nextMarker));

            return webhooks;
        }

        /// <summary>
        /// Used to validate an incoming webhook by computing cryptographic digests of the notification's payload and comparing them
        /// to the digests computed by Box and placed in the BOX-SIGNATURE-PRIMARY and BOX-SIGNATURE-SECONDARY request headers.
        /// 
        /// For more information about validating webhooks see  <see cref="https://docs.box.com/reference#signatures"/>
        /// </summary>
        /// <param name="deliveryTimestamp">Value in BOX-DELIVERY-TIMESTAMP header.</param>
        /// <param name="signaturePrimary">Value in BOX-SIGNATURE-PRIMARY header.</param>
        /// <param name="signatureSecondary">Value in BOX-SIGNATURE-SECONDARY header.</param>
        /// <param name="payload">Body of the incoming webhook request.</param>
        /// <param name="primaryWebhookKey">Primary webhook signature key.</param>
        /// <param name="secondaryWebhookKey">Secondary webhook signature key.</param>
        /// <returns>Returns true if at least one of the two webhook signatures match the computed signature.</returns>
        public static bool VerifyWebhook(string deliveryTimestamp, string signaturePrimary, string signatureSecondary, string payload, 
                                         string primaryWebhookKey, string secondaryWebhookKey)
        {
            var primaryKeyBytes = Encoding.UTF8.GetBytes(primaryWebhookKey);
            var secondaryKeyBytes = Encoding.UTF8.GetBytes(secondaryWebhookKey);
            var bodyBytes = Encoding.UTF8.GetBytes(payload);
            var allBytes = bodyBytes.Concat(Encoding.UTF8.GetBytes(deliveryTimestamp)).ToArray();
            using (var hmacsha256Primary = new HMACSHA256(primaryKeyBytes))
            using (var hmacsha256Secondary = new HMACSHA256(secondaryKeyBytes))
            {
                byte[] hashBytes = hmacsha256Primary.ComputeHash(allBytes);
                var hashPrimary = Convert.ToBase64String(hashBytes);

                hashBytes = hmacsha256Secondary.ComputeHash(allBytes);
                var hashSecondary = Convert.ToBase64String(hashBytes);

                if (hashPrimary != signaturePrimary && hashSecondary != signatureSecondary)
                {
                    return false;
                }
            }

            return true;

        }
    }
}
