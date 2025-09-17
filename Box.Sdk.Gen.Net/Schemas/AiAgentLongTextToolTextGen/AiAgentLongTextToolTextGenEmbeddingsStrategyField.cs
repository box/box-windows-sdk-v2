using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentLongTextToolTextGenEmbeddingsStrategyField : ISerializable {
        /// <summary>
        /// The strategy used for the AI agent for calculating embeddings.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The number of tokens per chunk.
        /// </summary>
        [JsonPropertyName("num_tokens_per_chunk")]
        public long? NumTokensPerChunk { get; init; }

        public AiAgentLongTextToolTextGenEmbeddingsStrategyField() {
            
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