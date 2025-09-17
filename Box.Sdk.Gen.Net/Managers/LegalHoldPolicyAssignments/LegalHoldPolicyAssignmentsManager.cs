using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class LegalHoldPolicyAssignmentsManager : ILegalHoldPolicyAssignmentsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public LegalHoldPolicyAssignmentsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Retrieves a list of items a legal hold policy has been assigned to.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getLegalHoldPolicyAssignments method
        /// </param>
        /// <param name="headers">
        /// Headers of getLegalHoldPolicyAssignments method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<LegalHoldPolicyAssignments> GetLegalHoldPolicyAssignmentsAsync(GetLegalHoldPolicyAssignmentsQueryParams queryParams, GetLegalHoldPolicyAssignmentsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetLegalHoldPolicyAssignmentsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "policy_id", StringUtils.ToStringRepresentation(queryParams.PolicyId) }, { "assign_to_type", StringUtils.ToStringRepresentation(queryParams.AssignToType) }, { "assign_to_id", StringUtils.ToStringRepresentation(queryParams.AssignToId) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/legal_hold_policy_assignments"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<LegalHoldPolicyAssignments>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Assign a legal hold to a file, file version, folder, or user.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createLegalHoldPolicyAssignment method
        /// </param>
        /// <param name="headers">
        /// Headers of createLegalHoldPolicyAssignment method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<LegalHoldPolicyAssignment> CreateLegalHoldPolicyAssignmentAsync(CreateLegalHoldPolicyAssignmentRequestBody requestBody, CreateLegalHoldPolicyAssignmentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateLegalHoldPolicyAssignmentHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/legal_hold_policy_assignments"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<LegalHoldPolicyAssignment>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Retrieve a legal hold policy assignment.
        /// </summary>
        /// <param name="legalHoldPolicyAssignmentId">
        /// The ID of the legal hold policy assignment.
        /// Example: "753465"
        /// </param>
        /// <param name="headers">
        /// Headers of getLegalHoldPolicyAssignmentById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<LegalHoldPolicyAssignment> GetLegalHoldPolicyAssignmentByIdAsync(string legalHoldPolicyAssignmentId, GetLegalHoldPolicyAssignmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetLegalHoldPolicyAssignmentByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/legal_hold_policy_assignments/", StringUtils.ToStringRepresentation(legalHoldPolicyAssignmentId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<LegalHoldPolicyAssignment>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Remove a legal hold from an item.
        /// 
        /// This is an asynchronous process. The policy will not be
        /// fully removed yet when the response returns.
        /// </summary>
        /// <param name="legalHoldPolicyAssignmentId">
        /// The ID of the legal hold policy assignment.
        /// Example: "753465"
        /// </param>
        /// <param name="headers">
        /// Headers of deleteLegalHoldPolicyAssignmentById method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task DeleteLegalHoldPolicyAssignmentByIdAsync(string legalHoldPolicyAssignmentId, DeleteLegalHoldPolicyAssignmentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteLegalHoldPolicyAssignmentByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/legal_hold_policy_assignments/", StringUtils.ToStringRepresentation(legalHoldPolicyAssignmentId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

        /// <summary>
        /// Get a list of files with current file versions for a legal hold
        /// assignment.
        /// 
        /// In some cases you may want to get previous file versions instead. In these
        /// cases, use the `GET  /legal_hold_policy_assignments/:id/file_versions_on_hold`
        /// API instead to return any previous versions of a file for this legal hold
        /// policy assignment.
        /// 
        /// Due to ongoing re-architecture efforts this API might not return all file
        /// versions held for this policy ID. Instead, this API will only return the
        /// latest file version held in the newly developed architecture. The `GET
        /// /file_version_legal_holds` API can be used to fetch current and past versions
        /// of files held within the legacy architecture.
        /// 
        /// This endpoint does not support returning any content that is on hold due to
        /// a Custodian collaborating on a Hub.
        /// 
        /// The `GET /legal_hold_policy_assignments?policy_id={id}` API can be used to
        /// find a list of policy assignments for a given policy ID.
        /// </summary>
        /// <param name="legalHoldPolicyAssignmentId">
        /// The ID of the legal hold policy assignment.
        /// Example: "753465"
        /// </param>
        /// <param name="queryParams">
        /// Query parameters of getLegalHoldPolicyAssignmentFileOnHold method
        /// </param>
        /// <param name="headers">
        /// Headers of getLegalHoldPolicyAssignmentFileOnHold method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<FilesOnHold> GetLegalHoldPolicyAssignmentFileOnHoldAsync(string legalHoldPolicyAssignmentId, GetLegalHoldPolicyAssignmentFileOnHoldQueryParams? queryParams = default, GetLegalHoldPolicyAssignmentFileOnHoldHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetLegalHoldPolicyAssignmentFileOnHoldQueryParams();
            headers = headers ?? new GetLegalHoldPolicyAssignmentFileOnHoldHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/legal_hold_policy_assignments/", StringUtils.ToStringRepresentation(legalHoldPolicyAssignmentId), "/files_on_hold"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<FilesOnHold>(NullableUtils.Unwrap(response.Data));
        }

    }
}