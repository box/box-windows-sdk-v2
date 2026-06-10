using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AiTaxonomyFileReference : ISerializable {
        /// <summary>
        /// The type of the taxonomy source.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiTaxonomyFileReferenceTypeField>))]
        public StringEnum<AiTaxonomyFileReferenceTypeField>? Type { get; init; }

        /// <summary>
        /// The identifier for a taxonomy, which corresponds to the `taxonomy_key` of the taxonomy source.
        /// </summary>
        [JsonPropertyName("taxonomy_key")]
        public string? TaxonomyKey { get; init; }

        /// <summary>
        /// The ID of the taxonomy source. Required if the type is `file` and unsupported if the type is `taxonomy`.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        public AiTaxonomyFileReference() {
            
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