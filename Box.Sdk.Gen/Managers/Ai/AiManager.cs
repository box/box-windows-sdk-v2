using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class AiManager : IAiManager {
        public IAuthentication? Auth { get; init; }

        public NetworkSession NetworkSession { get; }

        public AiManager(NetworkSession? networkSession = default) {
            NetworkSession = networkSession ?? new NetworkSession();
        }
        /// <summary>
        /// Sends an AI request to supported LLMs and returns an answer specifically focused on the user's question given the provided context.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createAiAsk method
        /// </param>
        /// <param name="headers">
        /// Headers of createAiAsk method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<AiResponseFull?> CreateAiAskAsync(AiAsk requestBody, CreateAiAskHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateAiAskHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai/ask"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            if (StringUtils.ToStringRepresentation(response.Status) == "204") {
                return null;
            }
            return SimpleJsonSerializer.Deserialize<AiResponseFull>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Sends an AI request to supported Large Language Models (LLMs) and returns generated text based on the provided prompt.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createAiTextGen method
        /// </param>
        /// <param name="headers">
        /// Headers of createAiTextGen method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<AiResponse> CreateAiTextGenAsync(AiTextGen requestBody, CreateAiTextGenHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateAiTextGenHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai/text_gen"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<AiResponse>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Get the AI agent default config.
        /// </summary>
        /// <param name="queryParams">
        /// Query parameters of getAiAgentDefaultConfig method
        /// </param>
        /// <param name="headers">
        /// Headers of getAiAgentDefaultConfig method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen> GetAiAgentDefaultConfigAsync(GetAiAgentDefaultConfigQueryParams queryParams, GetAiAgentDefaultConfigHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new GetAiAgentDefaultConfigHeaders();
            Dictionary<string, string> queryParamsMap = Utils.PrepareParams(map: new Dictionary<string, string?>() { { "mode", StringUtils.ToStringRepresentation(queryParams.Mode?.Value) }, { "language", StringUtils.ToStringRepresentation(queryParams.Language) }, { "model", StringUtils.ToStringRepresentation(queryParams.Model) } });
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai_agent_default"), method: "GET", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Parameters = queryParamsMap, Headers = headersMap, Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.DeserializeWithoutRawJson<AiAgentAskOrAiAgentExtractOrAiAgentExtractStructuredOrAiAgentTextGen>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Sends an AI request to supported Large Language Models (LLMs) and extracts metadata in form of key-value pairs.
        /// In this request, both the prompt and the output can be freeform.
        /// Metadata template setup before sending the request is not required.
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createAiExtract method
        /// </param>
        /// <param name="headers">
        /// Headers of createAiExtract method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<AiResponse> CreateAiExtractAsync(AiExtract requestBody, CreateAiExtractHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateAiExtractHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai/extract"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<AiResponse>(NullableUtils.Unwrap(response.Data));
        }

        /// <summary>
        /// Sends an AI request to supported Large Language Models (LLMs) and returns extracted metadata as a set of key-value pairs.
        /// For this request, you either need a metadata template or a list of fields you want to extract.
        /// Input is **either** a metadata template or a list of fields to ensure the structure.
        /// To learn more about creating templates, see [Creating metadata templates in the Admin Console](https://support.box.com/hc/en-us/articles/360044194033-Customizing-Metadata-Templates)
        /// or use the [metadata template API](g://metadata/templates/create).
        /// </summary>
        /// <param name="requestBody">
        /// Request body of createAiExtractStructured method
        /// </param>
        /// <param name="headers">
        /// Headers of createAiExtractStructured method
        /// </param>
        /// <param name="cancellationToken">
        /// Token used for request cancellation.
        /// </param>
        public async System.Threading.Tasks.Task<AiExtractStructuredResponse> CreateAiExtractStructuredAsync(AiExtractStructured requestBody, CreateAiExtractStructuredHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) {
            headers = headers ?? new CreateAiExtractStructuredHeaders();
            Dictionary<string, string> headersMap = Utils.PrepareParams(map: DictionaryUtils.MergeDictionaries(new Dictionary<string, string?>() {  }, headers.ExtraHeaders));
            FetchResponse response = await this.NetworkSession.NetworkClient.FetchAsync(options: new FetchOptions(url: string.Concat(this.NetworkSession.BaseUrls.BaseUrl, "/2.0/ai/extract_structured"), method: "POST", contentType: "application/json", responseFormat: Box.Sdk.Gen.ResponseFormat.Json) { Headers = headersMap, Data = SimpleJsonSerializer.Serialize(requestBody), Auth = this.Auth, NetworkSession = this.NetworkSession, CancellationToken = cancellationToken }).ConfigureAwait(false);
            return SimpleJsonSerializer.Deserialize<AiExtractStructuredResponse>(NullableUtils.Unwrap(response.Data));
        }

    }
}