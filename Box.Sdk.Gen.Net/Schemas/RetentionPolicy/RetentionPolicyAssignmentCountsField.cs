using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class RetentionPolicyAssignmentCountsField : ISerializable {
        /// <summary>
        /// The number of enterprise assignments this policy has. The maximum value is 1.
        /// </summary>
        [JsonPropertyName("enterprise")]
        public long? Enterprise { get; init; }

        /// <summary>
        /// The number of folder assignments this policy has.
        /// </summary>
        [JsonPropertyName("folder")]
        public long? Folder { get; init; }

        /// <summary>
        /// The number of metadata template assignments this policy has.
        /// </summary>
        [JsonPropertyName("metadata_template")]
        public long? MetadataTemplate { get; init; }

        public RetentionPolicyAssignmentCountsField() {
            
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