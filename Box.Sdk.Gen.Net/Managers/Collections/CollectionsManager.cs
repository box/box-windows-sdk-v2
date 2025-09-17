using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CollectionsManager : ICollectionsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public CollectionsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves all collections for a given user.
        /// 
        /// Currently, only the `favorites` collection
        /// is supported.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getCollections method
        /// </param>
        /// <param name="headers">
        /// Headers of getCollections method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Collections> GetCollectionsAsync(GetCollectionsQueryParams? queryParams = default, GetCollectionsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetCollectionsQueryParams();
            headers = headers ?? new GetCollectionsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) }, { "offset", StringUtils.ToStringRepresentation(queryParams.Offset) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collections"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Collections>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieves the files and/or folders contained within
        /// this collection.
        /// </summary>
        /// <param name="collectionId">
        /// The ID of the collection.
        /// Example: "926489"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getCollectionItems method
        /// </param>
        /// <param name="headers">
        /// Headers of getCollectionItems method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<ItemsOffsetPaginated> GetCollectionItemsAsync(string collectionId, GetCollectionItemsQueryParams? queryParams = default, GetCollectionItemsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetCollectionItemsQueryParams();
            headers = headers ?? new GetCollectionItemsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) }, { "offset", StringUtils.ToStringRepresentation(queryParams.Offset) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collections/", StringUtils.ToStringRepresentation(collectionId), "/items"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<ItemsOffsetPaginated>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieves a collection by its ID.
        /// </summary>
        /// <param name="collectionId">
        /// The ID of the collection.
        /// Example: "926489"
        /// </param>
        /// <param name="headers">
        /// Headers of getCollectionById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Collection> GetCollectionByIdAsync(string collectionId, GetCollectionByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetCollectionByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/collections/", StringUtils.ToStringRepresentation(collectionId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Collection>(NullableUtils.Unwrap(response.Data));
        }

    }
}