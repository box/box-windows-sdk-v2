using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class MetadataCascadePoliciesManager : IMetadataCascadePoliciesManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public MetadataCascadePoliciesManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves a list of all the metadata cascade policies
        /// that are applied to a given folder. This can not be used on the root
        /// folder with ID `0`.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getMetadataCascadePolicies method
        /// </param>
        /// <param name="headers">
        /// Headers of getMetadataCascadePolicies method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataCascadePolicies> GetMetadataCascadePoliciesAsync(GetMetadataCascadePoliciesQueryParams queryParams, GetMetadataCascadePoliciesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetMetadataCascadePoliciesHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "folder_id", StringUtils.ToStringRepresentation(queryParams.FolderId) }, { "owner_enterprise_id", StringUtils.ToStringRepresentation(queryParams.OwnerEnterpriseId) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "offset", StringUtils.ToStringRepresentation(queryParams.Offset) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_cascade_policies"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataCascadePolicies>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Creates a new metadata cascade policy that applies a given
        /// metadata template to a given folder and automatically
        /// cascades it down to any files within that folder.
        /// 
        /// In order for the policy to be applied a metadata instance must first
        /// be applied to the folder the policy is to be applied to.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createMetadataCascadePolicy method
        /// </param>
        /// <param name="headers">
        /// Headers of createMetadataCascadePolicy method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataCascadePolicy> CreateMetadataCascadePolicyAsync(CreateMetadataCascadePolicyRequestBody requestBody, CreateMetadataCascadePolicyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateMetadataCascadePolicyHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_cascade_policies"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataCascadePolicy>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieve a specific metadata cascade policy assigned to a folder.
        /// </summary>
        /// <param name="metadataCascadePolicyId">
        /// The ID of the metadata cascade policy.
        /// Example: "6fd4ff89-8fc1-42cf-8b29-1890dedd26d7"
        /// </param>
        /// <param name="headers">
        /// Headers of getMetadataCascadePolicyById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<MetadataCascadePolicy> GetMetadataCascadePolicyByIdAsync(string metadataCascadePolicyId, GetMetadataCascadePolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetMetadataCascadePolicyByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_cascade_policies/", StringUtils.ToStringRepresentation(metadataCascadePolicyId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<MetadataCascadePolicy>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Deletes a metadata cascade policy.
        /// </summary>
        /// <param name="metadataCascadePolicyId">
        /// The ID of the metadata cascade policy.
        /// Example: "6fd4ff89-8fc1-42cf-8b29-1890dedd26d7"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteMetadataCascadePolicyById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteMetadataCascadePolicyByIdAsync(string metadataCascadePolicyId, DeleteMetadataCascadePolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteMetadataCascadePolicyByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_cascade_policies/", StringUtils.ToStringRepresentation(metadataCascadePolicyId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Force the metadata on a folder with a metadata cascade policy to be applied to
        /// all of its children. This can be used after creating a new cascade policy to
        /// enforce the metadata to be cascaded down to all existing files within that
        /// folder.
        /// </summary>
        /// <param name="metadataCascadePolicyId">
        /// The ID of the cascade policy to force-apply.
        /// Example: "6fd4ff89-8fc1-42cf-8b29-1890dedd26d7"
        /// </param>
        /// <param name="requestBody">
        /// Request body of applyMetadataCascadePolicy method
        /// </param>
        /// <param name="headers">
        /// Headers of applyMetadataCascadePolicy method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task ApplyMetadataCascadePolicyAsync(string metadataCascadePolicyId, ApplyMetadataCascadePolicyRequestBody requestBody, ApplyMetadataCascadePolicyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new ApplyMetadataCascadePolicyHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/metadata_cascade_policies/", StringUtils.ToStringRepresentation(metadataCascadePolicyId), "/apply"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}