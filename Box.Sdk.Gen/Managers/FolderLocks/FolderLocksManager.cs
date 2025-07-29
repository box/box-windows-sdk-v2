using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class FolderLocksManager : IFolderLocksManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public FolderLocksManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves folder lock details for a given folder.
        /// 
        /// You must be authenticated as the owner or co-owner of the folder to
        /// use this endpoint.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getFolderLocks method
        /// </param>
        /// <param name="headers">
        /// Headers of getFolderLocks method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FolderLocks> GetFolderLocksAsync(GetFolderLocksQueryParams queryParams, GetFolderLocksHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetFolderLocksHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "folder_id", StringUtils.ToStringRepresentation(queryParams.FolderId) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/folder_locks"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FolderLocks>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a folder lock on a folder, preventing it from being moved and/or
        /// deleted.
        /// 
        /// You must be authenticated as the owner or co-owner of the folder to
        /// use this endpoint.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createFolderLock method
        /// </param>
        /// <param name="headers">
        /// Headers of createFolderLock method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FolderLock> CreateFolderLockAsync(CreateFolderLockRequestBody requestBody, CreateFolderLockHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateFolderLockHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/folder_locks"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FolderLock>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes a folder lock on a given folder.
        /// 
        /// You must be authenticated as the owner or co-owner of the folder to
        /// use this endpoint.
        /// </summary>
        /// <param name="folderLockId">
        /// The ID of the folder lock.
        /// Example: "12345"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteFolderLockById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteFolderLockByIdAsync(string folderLockId, DeleteFolderLockByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteFolderLockByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/folder_locks/", StringUtils.ToStringRepresentation(folderLockId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}