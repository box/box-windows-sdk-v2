using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ClassificationTemplateFieldsOptionsField : ISerializable {
        /// <summary>
        /// The unique ID of this classification.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The display name and key for this classification.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; }

        /// <summary>
        /// Additional information about the classification.
        /// </summary>
        [JsonPropertyName("staticConfig")]
        public ClassificationTemplateFieldsOptionsStaticConfigField? StaticConfig { get; init; }

        public ClassificationTemplateFieldsOptionsField(string id, string key) {
            Id = id;
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