using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobScheduleUpdateRequestV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdescriptionSet")]
        protected bool _isDescriptionSet { get; set; }

        protected string? _description { get; set; }

        /// <summary>
        /// Whether the schedule is active.
        /// </summary>
        [JsonPropertyName("state")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleUpdateRequestV2026R0StateField>))]
        public StringEnum<ShuttleJobScheduleUpdateRequestV2026R0StateField>? State { get; init; }

        /// <summary>
        /// The kind of run the schedule starts. `scan` is not a valid schedule
        /// run mode.
        /// </summary>
        [JsonPropertyName("run_mode")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleUpdateRequestV2026R0RunModeField>))]
        public StringEnum<ShuttleJobScheduleUpdateRequestV2026R0RunModeField>? RunMode { get; init; }

        /// <summary>
        /// An optional human-readable description of the schedule.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get => _description; init { _description = value; _isDescriptionSet = true; } }

        [JsonPropertyName("trigger")]
        public ShuttleJobScheduleTriggerV2026R0? Trigger { get; init; }

        public ShuttleJobScheduleUpdateRequestV2026R0() {
            
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