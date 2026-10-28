using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobScheduleCreateRequestV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdescriptionSet")]
        protected bool _isDescriptionSet { get; set; }

        protected string? _description { get; set; }

        /// <summary>
        /// The unique identifier of the shuttle job this schedule belongs to.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; }

        /// <summary>
        /// The kind of run the schedule starts. `scan` is not a valid schedule
        /// run mode.
        /// </summary>
        [JsonPropertyName("run_mode")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleCreateRequestV2026R0RunModeField>))]
        public StringEnum<ShuttleJobScheduleCreateRequestV2026R0RunModeField> RunMode { get; }

        /// <summary>
        /// Whether the schedule is active. When omitted, defaults to `enabled`.
        /// </summary>
        [JsonPropertyName("state")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleCreateRequestV2026R0StateField>))]
        public StringEnum<ShuttleJobScheduleCreateRequestV2026R0StateField>? State { get; init; }

        /// <summary>
        /// An optional human-readable description of the schedule.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get => _description; init { _description = value; _isDescriptionSet = true; } }

        [JsonPropertyName("trigger")]
        public ShuttleJobScheduleTriggerV2026R0 Trigger { get; }

        public ShuttleJobScheduleCreateRequestV2026R0(string jobId, ShuttleJobScheduleCreateRequestV2026R0RunModeField runMode, ShuttleJobScheduleTriggerV2026R0 trigger) {
            JobId = jobId;
            RunMode = runMode;
            Trigger = trigger;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleJobScheduleCreateRequestV2026R0(string jobId, StringEnum<ShuttleJobScheduleCreateRequestV2026R0RunModeField> runMode, ShuttleJobScheduleTriggerV2026R0 trigger) {
            JobId = jobId;
            RunMode = runMode;
            Trigger = trigger;
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