using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathMappingUpdateRequestV2026R0 : ISerializable {
        /// <summary>
        /// Whether this source path should be skipped during the migration.
        /// </summary>
        [JsonPropertyName("is_path_skipped")]
        public bool? IsPathSkipped { get; set; }

        [JsonPropertyName("source")]
        public ShuttlePathMappingLocationInputV2026R0 Source { get; set; }

        /// <summary>
        /// The destination location to migrate to. Set to `null` to clear the
        /// destination, for example when skipping the source path.
        /// </summary>
        [JsonPropertyName("target")]
        public ShuttlePathMappingTargetLocationInputV2026R0 Target { get; set; }

        public ShuttlePathMappingUpdateRequestV2026R0() {
            
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