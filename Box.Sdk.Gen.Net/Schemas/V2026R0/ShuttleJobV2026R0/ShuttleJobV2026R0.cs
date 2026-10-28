using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_iscommentSet")]
        protected bool _isCommentSet { get; set; }

        protected string? _comment { get; set; }

        /// <summary>
        /// The unique identifier for the shuttle job.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `shuttle_job`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobV2026R0TypeField>))]
        public StringEnum<ShuttleJobV2026R0TypeField> Type { get; }

        /// <summary>
        /// The human-readable name of the shuttle job.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// An optional free-form comment describing the shuttle job.
        /// </summary>
        [JsonPropertyName("comment")]
        public string? Comment { get => _comment; init { _comment = value; _isCommentSet = true; } }

        /// <summary>
        /// The type of work the shuttle job performs.
        /// </summary>
        [JsonPropertyName("job_type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobV2026R0JobTypeField>))]
        public StringEnum<ShuttleJobV2026R0JobTypeField> JobType { get; }

        /// <summary>
        /// The current state of the shuttle job. This is an open string whose set
        /// of values is expected to grow; the currently documented values include
        /// `created`, `started`, `stopped`, `completed`, `failed`, `invalid`, and
        /// `unknown`. Clients should treat any value they do not recognize as
        /// `unknown`.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; }

        /// <summary>
        /// The current mapping-preparation state of the job. This is an open string
        /// whose set of values is expected to grow; the currently documented values
        /// include `awaiting_path_mapping`, `importing_paths`,
        /// `paths_import_rejected`, `paths_invalid`, `paths_ready`,
        /// `scanning_source`, `source_scan_failed`, `source_scan_out_of_date`,
        /// `awaiting_subject_mapping`, `importing_subjects`,
        /// `subjects_import_rejected`, `mapping_complete`, `building_map`,
        /// `map_build_failed`, `map_out_of_date`, and `ready`.
        /// `paths_import_rejected` means the last path-mapping spreadsheet was
        /// rejected or failed and the path mappings have not been changed since;
        /// it clears on the next import or the next manual path-mapping change.
        /// `subjects_import_rejected` means the last subject-mapping spreadsheet
        /// was rejected or failed and the subject mappings have not been changed
        /// since; it clears on the next import or the next manual subject-mapping
        /// change.
        /// </summary>
        [JsonPropertyName("mapping_state")]
        public string MappingState { get; }

        /// <summary>
        /// The source system the shuttle job migrates content from.
        /// </summary>
        [JsonPropertyName("source_system")]
        public ShuttleJobV2026R0SourceSystemField SourceSystem { get; }

        /// <summary>
        /// The account mapping configuration that controls how source identities
        /// and permissions are translated into Box.
        /// </summary>
        [JsonPropertyName("account_mapping")]
        public ShuttleJobV2026R0AccountMappingField? AccountMapping { get; init; }

        /// <summary>
        /// A list of free-form tags associated with the shuttle job.
        /// </summary>
        [JsonPropertyName("tags")]
        public IReadOnlyList<string>? Tags { get; init; }

        /// <summary>
        /// The tunable settings that control how the shuttle job runs.
        /// </summary>
        [JsonPropertyName("settings")]
        public ShuttleJobV2026R0SettingsField? Settings { get; init; }

        public ShuttleJobV2026R0(string id, string name, ShuttleJobV2026R0JobTypeField jobType, string state, string mappingState, ShuttleJobV2026R0SourceSystemField sourceSystem, ShuttleJobV2026R0TypeField type = ShuttleJobV2026R0TypeField.ShuttleJob) {
            Id = id;
            Type = type;
            Name = name;
            JobType = jobType;
            State = state;
            MappingState = mappingState;
            SourceSystem = sourceSystem;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleJobV2026R0(string id, string name, StringEnum<ShuttleJobV2026R0JobTypeField> jobType, string state, string mappingState, ShuttleJobV2026R0SourceSystemField sourceSystem, StringEnum<ShuttleJobV2026R0TypeField> type) {
            Id = id;
            Type = ShuttleJobV2026R0TypeField.ShuttleJob;
            Name = name;
            JobType = jobType;
            State = state;
            MappingState = mappingState;
            SourceSystem = sourceSystem;
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