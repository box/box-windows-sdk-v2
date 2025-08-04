using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentInfoModelsField : ISerializable {
        /// <summary>
        /// The name of the model used for the request.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The provider that owns the model used for the request.
        /// </summary>
        [JsonPropertyName("provider")]
        public string? Provider { get; init; }

        /// <summary>
        /// The supported purpose utilized by the model used for the request.
        /// </summary>
        [JsonPropertyName("supported_purpose")]
        public string? SupportedPurpose { get; init; }

        public AiAgentInfoModelsField() {
            
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