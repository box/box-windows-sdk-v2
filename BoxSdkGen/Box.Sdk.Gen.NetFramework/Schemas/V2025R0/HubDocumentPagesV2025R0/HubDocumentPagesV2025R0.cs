using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubDocumentPagesV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isnext_markerSet")]
        protected bool _isNextMarkerSet { get; set; }

        protected string _nextMarker { get; set; }

        /// <summary>
        /// Ordered list of pages.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<HubDocumentPageV2025R0> Entries { get; set; }

        /// <summary>
        /// The value will always be `document_pages`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<HubDocumentPagesV2025R0TypeField>))]
        public StringEnum<HubDocumentPagesV2025R0TypeField> Type { get; set; }

        /// <summary>
        /// The limit that was used for these entries. This will be the same as the
        /// `limit` query parameter unless that value exceeded the maximum value
        /// allowed. The maximum value varies by API.
        /// </summary>
        [JsonPropertyName("limit")]
        public long? Limit { get; set; }

        /// <summary>
        /// The marker for the start of the next page of results.
        /// </summary>
        [JsonPropertyName("next_marker")]
        public string NextMarker { get => _nextMarker; set { _nextMarker = value; _isNextMarkerSet = true; } }

        public HubDocumentPagesV2025R0(IReadOnlyList<HubDocumentPageV2025R0> entries, HubDocumentPagesV2025R0TypeField type = HubDocumentPagesV2025R0TypeField.DocumentPages) {
            Entries = entries;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal HubDocumentPagesV2025R0(IReadOnlyList<HubDocumentPageV2025R0> entries, StringEnum<HubDocumentPagesV2025R0TypeField> type) {
            Entries = entries;
            Type = HubDocumentPagesV2025R0TypeField.DocumentPages;
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