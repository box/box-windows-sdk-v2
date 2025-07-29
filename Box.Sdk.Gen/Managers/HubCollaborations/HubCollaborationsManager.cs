using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class HubCollaborationsManager : IHubCollaborationsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public HubCollaborationsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves all collaborations for a hub.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getHubCollaborationsV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getHubCollaborationsV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubCollaborationsV2025R0> GetHubCollaborationsV2025R0Async(GetHubCollaborationsV2025R0QueryParams queryParams, GetHubCollaborationsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetHubCollaborationsV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "hub_id", StringUtils.ToStringRepresentation(queryParams.HubId) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hub_collaborations"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubCollaborationsV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Adds a collaboration for a single user or a single group to a hub.
        /// 
        /// Collaborations can be created using email address, user IDs, or group IDs.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createHubCollaborationV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of createHubCollaborationV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubCollaborationV2025R0> CreateHubCollaborationV2025R0Async(HubCollaborationCreateRequestV2025R0 requestBody, CreateHubCollaborationV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateHubCollaborationV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hub_collaborations"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubCollaborationV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieves details for a hub collaboration by collaboration ID.
        /// </summary>
        /// <param name="hubCollaborationId">
        /// The ID of the hub collaboration.
        /// Example: "1234"
        /// </param>
        /// <param name="headers">
        /// Headers of getHubCollaborationByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubCollaborationV2025R0> GetHubCollaborationByIdV2025R0Async(string hubCollaborationId, GetHubCollaborationByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetHubCollaborationByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hub_collaborations/", StringUtils.ToStringRepresentation(hubCollaborationId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubCollaborationV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a hub collaboration.
        /// Can be used to change the hub role.
        /// </summary>
        /// <param name="hubCollaborationId">
        /// The ID of the hub collaboration.
        /// Example: "1234"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateHubCollaborationByIdV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of updateHubCollaborationByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubCollaborationV2025R0> UpdateHubCollaborationByIdV2025R0Async(string hubCollaborationId, HubCollaborationUpdateRequestV2025R0 requestBody, UpdateHubCollaborationByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new UpdateHubCollaborationByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hub_collaborations/", StringUtils.ToStringRepresentation(hubCollaborationId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubCollaborationV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes a single hub collaboration.
        /// </summary>
        /// <param name="hubCollaborationId">
        /// The ID of the hub collaboration.
        /// Example: "1234"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteHubCollaborationByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteHubCollaborationByIdV2025R0Async(string hubCollaborationId, DeleteHubCollaborationByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteHubCollaborationByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hub_collaborations/", StringUtils.ToStringRepresentation(hubCollaborationId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}