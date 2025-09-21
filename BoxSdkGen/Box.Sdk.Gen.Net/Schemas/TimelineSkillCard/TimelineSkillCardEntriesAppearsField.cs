using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class TimelineSkillCardEntriesAppearsField : ISerializable {
        /// <summary>
        /// The time in seconds when an
        /// entry should start appearing on a timeline.
        /// </summary>
        [JsonPropertyName("start")]
        public long? Start { get; init; }

        /// <summary>
        /// The time in seconds when an
        /// entry should stop appearing on a timeline.
        /// </summary>
        [JsonPropertyName("end")]
        public long? End { get; init; }

        public TimelineSkillCardEntriesAppearsField() {
            
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