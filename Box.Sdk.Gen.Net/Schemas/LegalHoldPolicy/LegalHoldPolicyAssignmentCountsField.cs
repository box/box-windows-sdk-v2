using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class LegalHoldPolicyAssignmentCountsField : ISerializable {
        /// <summary>
        /// The number of users this policy is applied to.
        /// </summary>
        [JsonPropertyName("user")]
        public long? User { get; init; }

        /// <summary>
        /// The number of folders this policy is applied to.
        /// </summary>
        [JsonPropertyName("folder")]
        public long? Folder { get; init; }

        /// <summary>
        /// The number of files this policy is applied to.
        /// </summary>
        [JsonPropertyName("file")]
        public long? File { get; init; }

        /// <summary>
        /// The number of file versions this policy is applied to.
        /// </summary>
        [JsonPropertyName("file_version")]
        public long? FileVersion { get; init; }

        public LegalHoldPolicyAssignmentCountsField() {
            
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