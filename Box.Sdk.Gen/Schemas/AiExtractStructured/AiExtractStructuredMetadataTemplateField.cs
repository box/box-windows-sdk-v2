using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiExtractStructuredMetadataTemplateField : ISerializable {
        /// <summary>
        /// The name of the metadata template.
        /// </summary>
        [JsonPropertyName("template_key")]
        public string? TemplateKey { get; init; }

        /// <summary>
        /// Value is always `metadata_template`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiExtractStructuredMetadataTemplateTypeField>))]
        public StringEnum<AiExtractStructuredMetadataTemplateTypeField>? Type { get; init; }

        /// <summary>
        /// The scope of the metadata template that can either be global or
        /// enterprise.
        /// * The **global** scope is used for templates that are
        /// available to any Box enterprise.
        /// * The **enterprise** scope represents templates created within a specific enterprise,
        ///   containing the ID of that enterprise.
        /// </summary>
        [JsonPropertyName("scope")]
        public string? Scope { get; init; }

        public AiExtractStructuredMetadataTemplateField() {
            
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