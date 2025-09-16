using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CollaborationAllowlistEntriesManager : ICollaborationAllowlistEntriesManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public CollaborationAllowlistEntriesManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Returns the list domains that have been deemed safe to create collaborations
        /// for within the current enterprise.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getCollaborationWhitelistEntries method
        /// </param>
        /// <param name="headers">
        /// Headers of getCollaborationWhitelistEntries method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<CollaborationAllowlistEntries> GetCollaborationWhitelistEntriesAsync(GetCollaborationWhitelistEntriesQueryParams? queryParams = default, GetCollaborationWhitelistEntriesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetCollaborationWhitelistEntriesQueryParams();
            headers = headers ?? new GetCollaborationWhitelistEntriesHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collaboration_whitelist_entries"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<CollaborationAllowlistEntries>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a new entry in the list of allowed domains to allow
        /// collaboration for.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createCollaborationWhitelistEntry method
        /// </param>
        /// <param name="headers">
        /// Headers of createCollaborationWhitelistEntry method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<CollaborationAllowlistEntry> CreateCollaborationWhitelistEntryAsync(CreateCollaborationWhitelistEntryRequestBody requestBody, CreateCollaborationWhitelistEntryHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateCollaborationWhitelistEntryHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collaboration_whitelist_entries"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<CollaborationAllowlistEntry>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Returns a domain that has been deemed safe to create collaborations
        /// for within the current enterprise.
        /// </summary>
        /// <param name="collaborationWhitelistEntryId">
        /// The ID of the entry in the list.
        /// Example: "213123"
        /// </param>
        /// <param name="headers">
        /// Headers of getCollaborationWhitelistEntryById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<CollaborationAllowlistEntry> GetCollaborationWhitelistEntryByIdAsync(string collaborationWhitelistEntryId, GetCollaborationWhitelistEntryByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetCollaborationWhitelistEntryByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collaboration_whitelist_entries/", StringUtils.ToStringRepresentation(collaborationWhitelistEntryId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<CollaborationAllowlistEntry>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Removes a domain from the list of domains that have been deemed safe to create
        /// collaborations for within the current enterprise.
        /// </summary>
        /// <param name="collaborationWhitelistEntryId">
        /// The ID of the entry in the list.
        /// Example: "213123"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteCollaborationWhitelistEntryById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteCollaborationWhitelistEntryByIdAsync(string collaborationWhitelistEntryId, DeleteCollaborationWhitelistEntryByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteCollaborationWhitelistEntryByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collaboration_whitelist_entries/", StringUtils.ToStringRepresentation(collaborationWhitelistEntryId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}