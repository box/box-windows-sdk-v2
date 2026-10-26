using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobV2026R0SettingsField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isbandwidth_limit_bytes_per_secSet")]
        protected bool _isBandwidthLimitBytesPerSecSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_ismax_file_versionsSet")]
        protected bool _isMaxFileVersionsSet { get; set; }

        protected long? _bandwidthLimitBytesPerSec { get; set; }

        protected int? _maxFileVersions { get; set; }

        /// <summary>
        /// The maximum transfer bandwidth in bytes per second. `null` when
        /// unlimited.
        /// </summary>
        [JsonPropertyName("bandwidth_limit_bytes_per_sec")]
        public long? BandwidthLimitBytesPerSec { get => _bandwidthLimitBytesPerSec; init { _bandwidthLimitBytesPerSec = value; _isBandwidthLimitBytesPerSecSet = true; } }

        /// <summary>
        /// Whether deletions in the source system are mirrored in Box.
        /// </summary>
        [JsonPropertyName("are_deletions_mirrored")]
        public bool? AreDeletionsMirrored { get; init; }

        /// <summary>
        /// Whether prior file versions are synced during the migration.
        /// </summary>
        [JsonPropertyName("are_file_versions_synced")]
        public bool? AreFileVersionsSynced { get; init; }

        /// <summary>
        /// The maximum number of file versions to sync. `null` when unlimited.
        /// </summary>
        [JsonPropertyName("max_file_versions")]
        public int? MaxFileVersions { get => _maxFileVersions; init { _maxFileVersions = value; _isMaxFileVersionsSet = true; } }

        /// <summary>
        /// Whether existing files in Box are overwritten.
        /// </summary>
        [JsonPropertyName("is_overwrite_enabled")]
        public bool? IsOverwriteEnabled { get; init; }

        /// <summary>
        /// Whether mappings that already completed successfully are skipped when
        /// the job is rerun.
        /// </summary>
        [JsonPropertyName("are_completed_mappings_skipped_on_rerun")]
        public bool? AreCompletedMappingsSkippedOnRerun { get; init; }

        /// <summary>
        /// Whether migrated content is scanned during the migration. Applies
        /// only when the source system supports content scanning; ignored
        /// otherwise.
        /// </summary>
        [JsonPropertyName("is_content_scanning_enabled")]
        public bool? IsContentScanningEnabled { get; init; }

        /// <summary>
        /// Whether shared folders on the source are included in the migration.
        /// Applies only when the source system supports shared folders; ignored
        /// otherwise.
        /// </summary>
        [JsonPropertyName("include_shared_source_folders")]
        public bool? IncludeSharedSourceFolders { get; init; }

        /// <summary>
        /// Whether shared folders on the destination are included in the
        /// migration. Applies only when the destination system supports shared
        /// folders; ignored otherwise.
        /// </summary>
        [JsonPropertyName("include_shared_destination_folders")]
        public bool? IncludeSharedDestinationFolders { get; init; }

        public ShuttleJobV2026R0SettingsField() {
            
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