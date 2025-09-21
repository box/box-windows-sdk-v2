using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class TrashedFilesManager : ITrashedFilesManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public TrashedFilesManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Restores a file that has been moved to the trash.
        /// 
        /// An optional new parent ID can be provided to restore the file to in case the
        /// original folder has been deleted.
        /// </summary>
        /// <param name="fileId">
        /// The unique identifier that represents a file.
        /// 
        /// The ID for any file can be determined
        /// by visiting a file in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/files/123`
        /// the `file_id` is `123`.
        /// Example: "12345"
        /// </param>
        /// <param name="requestBody">
        /// Request body of restoreFileFromTrash method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of restoreFileFromTrash method
        /// </param>
        /// <param name="headers">
        /// Headers of restoreFileFromTrash method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<TrashFileRestored> RestoreFileFromTrashAsync(string fileId, RestoreFileFromTrashRequestBody? requestBody = default, RestoreFileFromTrashQueryParams? queryParams = default, RestoreFileFromTrashHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new RestoreFileFromTrashRequestBody();
            queryParams = queryParams ?? new RestoreFileFromTrashQueryParams();
            headers = headers ?? new RestoreFileFromTrashHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId)), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<TrashFileRestored>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieves a file that has been moved to the trash.
        /// 
        /// Please note that only if the file itself has been moved to the
        /// trash can it be retrieved with this API call. If instead one of
        /// its parent folders was moved to the trash, only that folder
        /// can be inspected using the
        /// [`GET /folders/:id/trash`](e://get_folders_id_trash) API.
        /// 
        /// To list all items that have been moved to the trash, please
        /// use the [`GET /folders/trash/items`](e://get-folders-trash-items/)
        /// API.
        /// </summary>
        /// <param name="fileId">
        /// The unique identifier that represents a file.
        /// 
        /// The ID for any file can be determined
        /// by visiting a file in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/files/123`
        /// the `file_id` is `123`.
        /// Example: "12345"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getTrashedFileById method
        /// </param>
        /// <param name="headers">
        /// Headers of getTrashedFileById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<TrashFile> GetTrashedFileByIdAsync(string fileId, GetTrashedFileByIdQueryParams? queryParams = default, GetTrashedFileByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetTrashedFileByIdQueryParams();
            headers = headers ?? new GetTrashedFileByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId), "/trash"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<TrashFile>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Permanently deletes a file that is in the trash.
        /// This action cannot be undone.
        /// </summary>
        /// <param name="fileId">
        /// The unique identifier that represents a file.
        /// 
        /// The ID for any file can be determined
        /// by visiting a file in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/files/123`
        /// the `file_id` is `123`.
        /// Example: "12345"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteTrashedFileById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteTrashedFileByIdAsync(string fileId, DeleteTrashedFileByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteTrashedFileByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId), "/trash"), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}