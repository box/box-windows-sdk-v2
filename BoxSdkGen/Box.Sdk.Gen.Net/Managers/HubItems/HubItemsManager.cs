using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class HubItemsManager : IHubItemsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public HubItemsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves all items associated with a Box Hub.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getHubItemsV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getHubItemsV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubItemsV2025R0> GetHubItemsV2025R0Async(GetHubItemsV2025R0QueryParams queryParams, GetHubItemsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetHubItemsV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "hub_id", StringUtils.ToStringRepresentation(queryParams.HubId) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hub_items"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubItemsV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Adds and/or removes Box Hub items from a Box Hub.
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
        /// Request body of manageHubItemsV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of manageHubItemsV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<HubItemsManageResponseV2025R0> ManageHubItemsV2025R0Async(string hubId, HubItemsManageRequestV2025R0 requestBody, ManageHubItemsV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new ManageHubItemsV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/hubs/", StringUtils.ToStringRepresentation(hubId), "/manage_items"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<HubItemsManageResponseV2025R0>(NullableUtils.Unwrap(response.Data));
        }

    }
}