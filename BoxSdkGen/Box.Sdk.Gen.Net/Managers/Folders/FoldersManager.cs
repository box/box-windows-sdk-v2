using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class FoldersManager : IFoldersManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public FoldersManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves details for a folder, including the first 100 entries
        /// in the folder.
        /// 
        /// Passing `sort`, `direction`, `offset`, and `limit`
        /// parameters in query allows you to manage the
        /// list of returned
        /// [folder items](r://folder--full#param-item-collection).
        /// 
        /// To fetch more items within the folder, use the
        /// [Get items in a folder](e://get-folders-id-items) endpoint.
        /// </summary>
        /// <param name="folderId">
        /// The unique identifier that represent a folder.
        /// 
        /// The ID for any folder can be determined
        /// by visiting this folder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/folder/123`
        /// the `folder_id` is `123`.
        /// 
        /// The root folder of a Box account is
        /// always represented by the ID `0`.
        /// Example: "12345"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getFolderById method
        /// </param>
        /// <param name="headers">
        /// Headers of getFolderById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FolderFull> GetFolderByIdAsync(string folderId, GetFolderByIdQueryParams? queryParams = default, GetFolderByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetFolderByIdQueryParams();
            headers = headers ?? new GetFolderByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) }, { "sort", StringUtils.ToStringRepresentation(queryParams.Sort?.Value) }, { "direction", StringUtils.ToStringRepresentation(queryParams.Direction?.Value) }, { "offset", StringUtils.ToStringRepresentation(queryParams.Offset) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "if-none-match", StringUtils.ToStringRepresentation(headers.IfNoneMatch) }, { "boxapi", StringUtils.ToStringRepresentation(headers.Boxapi) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/folders/", StringUtils.ToStringRepresentation(folderId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FolderFull>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a folder. This can be also be used to move the folder,
        /// create shared links, update collaborations, and more.
        /// </summary>
        /// <param name="folderId">
        /// The unique identifier that represent a folder.
        /// 
        /// The ID for any folder can be determined
        /// by visiting this folder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/folder/123`
        /// the `folder_id` is `123`.
        /// 
        /// The root folder of a Box account is
        /// always represented by the ID `0`.
        /// Example: "12345"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateFolderById method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of updateFolderById method
        /// </param>
        /// <param name="headers">
        /// Headers of updateFolderById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FolderFull> UpdateFolderByIdAsync(string folderId, UpdateFolderByIdRequestBody? requestBody = default, UpdateFolderByIdQueryParams? queryParams = default, UpdateFolderByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new UpdateFolderByIdRequestBody();
            queryParams = queryParams ?? new UpdateFolderByIdQueryParams();
            headers = headers ?? new UpdateFolderByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "if-match", StringUtils.ToStringRepresentation(headers.IfMatch) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/folders/", StringUtils.ToStringRepresentation(folderId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FolderFull>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes a folder, either permanently or by moving it to
        /// the trash.
        /// </summary>
        /// <param name="folderId">
        /// The unique identifier that represent a folder.
        /// 
        /// The ID for any folder can be determined
        /// by visiting this folder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/folder/123`
        /// the `folder_id` is `123`.
        /// 
        /// The root folder of a Box account is
        /// always represented by the ID `0`.
        /// Example: "12345"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of deleteFolderById method
        /// </param>
        /// <param name="headers">
        /// Headers of deleteFolderById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteFolderByIdAsync(string folderId, DeleteFolderByIdQueryParams? queryParams = default, DeleteFolderByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new DeleteFolderByIdQueryParams();
            headers = headers ?? new DeleteFolderByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "recursive", StringUtils.ToStringRepresentation(queryParams.Recursive) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "if-match", StringUtils.ToStringRepresentation(headers.IfMatch) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/folders/", StringUtils.ToStringRepresentation(folderId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Retrieves a page of items in a folder. These items can be files,
        /// folders, and web links.
        /// 
        /// To request more information about the folder itself, like its size,
        /// use the [Get a folder](#get-folders-id) endpoint instead.
        /// </summary>
        /// <param name="folderId">
        /// The unique identifier that represent a folder.
        /// 
        /// The ID for any folder can be determined
        /// by visiting this folder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/folder/123`
        /// the `folder_id` is `123`.
        /// 
        /// The root folder of a Box account is
        /// always represented by the ID `0`.
        /// Example: "12345"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getFolderItems method
        /// </param>
        /// <param name="headers">
        /// Headers of getFolderItems method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Items> GetFolderItemsAsync(string folderId, GetFolderItemsQueryParams? queryParams = default, GetFolderItemsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetFolderItemsQueryParams();
            headers = headers ?? new GetFolderItemsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) }, { "usemarker", StringUtils.ToStringRepresentation(queryParams.Usemarker) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "offset", StringUtils.ToStringRepresentation(queryParams.Offset) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "sort", StringUtils.ToStringRepresentation(queryParams.Sort?.Value) }, { "direction", StringUtils.ToStringRepresentation(queryParams.Direction?.Value) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "boxapi", StringUtils.ToStringRepresentation(headers.Boxapi) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/folders/", StringUtils.ToStringRepresentation(folderId), "/items"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Items>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a new empty folder within the specified parent folder.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createFolder method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of createFolder method
        /// </param>
        /// <param name="headers">
        /// Headers of createFolder method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FolderFull> CreateFolderAsync(CreateFolderRequestBody requestBody, CreateFolderQueryParams? queryParams = default, CreateFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new CreateFolderQueryParams();
            headers = headers ?? new CreateFolderHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/folders"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FolderFull>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a copy of a folder within a destination folder.
        /// 
        /// The original folder will not be changed.
        /// </summary>
        /// <param name="folderId">
        /// The unique identifier of the folder to copy.
        /// 
        /// The ID for any folder can be determined
        /// by visiting this folder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/folder/123`
        /// the `folder_id` is `123`.
        /// 
        /// The root folder with the ID `0` can not be copied.
        /// Example: "0"
        /// </param>
        /// <param name="requestBody">
        /// Request body of copyFolder method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of copyFolder method
        /// </param>
        /// <param name="headers">
        /// Headers of copyFolder method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FolderFull> CopyFolderAsync(string folderId, CopyFolderRequestBody requestBody, CopyFolderQueryParams? queryParams = default, CopyFolderHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new CopyFolderQueryParams();
            headers = headers ?? new CopyFolderHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/folders/", StringUtils.ToStringRepresentation(folderId), "/copy"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FolderFull>(NullableUtils.Unwrap(response.Data));
        }

    }
}