using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiExtractStructured : ISerializable {
        /// <summary>
        /// The items to be processed by the LLM. Currently you can use files only.
        /// </summary>
        [JsonPropertyName("items")]
        public IReadOnlyList<AiItemBase> Items { get; }

        /// <summary>
        /// The metadata template containing the fields to extract.
        /// For your request to work, you must provide either `metadata_template` or `fields`, but not both.
        /// </summary>
        [JsonPropertyName("metadata_template")]
        public AiExtractStructuredMetadataTemplateField? MetadataTemplate { get; init; }

        /// <summary>
        /// The fields to be extracted from the provided items.
        /// For your request to work, you must provide either `metadata_template` or `fields`, but not both.
        /// </summary>
        [JsonPropertyName("fields")]
        public IReadOnlyList<AiExtractStructuredFieldsField>? Fields { get; init; }

        [JsonPropertyName("ai_agent")]
        public AiAgentExtractStructuredOrAiAgentReference? AiAgent { get; init; }

        public AiExtractStructured(IReadOnlyList<AiItemBase> items) {
            Items = items;
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