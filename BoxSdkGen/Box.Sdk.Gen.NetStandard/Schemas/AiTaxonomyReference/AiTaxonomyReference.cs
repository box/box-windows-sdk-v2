using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AiTaxonomyReference : ISerializable {
        /// <summary>
        /// The type of the taxonomy source.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiTaxonomyReferenceTypeField>))]
        public StringEnum<AiTaxonomyReferenceTypeField> Type { get; set; }

        /// <summary>
        /// The identifier for a taxonomy, which corresponds to the `taxonomy_key` of the taxonomy source.
        /// </summary>
        [JsonPropertyName("taxonomy_key")]
        public string TaxonomyKey { get; set; }

        /// <summary>
        /// The namespace of the taxonomy source.
        /// </summary>
        [JsonPropertyName("namespace")]
        public string NamespaceParam { get; set; }

        public AiTaxonomyReference() {
            
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