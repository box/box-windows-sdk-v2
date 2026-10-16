using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitPageSelectionItemV2026R0 : ISerializable {
        /// <summary>
        /// Optional name for the output file (without extension). When omitted the
        /// system generates a sequential name based on the source file name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// An ordered list of page segments to include in this output file. Each
        /// segment specifies either a single page or a contiguous page range.
        /// </summary>
        [JsonPropertyName("segments")]
        public IReadOnlyList<DocumentSplitPageSelectionSegmentV2026R0> Segments { get; }

        public DocumentSplitPageSelectionItemV2026R0(IReadOnlyList<DocumentSplitPageSelectionSegmentV2026R0> segments) {
            Segments = segments;
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