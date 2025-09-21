using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class TimelineSkillCardSkillCardTitleField : ISerializable {
        /// <summary>
        /// An optional identifier for the title.
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; init; }

        /// <summary>
        /// The actual title to show in the UI.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; }

        public TimelineSkillCardSkillCardTitleField(string message) {
            Message = message;
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