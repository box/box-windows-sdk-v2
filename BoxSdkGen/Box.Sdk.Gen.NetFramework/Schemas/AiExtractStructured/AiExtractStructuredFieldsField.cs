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
        public string Key { get; set; }

        /// <summary>
        /// A description of the field.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// The display name of the field.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The context about the key that may include how to find and format it.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        /// <summary>
        /// The type of the field. It can include but is not limited to `string`, `float`, `date`, `enum`, `multiSelect`,`taxonomy`, `struct`, and `table`.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// A list of options for this field. This is most often used in combination with the `enum` and `multiSelect` field types.
        /// </summary>
        [JsonPropertyName("options")]
        public IReadOnlyList<AiExtractStructuredFieldsOptionsField> Options { get; set; }

        /// <summary>
        /// The nested fields for this field. Used with `struct` and `table` field types to define the nested structure.
        /// </summary>
        [JsonPropertyName("fields")]
        public IReadOnlyList<AiExtractSubField> Fields { get; set; }

        /// <summary>
        /// The identifier for a taxonomy, which corresponds to the `key` of the taxonomy source. Required if using `taxonomy` type field.
        /// </summary>
        [JsonPropertyName("taxonomy_key")]
        public string TaxonomyKey { get; set; }

        /// <summary>
        /// The namespace of the taxonomy source. Required if using `taxonomy` type field from an existing taxonomy.
        /// </summary>
        [JsonPropertyName("namespace")]
        public string NamespaceParam { get; set; }

        [JsonPropertyName("options_rules")]
        public AiOptionsRules OptionsRules { get; set; }

        public AiExtractStructuredFieldsField(string key) {
            Key = key;
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