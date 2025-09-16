using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldInformationBarrierReportDetailsDetailsField : ISerializable {
        /// <summary>
        /// Folder ID for locating this report.
        /// </summary>
        [JsonPropertyName("folder_id")]
        public string? FolderId { get; init; }

        public ShieldInformationBarrierReportDetailsDetailsField() {
            
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