using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class AiExtractFieldOption : ISerializable {
        /// <summary>
        /// A unique identifier for the option.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; }

        public AiExtractFieldOption(string key) {
            Key = key;
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