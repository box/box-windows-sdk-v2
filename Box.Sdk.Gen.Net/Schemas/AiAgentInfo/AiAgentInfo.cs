using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentInfo : ISerializable {
        /// <summary>
        /// The models used for the request.
        /// </summary>
        [JsonPropertyName("models")]
        public IReadOnlyList<AiAgentInfoModelsField>? Models { get; init; }

        /// <summary>
        /// The processor used for the request.
        /// </summary>
        [JsonPropertyName("processor")]
        public string? Processor { get; init; }

        public AiAgentInfo() {
            
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