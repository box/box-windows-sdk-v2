using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class SharedLinksWebLinksManager : ISharedLinksWebLinksManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public SharedLinksWebLinksManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Returns the web link represented by a shared link.
        /// 
        /// A shared web link can be represented by a shared link,
        /// which can originate within the current enterprise or within another.
        /// 
        /// This endpoint allows an application to retrieve information about a
        /// shared web link when only given a shared link.
        /// </summary>
        /// <param name="headers">
        /// Headers of findWebLinkForSharedLink method
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of findWebLinkForSharedLink method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<WebLink> FindWebLinkForSharedLinkAsync(FindWebLinkForSharedLinkHeaders headers, FindWebLinkForSharedLinkQueryParams? queryParams = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new FindWebLinkForSharedLinkQueryParams();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() { { "if-none-match", StringUtils.ToStringRepresentation(headers.IfNoneMatch) }, { "boxapi", StringUtils.ToStringRepresentation(headers.Boxapi) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/shared_items#web_links"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<WebLink>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Gets the information for a shared link on a web link.
        /// </summary>
        /// <param name="webLinkId">
        /// The ID of the web link.
        /// Example: "12345"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getSharedLinkForWebLink method
        /// </param>
        /// <param name="headers">
        /// Headers of getSharedLinkForWebLink method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<WebLink> GetSharedLinkForWebLinkAsync(string webLinkId, GetSharedLinkForWebLinkQueryParams queryParams, GetSharedLinkForWebLinkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetSharedLinkForWebLinkHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/web_links/", StringUtils.ToStringRepresentation(webLinkId), "#get_shared_link"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<WebLink>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Adds a shared link to a web link.
        /// </summary>
        /// <param name="webLinkId">
        /// The ID of the web link.
        /// Example: "12345"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of addShareLinkToWebLink method
        /// </param>
        /// <param name="requestBody">
        /// Request body of addShareLinkToWebLink method
        /// </param>
        /// <param name="headers">
        /// Headers of addShareLinkToWebLink method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<WebLink> AddShareLinkToWebLinkAsync(string webLinkId, AddShareLinkToWebLinkQueryParams queryParams, AddShareLinkToWebLinkRequestBody? requestBody = default, AddShareLinkToWebLinkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new AddShareLinkToWebLinkRequestBody();
            headers = headers ?? new AddShareLinkToWebLinkHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/web_links/", StringUtils.ToStringRepresentation(webLinkId), "#add_shared_link"), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<WebLink>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates a shared link on a web link.
        /// </summary>
        /// <param name="webLinkId">
        /// The ID of the web link.
        /// Example: "12345"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of updateSharedLinkOnWebLink method
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateSharedLinkOnWebLink method
        /// </param>
        /// <param name="headers">
        /// Headers of updateSharedLinkOnWebLink method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<WebLink> UpdateSharedLinkOnWebLinkAsync(string webLinkId, UpdateSharedLinkOnWebLinkQueryParams queryParams, UpdateSharedLinkOnWebLinkRequestBody? requestBody = default, UpdateSharedLinkOnWebLinkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new UpdateSharedLinkOnWebLinkRequestBody();
            headers = headers ?? new UpdateSharedLinkOnWebLinkHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/web_links/", StringUtils.ToStringRepresentation(webLinkId), "#update_shared_link"), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<WebLink>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Removes a shared link from a web link.
        /// </summary>
        /// <param name="webLinkId">
        /// The ID of the web link.
        /// Example: "12345"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of removeSharedLinkFromWebLink method
        /// </param>
        /// <param name="requestBody">
        /// Request body of removeSharedLinkFromWebLink method
        /// </param>
        /// <param name="headers">
        /// Headers of removeSharedLinkFromWebLink method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<WebLink> RemoveSharedLinkFromWebLinkAsync(string webLinkId, RemoveSharedLinkFromWebLinkQueryParams queryParams, RemoveSharedLinkFromWebLinkRequestBody? requestBody = default, RemoveSharedLinkFromWebLinkHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new RemoveSharedLinkFromWebLinkRequestBody();
            headers = headers ?? new RemoveSharedLinkFromWebLinkHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/web_links/", StringUtils.ToStringRepresentation(webLinkId), "#remove_shared_link"), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<WebLink>(NullableUtils.Unwrap(response.Data));
        }

    }
}