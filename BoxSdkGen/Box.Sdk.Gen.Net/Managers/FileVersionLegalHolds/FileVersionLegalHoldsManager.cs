using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class FileVersionLegalHoldsManager : IFileVersionLegalHoldsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public FileVersionLegalHoldsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves information about the legal hold policies
        /// assigned to a file version.
        /// </summary>
        /// <param name="fileVersionLegalHoldId">
        /// The ID of the file version legal hold.
        /// Example: "2348213"
        /// </param>
        /// <param name="headers">
        /// Headers of getFileVersionLegalHoldById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileVersionLegalHold> GetFileVersionLegalHoldByIdAsync(string fileVersionLegalHoldId, GetFileVersionLegalHoldByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetFileVersionLegalHoldByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/file_version_legal_holds/", StringUtils.ToStringRepresentation(fileVersionLegalHoldId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FileVersionLegalHold>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Get a list of file versions on legal hold for a legal hold
        /// assignment.
        /// 
        /// Due to ongoing re-architecture efforts this API might not return all file
        /// versions for this policy ID.
        /// 
        /// Instead, this API will only return file versions held in the legacy
        /// architecture. Two new endpoints will available to request any file versions
        /// held in the new architecture.
        /// 
        /// For file versions held in the new architecture, the `GET
        /// /legal_hold_policy_assignments/:id/file_versions_on_hold` API can be used to
        /// return all past file versions available for this policy assignment, and the
        /// `GET /legal_hold_policy_assignments/:id/files_on_hold` API can be used to
        /// return any current (latest) versions of a file under legal hold.
        /// 
        /// The `GET /legal_hold_policy_assignments?policy_id={id}` API can be used to
        /// find a list of policy assignments for a given policy ID.
        /// 
        /// Once the re-architecture is completed this API will be deprecated.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getFileVersionLegalHolds method
        /// </param>
        /// <param name="headers">
        /// Headers of getFileVersionLegalHolds method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileVersionLegalHolds> GetFileVersionLegalHoldsAsync(GetFileVersionLegalHoldsQueryParams queryParams, GetFileVersionLegalHoldsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetFileVersionLegalHoldsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "policy_id", StringUtils.ToStringRepresentation(queryParams.PolicyId) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/file_version_legal_holds"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FileVersionLegalHolds>(NullableUtils.Unwrap(response.Data));
        }

    }
}