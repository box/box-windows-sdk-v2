using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentBasicTextToolBase : ISerializable {
        /// <summary>
        /// The model used for the AI agent for basic text. For specific model values, see the [available models list](g://box-ai/supported-models).
        /// </summary>
        [JsonPropertyName("model")]
        public string? Model { get; init; }

        /// <summary>
        /// The number of tokens for completion.
        /// </summary>
        [JsonPropertyName("num_tokens_for_completion")]
        public long? NumTokensForCompletion { get; init; }

        [JsonPropertyName("llm_endpoint_params")]
        public AiLlmEndpointParams? LlmEndpointParams { get; init; }

        public AiAgentBasicTextToolBase() {
            
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}