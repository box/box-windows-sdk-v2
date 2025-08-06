using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IWebhooksManager {
        /// <summary>
    /// Returns all defined webhooks for the requesting application.
    /// 
    /// This API only returns webhooks that are applied to files or folders that are
    /// owned by the authenticated user. This means that an admin can not see webhooks
    /// created by a service account unless the admin has access to those folders, and
    /// vice versa.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getWebhooks method
    /// </param>
    /// <param name="headers">
    /// Headers of getWebhooks method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Webhooks> GetWebhooksAsync(GetWebhooksQueryParams? queryParams = default, GetWebhooksHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates a webhook.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createWebhook method
    /// </param>
    /// <param name="headers">
    /// Headers of createWebhook method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Webhook> CreateWebhookAsync(CreateWebhookRequestBody requestBody, CreateWebhookHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves a specific webhook.
    /// </summary>
    /// <param name="webhookId">
    /// The ID of the webhook.
    /// Example: "3321123"
    /// </param>
    /// <param name="headers">
    /// Headers of getWebhookById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Webhook> GetWebhookByIdAsync(string webhookId, GetWebhookByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates a webhook.
    /// </summary>
    /// <param name="webhookId">
    /// The ID of the webhook.
    /// Example: "3321123"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateWebhookById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateWebhookById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Webhook> UpdateWebhookByIdAsync(string webhookId, UpdateWebhookByIdRequestBody? requestBody = default, UpdateWebhookByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes a webhook.
    /// </summary>
    /// <param name="webhookId">
    /// The ID of the webhook.
    /// Example: "3321123"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteWebhookById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteWebhookByIdAsync(string webhookId, DeleteWebhookByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}