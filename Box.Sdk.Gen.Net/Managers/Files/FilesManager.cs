using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class FilesManager : IFilesManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public FilesManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves the details about a file.
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
        /// Query parameters of getFileById method
        /// </param>
        /// <param name="headers">
        /// Headers of getFileById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileFull> GetFileByIdAsync(string fileId, GetFileByIdQueryParams? queryParams = default, GetFileByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetFileByIdQueryParams();
            headers = headers ?? new GetFileByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "if-none-match", StringUtils.ToStringRepresentation(headers.IfNoneMatch) }, { "boxapi", StringUtils.ToStringRepresentation(headers.Boxapi) }, { "x-rep-hints", StringUtils.ToStringRepresentation(headers.XRepHints) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FileFull>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a file. This can be used to rename or move a file,
        /// create a shared link, or lock a file.
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
        /// Request body of updateFileById method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of updateFileById method
        /// </param>
        /// <param name="headers">
        /// Headers of updateFileById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileFull> UpdateFileByIdAsync(string fileId, UpdateFileByIdRequestBody? requestBody = default, UpdateFileByIdQueryParams? queryParams = default, UpdateFileByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new UpdateFileByIdRequestBody();
            queryParams = queryParams ?? new UpdateFileByIdQueryParams();
            headers = headers ?? new UpdateFileByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "if-match", StringUtils.ToStringRepresentation(headers.IfMatch) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FileFull>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes a file, either permanently or by moving it to
        /// the trash.
        /// 
        /// The the enterprise settings determine whether the item will
        /// be permanently deleted from Box or moved to the trash.
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
        /// Headers of deleteFileById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteFileByIdAsync(string fileId, DeleteFileByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteFileByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "if-match", StringUtils.ToStringRepresentation(headers.IfMatch) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a copy of a file.
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
        /// Request body of copyFile method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of copyFile method
        /// </param>
        /// <param name="headers">
        /// Headers of copyFile method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FileFull> CopyFileAsync(string fileId, CopyFileRequestBody requestBody, CopyFileQueryParams? queryParams = default, CopyFileHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new CopyFileQueryParams();
            headers = headers ?? new CopyFileHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId), "/copy"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FileFull>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieves a thumbnail, or smaller image representation, of a file.
        /// 
        /// Sizes of `32x32`,`64x64`, `128x128`, and `256x256` can be returned in
        /// the `.png` format and sizes of `32x32`, `160x160`, and `320x320`
        /// can be returned in the `.jpg` format.
        /// 
        /// Thumbnails can be generated for the image and video file formats listed
        /// [found on our community site][1].
        /// 
        /// [1]: https://community.box.com/t5/Migrating-and-Previewing-Content/File-Types-and-Fonts-Supported-in-Box-Content-Preview/ta-p/327
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
        /// <param name="extension">
        /// The file format for the thumbnail.
        /// Example: "png"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getFileThumbnailById method
        /// </param>
        /// <param name="headers">
        /// Headers of getFileThumbnailById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<string> GetFileThumbnailUrlAsync(string fileId, GetFileThumbnailUrlExtension extension, GetFileThumbnailUrlQueryParams? queryParams = default, GetFileThumbnailUrlHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetFileThumbnailUrlQueryParams();
            headers = headers ?? new GetFileThumbnailUrlHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "min_height", StringUtils.ToStringRepresentation(queryParams.MinHeight) }, { "min_width", StringUtils.ToStringRepresentation(queryParams.MinWidth) }, { "max_height", StringUtils.ToStringRepresentation(queryParams.MaxHeight) }, { "max_width", StringUtils.ToStringRepresentation(queryParams.MaxWidth) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId), "/thumbnail.", StringUtils.ToStringRepresentation(extension)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken, FollowRedirects = false }).ConfigureAwait(false);
            if (response.Headers.ContainsKey("location")) {
                return NullableUtils.Unwrap(response.Headers["location"]);
            }
            if (response.Headers.ContainsKey("Location")) {
                return NullableUtils.Unwrap(response.Headers["Location"]);
            }
            throw new BoxSdkException(message: "No location header in response");
        }

        /// <summary>
        /// Retrieves a thumbnail, or smaller image representation, of a file.
        /// 
        /// Sizes of `32x32`,`64x64`, `128x128`, and `256x256` can be returned in
        /// the `.png` format and sizes of `32x32`, `160x160`, and `320x320`
        /// can be returned in the `.jpg` format.
        /// 
        /// Thumbnails can be generated for the image and video file formats listed
        /// [found on our community site][1].
        /// 
        /// [1]: https://community.box.com/t5/Migrating-and-Previewing-Content/File-Types-and-Fonts-Supported-in-Box-Content-Preview/ta-p/327
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
        /// <param name="extension">
        /// The file format for the thumbnail.
        /// Example: "png"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getFileThumbnailById method
        /// </param>
        /// <param name="headers">
        /// Headers of getFileThumbnailById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<System.IO.Stream?> GetFileThumbnailByIdAsync(string fileId, GetFileThumbnailByIdExtension extension, GetFileThumbnailByIdQueryParams? queryParams = default, GetFileThumbnailByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetFileThumbnailByIdQueryParams();
            headers = headers ?? new GetFileThumbnailByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "min_height", StringUtils.ToStringRepresentation(queryParams.MinHeight) }, { "min_width", StringUtils.ToStringRepresentation(queryParams.MinWidth) }, { "max_height", StringUtils.ToStringRepresentation(queryParams.MaxHeight) }, { "max_width", StringUtils.ToStringRepresentation(queryParams.MaxWidth) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/files/", StringUtils.ToStringRepresentation(fileId), "/thumbnail.", StringUtils.ToStringRepresentation(extension)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Binary) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            if (StringUtils.ToStringRepresentation(response.Status) == "202") {
                return null;
            }
            return response.Content;
        }

    }
}