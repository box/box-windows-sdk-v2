using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitPageSelectionPageRangeV2026R0 : ISerializable {
        /// <summary>
        /// Discriminator. Must be `page_range` for this variant.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitPageSelectionPageRangeV2026R0TypeField>))]
        public StringEnum<DocumentSplitPageSelectionPageRangeV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The first page (inclusive) included in this segment.
        /// </summary>
        [JsonPropertyName("start")]
        public int Start { get; set; }

        /// <summary>
        /// The last page (inclusive) included in this segment.
        /// </summary>
        [JsonPropertyName("end")]
        public int End { get; set; }

        public DocumentSplitPageSelectionPageRangeV2026R0(int start, int end, DocumentSplitPageSelectionPageRangeV2026R0TypeField type = DocumentSplitPageSelectionPageRangeV2026R0TypeField.PageRange) {
            Type = type;
            Start = start;
            End = end;
        }
        
        [JsonConstructorAttribute]
        internal DocumentSplitPageSelectionPageRangeV2026R0(int start, int end, StringEnum<DocumentSplitPageSelectionPageRangeV2026R0TypeField> type) {
            Type = DocumentSplitPageSelectionPageRangeV2026R0TypeField.PageRange;
            Start = start;
            End = end;
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