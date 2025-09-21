using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class WebhooksManager : IWebhooksManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public WebhooksManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
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
        public async System.Threading.Tasks.Task<Webhooks> GetWebhooksAsync(GetWebhooksQueryParams? queryParams = default, GetWebhooksHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetWebhooksQueryParams();
            headers = headers ?? new GetWebhooksHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/webhooks"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Webhooks>(NullableUtils.Unwrap(response.Data));
        }

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
        public async System.Threading.Tasks.Task<Webhook> CreateWebhookAsync(CreateWebhookRequestBody requestBody, CreateWebhookHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateWebhookHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/webhooks"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Webhook>(NullableUtils.Unwrap(response.Data));
        }

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
        public async System.Threading.Tasks.Task<Webhook> GetWebhookByIdAsync(string webhookId, GetWebhookByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetWebhookByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/webhooks/", StringUtils.ToStringRepresentation(webhookId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Webhook>(NullableUtils.Unwrap(response.Data));
        }

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
        public async System.Threading.Tasks.Task<Webhook> UpdateWebhookByIdAsync(string webhookId, UpdateWebhookByIdRequestBody? requestBody = default, UpdateWebhookByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new UpdateWebhookByIdRequestBody();
            headers = headers ?? new UpdateWebhookByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/webhooks/", StringUtils.ToStringRepresentation(webhookId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Webhook>(NullableUtils.Unwrap(response.Data));
        }

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
        public async System.Threading.Tasks.Task DeleteWebhookByIdAsync(string webhookId, DeleteWebhookByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteWebhookByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/webhooks/", StringUtils.ToStringRepresentation(webhookId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}