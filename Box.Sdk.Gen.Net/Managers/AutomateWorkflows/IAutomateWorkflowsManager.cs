using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Parameters;

namespace Box.Sdk.Gen.Managers {
    public interface IAutomateWorkflowsManager {
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
    public System.Threading.Tasks.Task<AutomateWorkflowsV2026R0> GetAutomateWorkflowsV2026R0Async(GetAutomateWorkflowsV2026R0QueryParams queryParams, GetAutomateWorkflowsV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

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
    public System.Threading.Tasks.Task CreateAutomateWorkflowStartV2026R0Async(string workflowId, AutomateWorkflowStartRequestV2026R0 requestBody, CreateAutomateWorkflowStartV2026R0Headers? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}