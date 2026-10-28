using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathMappingV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier for the shuttle path mapping.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `shuttle_path_mapping`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttlePathMappingV2026R0TypeField>))]
        public StringEnum<ShuttlePathMappingV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The unique identifier of the shuttle job this mapping belongs to.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; set; }

        /// <summary>
        /// Whether this source path is skipped during the migration.
        /// </summary>
        [JsonPropertyName("is_path_skipped")]
        public bool IsPathSkipped { get; set; }

        [JsonPropertyName("source")]
        public ShuttlePathMappingLocationV2026R0 Source { get; set; }

        /// <summary>
        /// The destination location. `null` when the source path is skipped or
        /// the job has no destination, such as an analysis job.
        /// </summary>
        [JsonPropertyName("target")]
        public ShuttlePathMappingLocationV2026R0 Target { get; set; }

        public ShuttlePathMappingV2026R0(string id, string jobId, bool isPathSkipped, ShuttlePathMappingLocationV2026R0 source, ShuttlePathMappingV2026R0TypeField type = ShuttlePathMappingV2026R0TypeField.ShuttlePathMapping) {
            Id = id;
            Type = type;
            JobId = jobId;
            IsPathSkipped = isPathSkipped;
            Source = source;
        }
        
        [JsonConstructorAttribute]
        internal ShuttlePathMappingV2026R0(string id, string jobId, bool isPathSkipped, ShuttlePathMappingLocationV2026R0 source, StringEnum<ShuttlePathMappingV2026R0TypeField> type) {
            Id = id;
            Type = ShuttlePathMappingV2026R0TypeField.ShuttlePathMapping;
            JobId = jobId;
            IsPathSkipped = isPathSkipped;
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