using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class TrashedItemsManager : ITrashedItemsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public TrashedItemsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves the files and folders that have been moved
        /// to the trash.
        /// 
        /// Any attribute in the full files or folders objects can be passed
        /// in with the `fields` parameter to retrieve those specific
        /// attributes that are not returned by default.
        /// 
        /// This endpoint defaults to use offset-based pagination, yet also supports
        /// marker-based pagination using the `marker` parameter.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getTrashedItems method
        /// </param>
        /// <param name="headers">
        /// Headers of getTrashedItems method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Items> GetTrashedItemsAsync(GetTrashedItemsQueryParams? queryParams = default, GetTrashedItemsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetTrashedItemsQueryParams();
            headers = headers ?? new GetTrashedItemsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "offset", StringUtils.ToStringRepresentation(queryParams.Offset) }, { "usemarker", StringUtils.ToStringRepresentation(queryParams.Usemarker) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "direction", StringUtils.ToStringRepresentation(queryParams.Direction?.Value) }, { "sort", StringUtils.ToStringRepresentation(queryParams.Sort?.Value) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/folders/trash/items"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Items>(NullableUtils.Unwrap(response.Data));
        }

    }
}