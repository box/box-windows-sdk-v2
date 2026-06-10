using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiExtractSubField : ISerializable {
        /// <summary>
        /// A unique identifier for the nested field.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; }

        /// <summary>
        /// A description of the nested field.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// The display name of the nested field.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; init; }

        /// <summary>
        /// Context about the nested field that may include how to find and how to format it.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string? Prompt { get; init; }

        /// <summary>
        /// The type of the nested field. Allowed types include `string`, `float`, `date`, `number`, `text`, `boolean`, `enum` and `multiSelect`.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        /// <summary>
        /// A list of options for this nested field. Used with `enum` and `multiSelect` types.
        /// </summary>
        [JsonPropertyName("options")]
        public IReadOnlyList<AiExtractFieldOption>? Options { get; init; }

        public AiExtractSubField(string key) {
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