using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleRunV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isended_atSet")]
        protected bool _isEndedAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isduration_secondsSet")]
        protected bool _isDurationSecondsSet { get; set; }

        protected System.DateTimeOffset? _endedAt { get; set; }

        protected long? _durationSeconds { get; set; }

        /// <summary>
        /// The unique identifier for the shuttle run, in the form
        /// `{job_id}:{run_number}`.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `shuttle_run`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleRunV2026R0TypeField>))]
        public StringEnum<ShuttleRunV2026R0TypeField> Type { get; }

        /// <summary>
        /// The unique identifier of the shuttle job this run belongs to.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; }

        /// <summary>
        /// The mode the run was started in. This is an open string whose set of
        /// values is expected to grow; the currently documented values include
        /// `transfer`, `simulation`, `analysis`, `scan`, and `unknown`. Clients
        /// should treat any value they do not recognize as `unknown`.
        /// </summary>
        [JsonPropertyName("run_mode")]
        public string RunMode { get; }

        /// <summary>
        /// The current state of the shuttle run. This is an open string whose set
        /// of values is expected to grow; the currently documented values include
        /// `running`, `stopping`, `completed`, `completed_with_errors`, `stopped`,
        /// `failed`, `timed_out`, and `unknown`. Clients should treat any value
        /// they do not recognize as `unknown`.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; }

        /// <summary>
        /// When the run started.
        /// </summary>
        [JsonPropertyName("started_at")]
        public System.DateTimeOffset? StartedAt { get; init; }

        /// <summary>
        /// When the run ended. `null` while the run is still in progress.
        /// </summary>
        [JsonPropertyName("ended_at")]
        public System.DateTimeOffset? EndedAt { get => _endedAt; init { _endedAt = value; _isEndedAtSet = true; } }

        /// <summary>
        /// Total duration of the run in seconds. `null` while the run is still in
        /// progress.
        /// </summary>
        [JsonPropertyName("duration_seconds")]
        public long? DurationSeconds { get => _durationSeconds; init { _durationSeconds = value; _isDurationSecondsSet = true; } }

        /// <summary>
        /// Transfer statistics collected during the run.
        /// </summary>
        [JsonPropertyName("stats")]
        public ShuttleRunV2026R0StatsField? Stats { get; init; }

        /// <summary>
        /// The user who started the run. `null` for schedule- or system-initiated
        /// runs.
        /// </summary>
        [JsonPropertyName("created_by")]
        public UserBaseV2026R0? CreatedBy { get; init; }

        /// <summary>
        /// Report sections that can be requested for this run. This is an open
        /// list whose set of values is expected to grow; the currently documented
        /// values include `files_and_folders`, `event_log`, `transfer_overview`,
        /// `source_users`, `target_users`, `source_collaborators`,
        /// `target_collaborators`, `source_collaborations`, and
        /// `target_collaborations`.
        /// </summary>
        [JsonPropertyName("available_report_sections")]
        public IReadOnlyList<string>? AvailableReportSections { get; init; }

        public ShuttleRunV2026R0(string id, string jobId, string runMode, string state, ShuttleRunV2026R0TypeField type = ShuttleRunV2026R0TypeField.ShuttleRun) {
            Id = id;
            Type = type;
            JobId = jobId;
            RunMode = runMode;
            State = state;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleRunV2026R0(string id, string jobId, string runMode, string state, StringEnum<ShuttleRunV2026R0TypeField> type) {
            Id = id;
            Type = ShuttleRunV2026R0TypeField.ShuttleRun;
            JobId = jobId;
            RunMode = runMode;
            State = state;
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