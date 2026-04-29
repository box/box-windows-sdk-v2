using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public class AutomateWorkflowsManager {
        public IAuthentication Auth { get; set; }

        public NetworkSession NetworkSession { get; set; }

        public AutomateWorkflowsManager(NetworkSession networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Returns workflow actions from Automate for a folder, using the
        /// `WORKFLOW` action category.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getAutomateWorkflowsV2026R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of getAutomateWorkflowsV2026R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<AutomateWorkflowsV2026R0> GetAutomateWorkflowsV2026R0Async(GetAutomateWorkflowsV2026R0QueryParams queryParams, GetAutomateWorkflowsV2026R0Headers headers = default, System.Threading.CancellationToken cancellationToken = default) {
            headers = headers ?? new GetAutomateWorkflowsV2026R0Headers();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string>() { { "folder_id", StringUtils.ToStringRepresentation(queryParams.FolderId) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/automate_workflows"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<AutomateWorkflowsV2026R0>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Starts an Automate workflow manually by using a workflow action ID and file IDs.
        /// </summary>
        /// <param name="workflowId">
        /// The ID of the workflow.
        /// Example: "12345"
        /// </param>
        /// <param name="requestBody">
        /// Request body of createAutomateWorkflowStartV2026R0 method
        /// </param>
        /// <param name="headers">
        /// Headers of createAutomateWorkflowStartV2026R0 method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task CreateAutomateWorkflowStartV2026R0Async(string workflowId, AutomateWorkflowStartRequestV2026R0 requestBody, CreateAutomateWorkflowStartV2026R0Headers headers = default, System.Threading.CancellationToken cancellationToken = default) {
            headers = headers ?? new CreateAutomateWorkflowStartV2026R0Headers();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string>() { { "box-version", StringUtils.ToStringRepresentation(headers.BoxVersion) } }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/automate_workflows/", NullableUtils.Unwrap(StringUtils.ToStringRepresentation(workflowId)), "/start"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}