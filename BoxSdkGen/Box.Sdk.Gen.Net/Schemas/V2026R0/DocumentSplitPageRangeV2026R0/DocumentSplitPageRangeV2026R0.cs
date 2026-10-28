using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitPageRangeV2026R0 : ISerializable {
        /// <summary>
        /// The first page (inclusive) included in this slice.
        /// </summary>
        [JsonPropertyName("start")]
        public int Start { get; }

        /// <summary>
        /// The last page (inclusive) included in this slice.
        /// </summary>
        [JsonPropertyName("end")]
        public int End { get; }

        public DocumentSplitPageRangeV2026R0(int start, int end) {
            Start = start;
            End = end;
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