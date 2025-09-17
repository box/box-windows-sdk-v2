using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentSpreadsheetTool : ISerializable {
        /// <summary>
        /// The model used for the AI agent for spreadsheets. For specific model values, see the [available models list](g://box-ai/supported-models).
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// The number of tokens for completion.
        /// </summary>
        [JsonPropertyName("num_tokens_for_completion")]
        public long? NumTokensForCompletion { get; set; }

        [JsonPropertyName("llm_endpoint_params")]
        public AiLlmEndpointParams LlmEndpointParams { get; set; }

        public AiAgentSpreadsheetTool() {
            
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}