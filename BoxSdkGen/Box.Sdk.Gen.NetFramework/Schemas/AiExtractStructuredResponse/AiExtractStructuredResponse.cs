using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiExtractStructuredResponse : ISerializable {
        [JsonPropertyName("answer")]
        public AiExtractResponse Answer { get; set; }

        /// <summary>
        /// The ISO date formatted timestamp of when the answer to the prompt was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The reason the response finishes.
        /// </summary>
        [JsonPropertyName("completion_reason")]
        public string CompletionReason { get; set; }

        [JsonPropertyName("ai_agent_info")]
        public AiAgentInfo AiAgentInfo { get; set; }

        public AiExtractStructuredResponse(AiExtractResponse answer, System.DateTimeOffset createdAt) {
            Answer = answer;
            CreatedAt = createdAt;
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