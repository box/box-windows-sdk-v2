using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class FileRequestsManager : IFileRequestsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public FileRequestsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves the information about a file request.
        /// </summary>
        /// <param name="fileRequestId">
        /// The unique identifier that represent a file request.
        /// 
        /// The ID for any file request can be determined
        /// by visiting a file request builder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/filerequest/123`
        /// the `file_request_id` is `123`.
        /// Example: "123"
        /// </param>
        /// <param name="headers">
        /// Headers of getFileRequestById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileRequest> GetFileRequestByIdAsync(string fileRequestId, GetFileRequestByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetFileRequestByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/file_requests/", StringUtils.ToStringRepresentation(fileRequestId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FileRequest>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a file request. This can be used to activate or
        /// deactivate a file request.
        /// </summary>
        /// <param name="fileRequestId">
        /// The unique identifier that represent a file request.
        /// 
        /// The ID for any file request can be determined
        /// by visiting a file request builder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/filerequest/123`
        /// the `file_request_id` is `123`.
        /// Example: "123"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateFileRequestById method
        /// </param>
        /// <param name="headers">
        /// Headers of updateFileRequestById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileRequest> UpdateFileRequestByIdAsync(string fileRequestId, FileRequestUpdateRequest requestBody, UpdateFileRequestByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new UpdateFileRequestByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "if-match", StringUtils.ToStringRepresentation(headers.IfMatch) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/file_requests/", StringUtils.ToStringRepresentation(fileRequestId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FileRequest>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes a file request permanently.
        /// </summary>
        /// <param name="fileRequestId">
        /// The unique identifier that represent a file request.
        /// 
        /// The ID for any file request can be determined
        /// by visiting a file request builder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/filerequest/123`
        /// the `file_request_id` is `123`.
        /// Example: "123"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteFileRequestById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteFileRequestByIdAsync(string fileRequestId, DeleteFileRequestByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteFileRequestByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/file_requests/", StringUtils.ToStringRepresentation(fileRequestId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Copies an existing file request that is already present on one folder,
        /// and applies it to another folder.
        /// </summary>
        /// <param name="fileRequestId">
        /// The unique identifier that represent a file request.
        /// 
        /// The ID for any file request can be determined
        /// by visiting a file request builder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/filerequest/123`
        /// the `file_request_id` is `123`.
        /// Example: "123"
        /// </param>
        /// <param name="requestBody">
        /// Request body of createFileRequestCopy method
        /// </param>
        /// <param name="headers">
        /// Headers of createFileRequestCopy method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileRequest> CreateFileRequestCopyAsync(string fileRequestId, FileRequestCopyRequest requestBody, CreateFileRequestCopyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateFileRequestCopyHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/file_requests/", StringUtils.ToStringRepresentation(fileRequestId), "/copy"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FileRequest>(NullableUtils.Unwrap(response.Data));
        }

    }
}