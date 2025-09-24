using System.Threading.Tasks;
using Box.V2.Models;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents the webhooks V2 endpoints.
    /// </summary>
    public interface IBoxWebhooksManager
    {
        /// <summary>
        /// Create a new webhook.
        /// </summary>
        /// <param name="webhookRequest">BoxWebhookRequest object.</param>
        /// <returns>Returns a webhook object if creation succeeds.</returns>
        Task<BoxWebhook> CreateWebhookAsync(BoxWebhookRequest webhookRequest);

        /// <summary>
        /// Get a webhook.
        /// </summary>
        /// <param name="id">Webhook id.</param>
        /// <returns>Returns a webhook object.</returns>
        Task<BoxWebhook> GetWebhookAsync(string id);

        /// <summary>
        /// Update a webhook.
        /// </summary>
        /// <param name="webhookRequest">BoxWebhookRequest object.</param>
        /// <returns>Returns the updated webhook object.</returns>
        Task<BoxWebhook> UpdateWebhookAsync(BoxWebhookRequest webhookRequest);

        /// <summary>
        /// Delete a webhook.
        /// </summary>
        /// <param name="id">Webhook id.</param>
        /// <returns>Returns true if deleted successfully.</returns>
        Task<bool> DeleteWebhookAsync(string id);

        /// <summary>
        /// Fetch all defined webhooks for the requesting application and user.
        /// </summary>
        /// <param name="limit">Optional. Defaults to 100, max of 200.</param>
        /// <param name="nextMarker">Optional. Used to indicate starting point for next batch of webhooks.</param>
        /// <param name="autoPaginate">Whether or not to auto-paginate to fetch all items; defaults to false.</param>
        /// <returns>Returns all defined webhooks for the requesting application and user, up to the limit.</returns>
        Task<BoxCollectionMarkerBased<BoxWebhook>> GetWebhooksAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false);
    }
}
