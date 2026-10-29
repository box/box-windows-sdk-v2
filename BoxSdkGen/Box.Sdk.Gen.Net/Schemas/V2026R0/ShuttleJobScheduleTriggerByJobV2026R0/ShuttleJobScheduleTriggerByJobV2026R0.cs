using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobScheduleTriggerByJobV2026R0 : ISerializable {
        /// <summary>
        /// The value will always be `by_job`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleTriggerByJobV2026R0TypeField>))]
        public StringEnum<ShuttleJobScheduleTriggerByJobV2026R0TypeField> Type { get; }

        /// <summary>
        /// The unique identifier of the shuttle job whose run starts this
        /// schedule.
        /// </summary>
        [JsonPropertyName("triggered_by_job_id")]
        public string TriggeredByJobId { get; }

        /// <summary>
        /// When this schedule should start a run relative to the last run of
        /// the triggering job.
        /// </summary>
        [JsonPropertyName("last_run_condition")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleTriggerByJobV2026R0LastRunConditionField>))]
        public StringEnum<ShuttleJobScheduleTriggerByJobV2026R0LastRunConditionField> LastRunCondition { get; }

        public ShuttleJobScheduleTriggerByJobV2026R0(string triggeredByJobId, ShuttleJobScheduleTriggerByJobV2026R0LastRunConditionField lastRunCondition, ShuttleJobScheduleTriggerByJobV2026R0TypeField type = ShuttleJobScheduleTriggerByJobV2026R0TypeField.ByJob) {
            Type = type;
            TriggeredByJobId = triggeredByJobId;
            LastRunCondition = lastRunCondition;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleJobScheduleTriggerByJobV2026R0(string triggeredByJobId, StringEnum<ShuttleJobScheduleTriggerByJobV2026R0LastRunConditionField> lastRunCondition, StringEnum<ShuttleJobScheduleTriggerByJobV2026R0TypeField> type) {
            Type = ShuttleJobScheduleTriggerByJobV2026R0TypeField.ByJob;
            TriggeredByJobId = triggeredByJobId;
            LastRunCondition = lastRunCondition;
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