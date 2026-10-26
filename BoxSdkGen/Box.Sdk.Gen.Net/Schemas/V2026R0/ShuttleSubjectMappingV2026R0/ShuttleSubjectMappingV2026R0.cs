using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleSubjectMappingV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdiscovered_subject_idSet")]
        protected bool _isDiscoveredSubjectIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isrevisionSet")]
        protected bool _isRevisionSet { get; set; }

        protected string? _discoveredSubjectId { get; set; }

        protected int? _revision { get; set; }

        /// <summary>
        /// The unique identifier for the shuttle subject mapping.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `shuttle_subject_mapping`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleSubjectMappingV2026R0TypeField>))]
        public StringEnum<ShuttleSubjectMappingV2026R0TypeField> Type { get; }

        /// <summary>
        /// The unique identifier of the shuttle job this mapping belongs to.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; }

        /// <summary>
        /// The unique identifier of the discovered subject this mapping applies to.
        /// `null` for legacy mappings created before this field was tracked.
        /// </summary>
        [JsonPropertyName("discovered_subject_id")]
        public string? DiscoveredSubjectId { get => _discoveredSubjectId; init { _discoveredSubjectId = value; _isDiscoveredSubjectIdSet = true; } }

        /// <summary>
        /// The discovery-requirements revision this mapping was resolved against.
        /// `null` for legacy mappings.
        /// </summary>
        [JsonPropertyName("revision")]
        public int? Revision { get => _revision; init { _revision = value; _isRevisionSet = true; } }

        /// <summary>
        /// Whether this subject is skipped during the migration.
        /// </summary>
        [JsonPropertyName("is_subject_skipped")]
        public bool IsSubjectSkipped { get; }

        [JsonPropertyName("source")]
        public ShuttleSubjectV2026R0 Source { get; }

        /// <summary>
        /// The Box target subject. `null` when the subject is skipped or not yet
        /// assigned.
        /// </summary>
        [JsonPropertyName("target")]
        public ShuttleTargetSubjectV2026R0? Target { get; init; }

        public ShuttleSubjectMappingV2026R0(string id, string jobId, bool isSubjectSkipped, ShuttleSubjectV2026R0 source, ShuttleSubjectMappingV2026R0TypeField type = ShuttleSubjectMappingV2026R0TypeField.ShuttleSubjectMapping) {
            Id = id;
            Type = type;
            JobId = jobId;
            IsSubjectSkipped = isSubjectSkipped;
            Source = source;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleSubjectMappingV2026R0(string id, string jobId, bool isSubjectSkipped, ShuttleSubjectV2026R0 source, StringEnum<ShuttleSubjectMappingV2026R0TypeField> type) {
            Id = id;
            Type = ShuttleSubjectMappingV2026R0TypeField.ShuttleSubjectMapping;
            JobId = jobId;
            IsSubjectSkipped = isSubjectSkipped;
            Source = source;
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