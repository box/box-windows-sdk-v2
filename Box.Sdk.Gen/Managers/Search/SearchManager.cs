using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class SearchManager : ISearchManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public SearchManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Create a search using SQL-like syntax to return items that match specific
        /// metadata.
        /// 
        /// By default, this endpoint returns only the most basic info about the items for
        /// which the query matches. To get additional fields for each item, including any
        /// of the metadata, use the `fields` attribute in the query.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of searchByMetadataQuery method
        /// </param>
        /// <param name="headers">
        /// Headers of searchByMetadataQuery method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataQueryResults> SearchByMetadataQueryAsync(MetadataQuery requestBody, SearchByMetadataQueryHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new SearchByMetadataQueryHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_queries/execute_read"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataQueryResults>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Searches for files, folders, web links, and shared files across the
        /// users content or across the entire enterprise.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of searchForContent method
        /// </param>
        /// <param name="headers">
        /// Headers of searchForContent method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<SearchResultsOrSearchResultsWithSharedLinks> SearchForContentAsync(SearchForContentQueryParams? queryParams = default, SearchForContentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new SearchForContentQueryParams();
            headers = headers ?? new SearchForContentHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "query", StringUtils.ToStringRepresentation(queryParams.Query) }, { "scope", StringUtils.ToStringRepresentation(queryParams.Scope?.Value) }, { "file_extensions", StringUtils.ToStringRepresentation(queryParams.FileExtensions) }, { "created_at_range", StringUtils.ToStringRepresentation(queryParams.CreatedAtRange) }, { "updated_at_range", StringUtils.ToStringRepresentation(queryParams.UpdatedAtRange) }, { "size_range", StringUtils.ToStringRepresentation(queryParams.SizeRange) }, { "owner_user_ids", StringUtils.ToStringRepresentation(queryParams.OwnerUserIds) }, { "recent_updater_user_ids", StringUtils.ToStringRepresentation(queryParams.RecentUpdaterUserIds) }, { "ancestor_folder_ids", StringUtils.ToStringRepresentation(queryParams.AncestorFolderIds) }, { "content_types", StringUtils.ToStringRepresentation(queryParams.ContentTypes) }, { "type", StringUtils.ToStringRepresentation(queryParams.Type?.Value) }, { "trash_content", StringUtils.ToStringRepresentation(queryParams.TrashContent) }, { "mdfilters", StringUtils.ToStringRepresentation(queryParams.Mdfilters) }, { "sort", StringUtils.ToStringRepresentation(queryParams.Sort?.Value) }, { "direction", StringUtils.ToStringRepresentation(queryParams.Direction?.Value) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "include_recent_shared_links", StringUtils.ToStringRepresentation(queryParams.IncludeRecentSharedLinks) }, { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) }, { "offset", StringUtils.ToStringRepresentation(queryParams.Offset) }, { "deleted_user_ids", StringUtils.ToStringRepresentation(queryParams.DeletedUserIds) }, { "deleted_at_range", StringUtils.ToStringRepresentation(queryParams.DeletedAtRange) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/search"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.DeserializeWithoutRawJson<SearchResultsOrSearchResultsWithSharedLinks>(NullableUtils.Unwrap(response.Data));
        }

    }
}