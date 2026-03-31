using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubDocumentBlocksV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isnext_markerSet")]
        protected bool _isNextMarkerSet { get; set; }

        protected string _nextMarker { get; set; }

        /// <summary>
        /// Ordered list of blocks.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<HubDocumentBlockEntryV2025R0> Entries { get; set; }

        /// <summary>
        /// The value will always be `document_blocks`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<HubDocumentBlocksV2025R0TypeField>))]
        public StringEnum<HubDocumentBlocksV2025R0TypeField> Type { get; set; }

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

        public HubDocumentBlocksV2025R0(IReadOnlyList<HubDocumentBlockEntryV2025R0> entries, HubDocumentBlocksV2025R0TypeField type = HubDocumentBlocksV2025R0TypeField.DocumentBlocks) {
            Entries = entries;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal HubDocumentBlocksV2025R0(IReadOnlyList<HubDocumentBlockEntryV2025R0> entries, StringEnum<HubDocumentBlocksV2025R0TypeField> type) {
            Entries = entries;
            Type = HubDocumentBlocksV2025R0TypeField.DocumentBlocks;
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