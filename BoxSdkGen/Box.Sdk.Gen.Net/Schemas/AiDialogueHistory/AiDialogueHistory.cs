using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class AiDialogueHistory : ISerializable {
        /// <summary>
        /// The prompt previously provided by the client and answered by the LLM.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string? Prompt { get; init; }

        /// <summary>
        /// The answer previously provided by the LLM.
        /// </summary>
        [JsonPropertyName("answer")]
        public string? Answer { get; init; }

        /// <summary>
        /// The ISO date formatted timestamp of when the previous answer to the prompt was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        public AiDialogueHistory() {
            
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