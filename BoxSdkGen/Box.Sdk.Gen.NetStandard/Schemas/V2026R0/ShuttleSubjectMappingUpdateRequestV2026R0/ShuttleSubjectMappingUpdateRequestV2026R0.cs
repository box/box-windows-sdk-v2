using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleSubjectMappingUpdateRequestV2026R0 : ISerializable {
        /// <summary>
        /// Whether this subject should be skipped during the migration.
        /// </summary>
        [JsonPropertyName("is_subject_skipped")]
        public bool? IsSubjectSkipped { get; set; }

        /// <summary>
        /// The Box target to map to. Set to `null` only for a skipped subject.
        /// Setting it to `null` while the mapping is active fails validation.
        /// Updating `is_subject_skipped` alone preserves the existing target.
        /// </summary>
        [JsonPropertyName("target")]
        public ShuttleTargetSubjectInputV2026R0 Target { get; set; }

        public ShuttleSubjectMappingUpdateRequestV2026R0() {
            
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