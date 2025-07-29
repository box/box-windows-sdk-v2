using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class AiStudioManager : IAiStudioManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public AiStudioManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
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
        public async System.Threading.Tasks.Task<AiMultipleAgentResponse> GetAiAgentsAsync(GetAiAgentsQueryParams? queryParams = default, GetAiAgentsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetAiAgentsQueryParams();
            headers = headers ?? new GetAiAgentsHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "mode", StringUtils.ToStringRepresentation(queryParams.Mode) }, { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) }, { "agent_state", StringUtils.ToStringRepresentation(queryParams.AgentState) }, { "include_box_default", StringUtils.ToStringRepresentation(queryParams.IncludeBoxDefault) }, { "marker", StringUtils.ToStringRepresentation(queryParams.Marker) }, { "limit", StringUtils.ToStringRepresentation(queryParams.Limit) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai_agents"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<AiMultipleAgentResponse>(NullableUtils.Unwrap(response.Data));
        }

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
        public async System.Threading.Tasks.Task<AiSingleAgentResponseFull> CreateAiAgentAsync(CreateAiAgent requestBody, CreateAiAgentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateAiAgentHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai_agents"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<AiSingleAgentResponseFull>(NullableUtils.Unwrap(response.Data));
        }

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
        public async System.Threading.Tasks.Task<AiSingleAgentResponseFull> UpdateAiAgentByIdAsync(string agentId, CreateAiAgent requestBody, UpdateAiAgentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new UpdateAiAgentByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai_agents/", StringUtils.ToStringRepresentation(agentId)), method: "PUT", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<AiSingleAgentResponseFull>(NullableUtils.Unwrap(response.Data));
        }

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
        public async System.Threading.Tasks.Task<AiSingleAgentResponseFull> GetAiAgentByIdAsync(string agentId, GetAiAgentByIdQueryParams? queryParams = default, GetAiAgentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            queryParams = queryParams ?? new GetAiAgentByIdQueryParams();
            headers = headers ?? new GetAiAgentByIdHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "fields", StringUtils.ToStringRepresentation(queryParams.Fields) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai_agents/", StringUtils.ToStringRepresentation(agentId)), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<AiSingleAgentResponseFull>(NullableUtils.Unwrap(response.Data));
        }

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
        public async System.Threading.Tasks.Task DeleteAiAgentByIdAsync(string agentId, DeleteAiAgentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new DeleteAiAgentByIdHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai_agents/", StringUtils.ToStringRepresentation(agentId)), method: "DELETE", responseFormat: Box.Sdk.Gen.ResponseFormat.NoContent) { Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
        }

    }
}