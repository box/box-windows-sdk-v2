using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiExtractStructuredResponse : ISerializable {
        [JsonPropertyName("answer")]
        [JsonConverter(typeof(DictionaryObjectValuesConverter))]
        public Dictionary<string, object> Answer { get; }

        /// <summary>
        /// The ISO date formatted timestamp of when the answer to the prompt was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The reason the response finishes.
        /// </summary>
        [JsonPropertyName("completion_reason")]
        public string? CompletionReason { get; init; }

        [JsonPropertyName("ai_agent_info")]
        public AiAgentInfo? AiAgentInfo { get; init; }

        public AiExtractStructuredResponse(Dictionary<string, object> answer, System.DateTimeOffset createdAt) {
            Answer = answer;
            CreatedAt = createdAt;
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