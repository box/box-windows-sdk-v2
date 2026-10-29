using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleReportCreateRequestV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier of the shuttle run to generate a report for.
        /// </summary>
        [JsonPropertyName("run_id")]
        public string RunId { get; set; }

        /// <summary>
        /// The output file format of the report.
        /// </summary>
        [JsonPropertyName("format")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleReportCreateRequestV2026R0FormatField>))]
        public StringEnum<ShuttleReportCreateRequestV2026R0FormatField> Format { get; set; }

        /// <summary>
        /// The sections to include in the report. Must be a non-empty subset of
        /// the run's `available_report_sections`. This is an open list whose set
        /// of values is expected to grow; the currently documented values include
        /// `files_and_folders`, `event_log`, `transfer_overview`, `source_users`,
        /// `target_users`, `source_collaborators`, `target_collaborators`,
        /// `source_collaborations`, and `target_collaborations`.
        /// </summary>
        [JsonPropertyName("sections")]
        public IReadOnlyList<string> Sections { get; set; }

        /// <summary>
        /// Which users to include in user-related report sections. Defaults to
        /// `migrated` when omitted.
        /// </summary>
        [JsonPropertyName("users_scope")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleReportCreateRequestV2026R0UsersScopeField>))]
        public StringEnum<ShuttleReportCreateRequestV2026R0UsersScopeField> UsersScope { get; set; }

        public ShuttleReportCreateRequestV2026R0(string runId, ShuttleReportCreateRequestV2026R0FormatField format, IReadOnlyList<string> sections) {
            RunId = runId;
            Format = format;
            Sections = sections;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleReportCreateRequestV2026R0(string runId, StringEnum<ShuttleReportCreateRequestV2026R0FormatField> format, IReadOnlyList<string> sections) {
            RunId = runId;
            Format = format;
            Sections = sections;
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