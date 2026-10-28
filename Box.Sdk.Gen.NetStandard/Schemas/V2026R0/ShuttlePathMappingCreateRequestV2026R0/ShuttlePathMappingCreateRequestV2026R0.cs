using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathMappingCreateRequestV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier of the shuttle job this mapping belongs to.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; set; }

        /// <summary>
        /// Whether this source path should be skipped during the migration.
        /// Defaults to `false`.
        /// </summary>
        [JsonPropertyName("is_path_skipped")]
        public bool? IsPathSkipped { get; set; }

        [JsonPropertyName("source")]
        public ShuttlePathMappingLocationInputV2026R0 Source { get; set; }

        /// <summary>
        /// The destination location to migrate to. Omit or set to `null` when the
        /// source path is skipped.
        /// </summary>
        [JsonPropertyName("target")]
        public ShuttlePathMappingTargetLocationInputV2026R0 Target { get; set; }

        public ShuttlePathMappingCreateRequestV2026R0(string jobId, ShuttlePathMappingLocationInputV2026R0 source) {
            JobId = jobId;
            Source = source;
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