using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleSubjectMappingCreateRequestV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier of the shuttle job this mapping belongs to.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; set; }

        /// <summary>
        /// The unique identifier of the discovered subject to map.
        /// </summary>
        [JsonPropertyName("discovered_subject_id")]
        public string DiscoveredSubjectId { get; set; }

        /// <summary>
        /// The discovery-requirements revision to resolve against. Omit to use the
        /// latest revision.
        /// </summary>
        [JsonPropertyName("revision")]
        public int? Revision { get; set; }

        /// <summary>
        /// Whether this subject should be skipped during the migration. Defaults
        /// to `false`.
        /// </summary>
        [JsonPropertyName("is_subject_skipped")]
        public bool? IsSubjectSkipped { get; set; }

        /// <summary>
        /// The Box target to map to. Omit or set to `null` when the subject is
        /// skipped.
        /// </summary>
        [JsonPropertyName("target")]
        public ShuttleTargetSubjectInputV2026R0 Target { get; set; }

        public ShuttleSubjectMappingCreateRequestV2026R0(string jobId, string discoveredSubjectId) {
            JobId = jobId;
            DiscoveredSubjectId = discoveredSubjectId;
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