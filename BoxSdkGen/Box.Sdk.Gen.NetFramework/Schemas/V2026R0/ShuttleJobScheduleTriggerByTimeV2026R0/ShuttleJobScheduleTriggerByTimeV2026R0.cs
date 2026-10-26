using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobScheduleTriggerByTimeV2026R0 : ISerializable {
        /// <summary>
        /// The value will always be `by_time`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleTriggerByTimeV2026R0TypeField>))]
        public StringEnum<ShuttleJobScheduleTriggerByTimeV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The local wall-clock time when the schedule should first start a
        /// run, without an offset or `Z` suffix. Combined with `time_zone` to
        /// determine the instant, including DST.
        /// </summary>
        [JsonPropertyName("start_at")]
        public string StartAt { get; set; }

        /// <summary>
        /// The IANA time zone that gives `start_at` its calendar meaning (for
        /// example `America/Los_Angeles`). Recurrence and DST transitions are
        /// evaluated in this zone.
        /// </summary>
        [JsonPropertyName("time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// How often the schedule repeats after `start_at`.
        /// </summary>
        [JsonPropertyName("repeat")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobScheduleTriggerByTimeV2026R0RepeatField>))]
        public StringEnum<ShuttleJobScheduleTriggerByTimeV2026R0RepeatField> Repeat { get; set; }

        public ShuttleJobScheduleTriggerByTimeV2026R0(string startAt, string timeZone, ShuttleJobScheduleTriggerByTimeV2026R0RepeatField repeat, ShuttleJobScheduleTriggerByTimeV2026R0TypeField type = ShuttleJobScheduleTriggerByTimeV2026R0TypeField.ByTime) {
            Type = type;
            StartAt = startAt;
            TimeZone = timeZone;
            Repeat = repeat;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleJobScheduleTriggerByTimeV2026R0(string startAt, string timeZone, StringEnum<ShuttleJobScheduleTriggerByTimeV2026R0RepeatField> repeat, StringEnum<ShuttleJobScheduleTriggerByTimeV2026R0TypeField> type) {
            Type = ShuttleJobScheduleTriggerByTimeV2026R0TypeField.ByTime;
            StartAt = startAt;
            TimeZone = timeZone;
            Repeat = repeat;
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