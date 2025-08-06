using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class FileVersionRetentionsManager : IFileVersionRetentionsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public FileVersionRetentionsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves all file version retentions for the given enterprise.
        /// 
        /// **Note**:
        /// File retention API is now **deprecated**. 
        /// To get information about files and file versions under retention,
        /// see [files under retention](e://get-retention-policy-assignments-id-files-under-retention) or [file versions under retention](e://get-retention-policy-assignments-id-file-versions-under-retention) endpoints.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getFileVersionRetentions method
        /// </param>
        /// <param name="headers">
        /// Headers of getFileVersionRetentions method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileVersionRetentions> GetFileVersionRetentionsAsync(GetFileVersionRetentionsQueryParams? queryParams = default, GetFileVersionRetentionsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetFileVersionRetentionsQueryParams();
            headers = headers ?? new GetFileVersionRetentionsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "file_id", StringUtils.ToStringRepresentation(queryParams.FileId) }, { "file_version_id", StringUtils.ToStringRepresentation(queryParams.FileVersionId) }, { "policy_id", StringUtils.ToStringRepresentation(queryParams.PolicyId) }, { "disposition_action", StringUtils.ToStringRepresentation(queryParams.DispositionAction) }, { "disposition_before", StringUtils.ToStringRepresentation(queryParams.DispositionBefore) }, { "disposition_after", StringUtils.ToStringRepresentation(queryParams.DispositionAfter) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/file_version_retentions"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FileVersionRetentions>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Returns information about a file version retention.
        /// 
        /// **Note**:
        /// File retention API is now **deprecated**. 
        /// To get information about files and file versions under retention,
        /// see [files under retention](e://get-retention-policy-assignments-id-files-under-retention) or [file versions under retention](e://get-retention-policy-assignments-id-file-versions-under-retention) endpoints.
        /// </summary>
        /// <param name="fileVersionRetentionId">
        /// The ID of the file version retention.
        /// Example: "3424234"
        /// </param>
        /// <param name="headers">
        /// Headers of getFileVersionRetentionById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileVersionRetention> GetFileVersionRetentionByIdAsync(string fileVersionRetentionId, GetFileVersionRetentionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetFileVersionRetentionByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/file_version_retentions/", StringUtils.ToStringRepresentation(fileVersionRetentionId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FileVersionRetention>(NullableUtils.Unwrap(response.Data));
        }

    }
}