using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class LegalHoldPoliciesManager : ILegalHoldPoliciesManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public LegalHoldPoliciesManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves a list of legal hold policies that belong to
        /// an enterprise.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getLegalHoldPolicies method
        /// </param>
        /// <param name="headers">
        /// Headers of getLegalHoldPolicies method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<LegalHoldPolicies> GetLegalHoldPoliciesAsync(GetLegalHoldPoliciesQueryParams? queryParams = default, GetLegalHoldPoliciesHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetLegalHoldPoliciesQueryParams();
            headers = headers ?? new GetLegalHoldPoliciesHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "policy_name", StringUtils.ToStringRepresentation(queryParams.PolicyName) }, { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/legal_hold_policies"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<LegalHoldPolicies>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Create a new legal hold policy.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createLegalHoldPolicy method
        /// </param>
        /// <param name="headers">
        /// Headers of createLegalHoldPolicy method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<LegalHoldPolicy> CreateLegalHoldPolicyAsync(CreateLegalHoldPolicyRequestBody requestBody, CreateLegalHoldPolicyHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateLegalHoldPolicyHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/legal_hold_policies"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<LegalHoldPolicy>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieve a legal hold policy.
        /// </summary>
        /// <param name="legalHoldPolicyId">
        /// The ID of the legal hold policy.
        /// Example: "324432"
        /// </param>
        /// <param name="headers">
        /// Headers of getLegalHoldPolicyById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<LegalHoldPolicy> GetLegalHoldPolicyByIdAsync(string legalHoldPolicyId, GetLegalHoldPolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetLegalHoldPolicyByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/legal_hold_policies/", StringUtils.ToStringRepresentation(legalHoldPolicyId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<LegalHoldPolicy>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Update legal hold policy.
        /// </summary>
        /// <param name="legalHoldPolicyId">
        /// The ID of the legal hold policy.
        /// Example: "324432"
        /// </param>
        /// <param name="requestBody">
        /// Request body of updateLegalHoldPolicyById method
        /// </param>
        /// <param name="headers">
        /// Headers of updateLegalHoldPolicyById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<LegalHoldPolicy> UpdateLegalHoldPolicyByIdAsync(string legalHoldPolicyId, UpdateLegalHoldPolicyByIdRequestBody? requestBody = default, UpdateLegalHoldPolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            requestBody = requestBody ?? new UpdateLegalHoldPolicyByIdRequestBody();
            headers = headers ?? new UpdateLegalHoldPolicyByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/legal_hold_policies/", StringUtils.ToStringRepresentation(legalHoldPolicyId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<LegalHoldPolicy>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Delete an existing legal hold policy.
        /// 
        /// This is an asynchronous process. The policy will not be
        /// fully deleted yet when the response returns.
        /// </summary>
        /// <param name="legalHoldPolicyId">
        /// The ID of the legal hold policy.
        /// Example: "324432"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteLegalHoldPolicyById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteLegalHoldPolicyByIdAsync(string legalHoldPolicyId, DeleteLegalHoldPolicyByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteLegalHoldPolicyByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/legal_hold_policies/", StringUtils.ToStringRepresentation(legalHoldPolicyId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}