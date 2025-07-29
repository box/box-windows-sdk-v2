using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IWorkflowsManager {
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
    public System.Threading.Tasks.Task<Workflows> GetWorkflowsAsync(GetWorkflowsQueryParams queryParams, GetWorkflowsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

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
    public System.Threading.Tasks.Task StartWorkflowAsync(string workflowId, StartWorkflowRequestBody requestBody, StartWorkflowHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}