using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class MetadataTaxonomiesManager : IMetadataTaxonomiesManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public MetadataTaxonomiesManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Creates a new metadata taxonomy that can be used in
        /// metadata templates.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createMetadataTaxonomy method
        /// </param>
        /// <param name="headers">
        /// Headers of createMetadataTaxonomy method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomy> CreateMetadataTaxonomyAsync(CreateMetadataTaxonomyRequestBody requestBody, CreateMetadataTaxonomyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateMetadataTaxonomyHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomy>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Used to retrieve all metadata taxonomies in a namespace.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getMetadataTaxonomies method
        /// </param>
        /// <param name="headers">
        /// Headers of getMetadataTaxonomies method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomies> GetMetadataTaxonomiesAsync(string namespaceParam, GetMetadataTaxonomiesQueryParams? queryParams = default, GetMetadataTaxonomiesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetMetadataTaxonomiesQueryParams();
            headers = headers ?? new GetMetadataTaxonomiesHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomies>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Used to retrieve a metadata taxonomy by taxonomy key.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="headers">
        /// Headers of getMetadataTaxonomyByKey method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomy> GetMetadataTaxonomyByKeyAsync(string namespaceParam, string taxonomyKey, GetMetadataTaxonomyByKeyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetMetadataTaxonomyByKeyHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomy>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates an existing metadata taxonomy.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateMetadataTaxonomy method
        /// </param>
        /// <param name="headers">
        /// Headers of updateMetadataTaxonomy method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomy> UpdateMetadataTaxonomyAsync(string namespaceParam, string taxonomyKey, UpdateMetadataTaxonomyRequestBody requestBody, UpdateMetadataTaxonomyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new UpdateMetadataTaxonomyHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey)), method: "PATCH", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomy>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Delete a metadata taxonomy.
        /// This deletion is permanent and cannot be reverted.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteMetadataTaxonomy method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteMetadataTaxonomyAsync(string namespaceParam, string taxonomyKey, DeleteMetadataTaxonomyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteMetadataTaxonomyHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates new metadata taxonomy levels.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="requestBody">
        /// Request body of createMetadataTaxonomyLevel method
        /// </param>
        /// <param name="headers">
        /// Headers of createMetadataTaxonomyLevel method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomyLevels> CreateMetadataTaxonomyLevelAsync(string namespaceParam, string taxonomyKey, IReadOnlyList<MetadataTaxonomyLevel> requestBody, CreateMetadataTaxonomyLevelHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateMetadataTaxonomyLevelHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey), "/levels"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomyLevels>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates an existing metadata taxonomy level.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="levelIndex">
        /// The index of the metadata taxonomy level.
        /// Example: 1
        /// </param>
        /// <param name="requestBody">
        /// Request body of patchMetadataTaxonomiesIdIdLevelsId method
        /// </param>
        /// <param name="headers">
        /// Headers of patchMetadataTaxonomiesIdIdLevelsId method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomyLevel> PatchMetadataTaxonomiesIdIdLevelsIdAsync(string namespaceParam, string taxonomyKey, long levelIndex, PatchMetadataTaxonomiesIdIdLevelsIdRequestBody requestBody, PatchMetadataTaxonomiesIdIdLevelsIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new PatchMetadataTaxonomiesIdIdLevelsIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey), "/levels/", StringUtils.ToStringRepresentation(levelIndex)), method: "PATCH", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomyLevel>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a new metadata taxonomy level and appends it to the existing levels.
        /// If there are no levels defined yet, this will create the first level.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="requestBody">
        /// Request body of addMetadataTaxonomyLevel method
        /// </param>
        /// <param name="headers">
        /// Headers of addMetadataTaxonomyLevel method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomyLevels> AddMetadataTaxonomyLevelAsync(string namespaceParam, string taxonomyKey, AddMetadataTaxonomyLevelRequestBody requestBody, AddMetadataTaxonomyLevelHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new AddMetadataTaxonomyLevelHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey), "/levels:append"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomyLevels>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes the last level of the metadata taxonomy.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteMetadataTaxonomyLevel method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomyLevels> DeleteMetadataTaxonomyLevelAsync(string namespaceParam, string taxonomyKey, DeleteMetadataTaxonomyLevelHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteMetadataTaxonomyLevelHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey), "/levels:trim"), method: "POST", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomyLevels>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Used to retrieve metadata taxonomy nodes based on the parameters specified. 
        /// Results are sorted in lexicographic order unless a `query` parameter is passed. 
        /// With a `query` parameter specified, results are sorted in order of relevance.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getMetadataTaxonomyNodes method
        /// </param>
        /// <param name="headers">
        /// Headers of getMetadataTaxonomyNodes method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomyNodes> GetMetadataTaxonomyNodesAsync(string namespaceParam, string taxonomyKey, GetMetadataTaxonomyNodesQueryParams? queryParams = default, GetMetadataTaxonomyNodesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetMetadataTaxonomyNodesQueryParams();
            headers = headers ?? new GetMetadataTaxonomyNodesHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "level", StringUtils.ToStringRepresentation(queryParams.Level) }, { "parent", StringUtils.ToStringRepresentation(queryParams.Parent) }, { "ancestor", StringUtils.ToStringRepresentation(queryParams.Ancestor) }, { "query", StringUtils.ToStringRepresentation(queryParams.Query) }, { "include-total-result-count", StringUtils.ToStringRepresentation(queryParams.IncludeTotalResultCount) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey), "/nodes"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomyNodes>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a new metadata taxonomy node.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="requestBody">
        /// Request body of createMetadataTaxonomyNode method
        /// </param>
        /// <param name="headers">
        /// Headers of createMetadataTaxonomyNode method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomyNode> CreateMetadataTaxonomyNodeAsync(string namespaceParam, string taxonomyKey, CreateMetadataTaxonomyNodeRequestBody requestBody, CreateMetadataTaxonomyNodeHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateMetadataTaxonomyNodeHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey), "/nodes"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomyNode>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieves a metadata taxonomy node by its identifier.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="nodeId">
        /// The identifier of the metadata taxonomy node.
        /// Example: "14d3d433-c77f-49c5-b146-9dea370f6e32"
        /// </param>
        /// <param name="headers">
        /// Headers of getMetadataTaxonomyNodeById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomyNode> GetMetadataTaxonomyNodeByIdAsync(string namespaceParam, string taxonomyKey, string nodeId, GetMetadataTaxonomyNodeByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetMetadataTaxonomyNodeByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey), "/nodes/", StringUtils.ToStringRepresentation(nodeId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomyNode>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Updates an existing metadata taxonomy node.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="nodeId">
        /// The identifier of the metadata taxonomy node.
        /// Example: "14d3d433-c77f-49c5-b146-9dea370f6e32"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateMetadataTaxonomyNode method
        /// </param>
        /// <param name="headers">
        /// Headers of updateMetadataTaxonomyNode method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomyNode> UpdateMetadataTaxonomyNodeAsync(string namespaceParam, string taxonomyKey, string nodeId, UpdateMetadataTaxonomyNodeRequestBody? requestBody = default, UpdateMetadataTaxonomyNodeHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new UpdateMetadataTaxonomyNodeRequestBody();
            headers = headers ?? new UpdateMetadataTaxonomyNodeHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey), "/nodes/", StringUtils.ToStringRepresentation(nodeId)), method: "PATCH", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomyNode>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Delete a metadata taxonomy node.
        /// This deletion is permanent and cannot be reverted.
        /// Only metadata taxonomy nodes without any children can be deleted.
        /// </summary>
        /// <param name="namespaceParam">
        /// The namespace of the metadata taxonomy.
        /// Example: "enterprise_123456"
        /// </param>
        /// <param name="taxonomyKey">
        /// The key of the metadata taxonomy.
        /// Example: "geography"
        /// </param>
        /// <param name="nodeId">
        /// The identifier of the metadata taxonomy node.
        /// Example: "14d3d433-c77f-49c5-b146-9dea370f6e32"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteMetadataTaxonomyNode method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteMetadataTaxonomyNodeAsync(string namespaceParam, string taxonomyKey, string nodeId, DeleteMetadataTaxonomyNodeHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteMetadataTaxonomyNodeHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_taxonomies/", StringUtils.ToStringRepresentation(namespaceParam), "/", StringUtils.ToStringRepresentation(taxonomyKey), "/nodes/", StringUtils.ToStringRepresentation(nodeId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Used to retrieve metadata taxonomy nodes which are available for the taxonomy field based 
        /// on its configuration and the parameters specified. 
        /// Results are sorted in lexicographic order unless a `query` parameter is passed. 
        /// With a `query` parameter specified, results are sorted in order of relevance.
        /// </summary>
        /// <param name="scope">
        /// The scope of the metadata template.
        /// Example: "global"
        /// </param>
        /// <param name="templateKey">
        /// The name of the metadata template.
        /// Example: "properties"
        /// </param>
        /// <param name="fieldKey">
        /// The key of the metadata taxonomy field in the template.
        /// Example: "geography"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getMetadataTemplateFieldOptions method
        /// </param>
        /// <param name="headers">
        /// Headers of getMetadataTemplateFieldOptions method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataTaxonomyNodes> GetMetadataTemplateFieldOptionsAsync(GetMetadataTemplateFieldOptionsScope scope, string templateKey, string fieldKey, GetMetadataTemplateFieldOptionsQueryParams? queryParams = default, GetMetadataTemplateFieldOptionsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetMetadataTemplateFieldOptionsQueryParams();
            headers = headers ?? new GetMetadataTemplateFieldOptionsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "level", StringUtils.ToStringRepresentation(queryParams.Level) }, { "parent", StringUtils.ToStringRepresentation(queryParams.Parent) }, { "ancestor", StringUtils.ToStringRepresentation(queryParams.Ancestor) }, { "query", StringUtils.ToStringRepresentation(queryParams.Query) }, { "include-total-result-count", StringUtils.ToStringRepresentation(queryParams.IncludeTotalResultCount) }, { "only-selectable-options", StringUtils.ToStringRepresentation(queryParams.OnlySelectableOptions) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_templates/", StringUtils.ToStringRepresentation(scope), "/", StringUtils.ToStringRepresentation(templateKey), "/fields/", StringUtils.ToStringRepresentation(fieldKey), "/options"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataTaxonomyNodes>(NullableUtils.Unwrap(response.Data));
        }

    }
}