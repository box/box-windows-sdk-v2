using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class ArchivesManager : IArchivesManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public ArchivesManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves archives for an enterprise.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getArchivesV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getArchivesV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ArchivesV2025R0> GetArchivesV2025R0Async(GetArchivesV2025R0QueryParams? queryParams = default, GetArchivesV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetArchivesV2025R0QueryParams();
            headers = headers ?? new GetArchivesV2025R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/archives"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ArchivesV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates an archive.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createArchiveV2025R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of createArchiveV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ArchiveV2025R0> CreateArchiveV2025R0Async(CreateArchiveV2025R0RequestBody requestBody, CreateArchiveV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateArchiveV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/archives"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ArchiveV2025R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Permanently deletes an archive.
        /// </summary>
        /// <param name="archiveId">
        /// The ID of the archive.
        /// Example: "982312"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteArchiveByIdV2025R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteArchiveByIdV2025R0Async(string archiveId, DeleteArchiveByIdV2025R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteArchiveByIdV2025R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/archives/", StringUtils.ToStringRepresentation(archiveId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}