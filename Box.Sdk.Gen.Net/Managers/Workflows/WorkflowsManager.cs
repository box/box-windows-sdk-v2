using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class WorkflowsManager : IWorkflowsManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public WorkflowsManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Returns list of workflows that act on a given `folder ID`, and
        /// have a flow with a trigger type of `WORKFLOW_MANUAL_START`.
        /// 
        /// You application must be authorized to use the `Manage Box Relay` application
        /// scope within the developer console in to use this endpoint.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getWorkflows method
        /// </param>
        /// <param name="headers">
        /// Headers of getWorkflows method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<Workflows> GetWorkflowsAsync(GetWorkflowsQueryParams queryParams, GetWorkflowsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetWorkflowsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "folder_id", StringUtils.ToStringRepresentation(queryParams.FolderId) }, { "trigger_type", StringUtils.ToStringRepresentation(queryParams.TriggerType) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/workflows"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<Workflows>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Initiates a flow with a trigger type of `WORKFLOW_MANUAL_START`.
        /// 
        /// You application must be authorized to use the `Manage Box Relay` application
        /// scope within the developer console.
        /// </summary>
        /// <param name="workflowId">
        /// The ID of the workflow.
        /// Example: "12345"
        /// </param>
        /// <param name="requestBody">
        /// Request body of startWorkflow method
        /// </param>
        /// <param name="headers">
        /// Headers of startWorkflow method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task StartWorkflowAsync(string workflowId, StartWorkflowRequestBody requestBody, StartWorkflowHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new StartWorkflowHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/workflows/", StringUtils.ToStringRepresentation(workflowId), "/start"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}