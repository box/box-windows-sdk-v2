using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class HubsManager : IHubsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public HubsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves all Box Hubs for requesting user.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getHubsV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getHubsV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubsV2025R0> GetHubsV2025R0Async(GetHubsV2025R0QueryParams? queryParams = default, GetHubsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetHubsV2025R0QueryParams();
            headers = headers ?? new GetHubsV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "query", StringUtils.ToStringRepresentation(queryParams.Query) }, { "scope", StringUtils.ToStringRepresentation(queryParams.Scope) }, { "sort", StringUtils.ToStringRepresentation(queryParams.Sort) }, { "direction", StringUtils.ToStringRepresentation(queryParams.Direction?.Value) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hubs"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubsV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a new Box Hub.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createHubV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of createHubV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubV2025R0> CreateHubV2025R0Async(HubCreateRequestV2025R0 requestBody, CreateHubV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateHubV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hubs"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieves all Box Hubs for a given enterprise.
        /// 
        /// Admins or Hub Co-admins of an enterprise
        /// with GCM scope can make this call.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getEnterpriseHubsV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getEnterpriseHubsV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubsV2025R0> GetEnterpriseHubsV2025R0Async(GetEnterpriseHubsV2025R0QueryParams? queryParams = default, GetEnterpriseHubsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetEnterpriseHubsV2025R0QueryParams();
            headers = headers ?? new GetEnterpriseHubsV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "query", StringUtils.ToStringRepresentation(queryParams.Query) }, { "sort", StringUtils.ToStringRepresentation(queryParams.Sort) }, { "direction", StringUtils.ToStringRepresentation(queryParams.Direction?.Value) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/enterprise_hubs"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubsV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieves details for a Box Hub by its ID.
        /// </summary>
        /// <param name="hubId">
        /// The unique identifier that represent a hub.
        /// 
        /// The ID for any hub can be determined
        /// by visiting this hub in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/hubs/123`
        /// the `hub_id` is `123`.
        /// Example: "12345"
        /// </param>
        /// <param name="headers">
        /// Headers of getHubByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubV2025R0> GetHubByIdV2025R0Async(string hubId, GetHubByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetHubByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hubs/", StringUtils.ToStringRepresentation(hubId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a Box Hub. Can be used to change title, description, or Box Hub settings.
        /// </summary>
        /// <param name="hubId">
        /// The unique identifier that represent a hub.
        /// 
        /// The ID for any hub can be determined
        /// by visiting this hub in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/hubs/123`
        /// the `hub_id` is `123`.
        /// Example: "12345"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateHubByIdV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of updateHubByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubV2025R0> UpdateHubByIdV2025R0Async(string hubId, HubUpdateRequestV2025R0 requestBody, UpdateHubByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new UpdateHubByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hubs/", StringUtils.ToStringRepresentation(hubId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes a single Box Hub.
        /// </summary>
        /// <param name="hubId">
        /// The unique identifier that represent a hub.
        /// 
        /// The ID for any hub can be determined
        /// by visiting this hub in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/hubs/123`
        /// the `hub_id` is `123`.
        /// Example: "12345"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteHubByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteHubByIdV2025R0Async(string hubId, DeleteHubByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteHubByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hubs/", StringUtils.ToStringRepresentation(hubId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a copy of a Box Hub.
        /// 
        /// The original Box Hub will not be modified.
        /// </summary>
        /// <param name="hubId">
        /// The unique identifier that represent a hub.
        /// 
        /// The ID for any hub can be determined
        /// by visiting this hub in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/hubs/123`
        /// the `hub_id` is `123`.
        /// Example: "12345"
        /// </param>
        /// <param name="requestBody">
        /// Request body of copyHubV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of copyHubV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubV2025R0> CopyHubV2025R0Async(string hubId, HubCopyRequestV2025R0 requestBody, CopyHubV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CopyHubV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hubs/", StringUtils.ToStringRepresentation(hubId), "/copy"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubV2025R0>(NullableUtils.Unwrap(response.Data));
        }

    }
}