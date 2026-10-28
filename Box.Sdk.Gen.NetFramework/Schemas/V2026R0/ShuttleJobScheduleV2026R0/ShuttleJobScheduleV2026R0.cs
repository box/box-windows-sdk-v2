using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobScheduleV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdescriptionSet")]
        protected bool _isDescriptionSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_islast_attempt_atSet")]
        protected bool _isLastAttemptAtSet { get; set; }

        protected string _description { get; set; }

        protected System.DateTimeOffset? _lastAttemptAt { get; set; }

        /// <summary>
        /// The unique identifier for the shuttle job schedule.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `shuttle_job_schedule`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleV2026R0TypeField>))]
        public StringEnum<ShuttleJobScheduleV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The unique identifier of the shuttle job this schedule belongs to.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; set; }

        /// <summary>
        /// Whether the schedule is active.
        /// </summary>
        [JsonPropertyName("state")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleV2026R0StateField>))]
        public StringEnum<ShuttleJobScheduleV2026R0StateField> State { get; set; }

        /// <summary>
        /// The kind of run the schedule starts. `scan` is not a valid schedule
        /// run mode.
        /// </summary>
        [JsonPropertyName("run_mode")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleV2026R0RunModeField>))]
        public StringEnum<ShuttleJobScheduleV2026R0RunModeField> RunMode { get; set; }

        /// <summary>
        /// An optional human-readable description of the schedule.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get => _description; set { _description = value; _isDescriptionSet = true; } }

        /// <summary>
        /// When the schedule last attempted to start a run. `null` when the
        /// schedule has never started a run.
        /// </summary>
        [JsonPropertyName("last_attempt_at")]
        public System.DateTimeOffset? LastAttemptAt { get => _lastAttemptAt; set { _lastAttemptAt = value; _isLastAttemptAtSet = true; } }

        [JsonPropertyName("trigger")]
        public ShuttleJobScheduleTriggerV2026R0 Trigger { get; set; }

        public ShuttleJobScheduleV2026R0(string id, string jobId, ShuttleJobScheduleV2026R0StateField state, ShuttleJobScheduleV2026R0RunModeField runMode, ShuttleJobScheduleTriggerV2026R0 trigger, ShuttleJobScheduleV2026R0TypeField type = ShuttleJobScheduleV2026R0TypeField.ShuttleJobSchedule) {
            Id = id;
            Type = type;
            JobId = jobId;
            State = state;
            RunMode = runMode;
            Trigger = trigger;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleJobScheduleV2026R0(string id, string jobId, StringEnum<ShuttleJobScheduleV2026R0StateField> state, StringEnum<ShuttleJobScheduleV2026R0RunModeField> runMode, ShuttleJobScheduleTriggerV2026R0 trigger, StringEnum<ShuttleJobScheduleV2026R0TypeField> type) {
            Id = id;
            Type = ShuttleJobScheduleV2026R0TypeField.ShuttleJobSchedule;
            JobId = jobId;
            State = state;
            RunMode = runMode;
            Trigger = trigger;
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