using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleReportV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_iscompleted_atSet")]
        protected bool _isCompletedAtSet { get; set; }

        protected System.DateTimeOffset? _completedAt { get; set; }

        /// <summary>
        /// The unique identifier for the shuttle report.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `shuttle_report`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleReportV2026R0TypeField>))]
        public StringEnum<ShuttleReportV2026R0TypeField> Type { get; }

        /// <summary>
        /// The unique identifier of the shuttle run this report was generated
        /// for.
        /// </summary>
        [JsonPropertyName("run_id")]
        public string RunId { get; }

        /// <summary>
        /// The generation status of the report. This is an open string whose set
        /// of values is expected to grow; the currently documented values include
        /// `pending`, `processing`, `complete`, `failed`, `canceled`, and
        /// `unknown`. Keep polling `pending` and `processing`. Stop on
        /// `complete`, `failed`, `canceled`, and `unknown`. Treat any value that
        /// is not recognized the same as `unknown`: do not fail parsing, and do
        /// not keep polling.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; }

        /// <summary>
        /// The output file format of the report.
        /// </summary>
        [JsonPropertyName("format")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleReportV2026R0FormatField>))]
        public StringEnum<ShuttleReportV2026R0FormatField> Format { get; }

        /// <summary>
        /// The sections included in the report. Echoes the sections requested on
        /// create. This is an open list whose set of values is expected to grow;
        /// the currently documented values include `files_and_folders`,
        /// `event_log`, `transfer_overview`, `source_users`, `target_users`,
        /// `source_collaborators`, `target_collaborators`,
        /// `source_collaborations`, and `target_collaborations`. Omitted on
        /// older reports that have no stored section list. New reports always
        /// include this field.
        /// </summary>
        [JsonPropertyName("sections")]
        public IReadOnlyList<string>? Sections { get; init; }

        /// <summary>
        /// Which users are included in user-related report sections.
        /// </summary>
        [JsonPropertyName("users_scope")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleReportV2026R0UsersScopeField>))]
        public StringEnum<ShuttleReportV2026R0UsersScopeField>? UsersScope { get; init; }

        /// <summary>
        /// The generated Box file or folder. Always present; `null` until
        /// `status` is `complete`.
        /// </summary>
        [JsonPropertyName("output")]
        public ShuttleReportOutputV2026R0? Output { get; init; }

        /// <summary>
        /// When report generation was requested.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// When report generation finished. Always present; `null` while
        /// generation is still in progress.
        /// </summary>
        [JsonPropertyName("completed_at")]
        public System.DateTimeOffset? CompletedAt { get => _completedAt; init { _completedAt = value; _isCompletedAtSet = true; } }

        public ShuttleReportV2026R0(string id, string runId, string status, ShuttleReportV2026R0FormatField format, ShuttleReportV2026R0TypeField type = ShuttleReportV2026R0TypeField.ShuttleReport) {
            Id = id;
            Type = type;
            RunId = runId;
            Status = status;
            Format = format;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleReportV2026R0(string id, string runId, string status, StringEnum<ShuttleReportV2026R0FormatField> format, StringEnum<ShuttleReportV2026R0TypeField> type) {
            Id = id;
            Type = ShuttleReportV2026R0TypeField.ShuttleReport;
            RunId = runId;
            Status = status;
            Format = format;
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