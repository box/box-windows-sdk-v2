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
        public IReadOnlyList<AiItemBase> Items { get; set; }

        /// <summary>
        /// The metadata template containing the fields to extract.
        /// For your request to work, you must provide either `metadata_template` or `fields`, but not both.
        /// </summary>
        [JsonPropertyName("metadata_template")]
        public AiExtractStructuredMetadataTemplateField MetadataTemplate { get; set; }

        /// <summary>
        /// The fields to be extracted from the provided items.
        /// For your request to work, you must provide either `metadata_template` or `fields`, but not both.
        /// </summary>
        [JsonPropertyName("fields")]
        public IReadOnlyList<AiExtractStructuredFieldsField> Fields { get; set; }

        [JsonPropertyName("ai_agent")]
        public AiExtractStructuredAgent AiAgent { get; set; }

        /// <summary>
        /// A flag to indicate whether confidence scores for every extracted field should be returned. Estimates the likelihood that an extracted metadata field value is accurate and correct. Displays a numerical and categorical confidence score to help users and automated systems quickly determine extraction reliability.
        /// </summary>
        [JsonPropertyName("include_confidence_score")]
        public bool? IncludeConfidenceScore { get; set; }

        /// <summary>
        /// A flag to indicate whether references for every extracted field should be returned. References and bounding boxes show where the agent extracted the metadata from. They help you check for accuracy and fix any mistakes. References are short, exact quotes from the original document used to verify results. Bounding boxes highlight the specific areas on the page where that text is found.
        /// </summary>
        [JsonPropertyName("include_reference")]
        public bool? IncludeReference { get; set; }

        /// <summary>
        /// The taxonomy sources to be used for the structured extraction. They can either be an existing file or a taxonomy.
        /// For your request to work, `fields` must also be provided. `taxonomy_sources` is not supported with `metadata_template`.
        /// </summary>
        [JsonPropertyName("taxonomy_sources")]
        public IReadOnlyList<AiTaxonomySource> TaxonomySources { get; set; }

        public AiExtractStructured(IReadOnlyList<AiItemBase> items) {
            Items = items;
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