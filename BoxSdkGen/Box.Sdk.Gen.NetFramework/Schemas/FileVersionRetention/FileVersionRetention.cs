using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileVersionRetention : ISerializable {
        /// <summary>
        /// The unique identifier for this file version retention.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `file_version_retention`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<FileVersionRetentionTypeField>))]
        public StringEnum<FileVersionRetentionTypeField> Type { get; set; }

        [JsonPropertyName("file_version")]
        public FileVersionMini FileVersion { get; set; }

        [JsonPropertyName("file")]
        public FileMini File { get; set; }

        /// <summary>
        /// When this file version retention object was
        /// created.
        /// </summary>
        [JsonPropertyName("applied_at")]
        public System.DateTimeOffset? AppliedAt { get; set; }

        /// <summary>
        /// When the retention expires on this file
        /// version retention.
        /// </summary>
        [JsonPropertyName("disposition_at")]
        public System.DateTimeOffset? DispositionAt { get; set; }

        [JsonPropertyName("winning_retention_policy")]
        public RetentionPolicyMini WinningRetentionPolicy { get; set; }

        public FileVersionRetention() {
            
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