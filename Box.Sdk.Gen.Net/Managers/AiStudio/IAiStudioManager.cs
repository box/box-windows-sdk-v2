using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface IAiStudioManager {
        /// <summary>
    /// Lists AI agents based on the provided parameters.
    /// </summary>
    /// <param name="queryParams">
    /// Query parameters of getAiAgents method
    /// </param>
    /// <param name="headers">
    /// Headers of getAiAgents method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<AiMultipleAgentResponse> GetAiAgentsAsync(GetAiAgentsQueryParams? queryParams = default, GetAiAgentsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Creates an AI agent. At least one of the following capabilities must be provided: `ask`, `text_gen`, `extract`.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createAiAgent method
    /// </param>
    /// <param name="headers">
    /// Headers of createAiAgent method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<AiSingleAgentResponseFull> CreateAiAgentAsync(CreateAiAgent requestBody, CreateAiAgentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Updates an AI agent.
    /// </summary>
    /// <param name="agentId">
    /// The ID of the agent to update.
    /// Example: "1234"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateAiAgentById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateAiAgentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<AiSingleAgentResponseFull> UpdateAiAgentByIdAsync(string agentId, CreateAiAgent requestBody, UpdateAiAgentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Gets an AI Agent using the `agent_id` parameter.
    /// </summary>
    /// <param name="agentId">
    /// The agent id to get.
    /// Example: "1234"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getAiAgentById method
    /// </param>
    /// <param name="headers">
    /// Headers of getAiAgentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<AiSingleAgentResponseFull> GetAiAgentByIdAsync(string agentId, GetAiAgentByIdQueryParams? queryParams = default, GetAiAgentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Deletes an AI agent using the provided parameters.
    /// </summary>
    /// <param name="agentId">
    /// The ID of the agent to delete.
    /// Example: "1234"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteAiAgentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteAiAgentByIdAsync(string agentId, DeleteAiAgentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}