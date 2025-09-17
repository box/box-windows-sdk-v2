using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiExtractStructuredFieldsField : ISerializable {
        /// <summary>
        /// A unique identifier for the field.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; }

        /// <summary>
        /// A description of the field.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// The display name of the field.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; init; }

        /// <summary>
        /// The context about the key that may include how to find and format it.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string? Prompt { get; init; }

        /// <summary>
        /// The type of the field. It include but is not limited to string, float, date, enum, and multiSelect.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        /// <summary>
        /// A list of options for this field. This is most often used in combination with the enum and multiSelect field types.
        /// </summary>
        [JsonPropertyName("options")]
        public IReadOnlyList<AiExtractStructuredFieldsOptionsField>? Options { get; init; }

        public AiExtractStructuredFieldsField(string key) {
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