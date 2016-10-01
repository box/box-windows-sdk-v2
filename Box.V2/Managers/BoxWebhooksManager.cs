using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Services;
using Box.V2.Extensions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Box.V2.Managers
{
    public class BoxWebhooksManager : BoxResourceManager
    {
        public BoxWebhooksManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }


        /// <summary>
        /// Create a new webhook
        /// </summary>
        /// <param name="webhookRequest"></param>
        /// <returns></returns>
        public async Task<BoxWebhook> CreateWebhookAsync(BoxWebhookRequest webhookRequest)
        {
            BoxRequest request = new BoxRequest(_config.WebhooksUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize<BoxWebhookRequest>(webhookRequest));

            IBoxResponse<BoxWebhook> response = await ToResponseAsync<BoxWebhook>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Get a webhook
        /// </summary>
        /// <param name="id">webhook id</param>
        /// <returns></returns>
        public async Task<BoxWebhook> GetWebhookAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.WebhooksUri, id);

            IBoxResponse<BoxWebhook> response = await ToResponseAsync<BoxWebhook>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Update a webhook
        /// </summary>
        /// <param name="webhookRequest"></param>
        /// <returns></returns>
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
        /// Delete a webhook
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteWebhookAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.WebhooksUri, id)
                .Method(RequestMethod.Delete);

            IBoxResponse<BoxWebhook> response = await ToResponseAsync<BoxWebhook>(request).ConfigureAwait(false);

            return response.Status == ResponseStatus.Success;
        }

        /// <summary>
        /// Fetch all defined webhooks for the requesting application and user
        /// </summary>
        /// <param name="limit">Optional. Defaults to 100, max of 200.</param>
        /// <param name="nextMarker">Optional. Used to indicate starting point for next batch of webhooks.</param>
        /// <returns></returns>
        public async Task<BoxWebhookCollection<BoxWebhook>> GetWebhooksAsync (int limit = 100, string nextMarker = null)
        {
            BoxRequest request = new BoxRequest(_config.WebhooksUri)
                .Param("limit", limit.ToString())
                .Param("marker", nextMarker);

            IBoxResponse<BoxWebhookCollection<BoxWebhook>> response = await ToResponseAsync<BoxWebhookCollection<BoxWebhook>>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        /// <summary>
        /// Convenience method to automatically fetch all webhooks for the requesting application and user using auto-pagination.
        /// </summary>
        /// <returns></returns>
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
    }
}
