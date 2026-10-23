using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitPageSelectionSinglePageV2026R0 : ISerializable {
        /// <summary>
        /// Discriminator. Must be `single_page` for this variant.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitPageSelectionSinglePageV2026R0TypeField>))]
        public StringEnum<DocumentSplitPageSelectionSinglePageV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// A single page number to include.
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }

        public DocumentSplitPageSelectionSinglePageV2026R0(int page, DocumentSplitPageSelectionSinglePageV2026R0TypeField type = DocumentSplitPageSelectionSinglePageV2026R0TypeField.SinglePage) {
            Type = type;
            Page = page;
        }
        
        [JsonConstructorAttribute]
        internal DocumentSplitPageSelectionSinglePageV2026R0(int page, StringEnum<DocumentSplitPageSelectionSinglePageV2026R0TypeField> type) {
            Type = DocumentSplitPageSelectionSinglePageV2026R0TypeField.SinglePage;
            Page = page;
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