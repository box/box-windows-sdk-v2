using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class TimelineSkillCardSkillField : ISerializable {
        /// <summary>
        /// The value will always be `service`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TimelineSkillCardSkillTypeField>))]
        public StringEnum<TimelineSkillCardSkillTypeField> Type { get; }

        /// <summary>
        /// A custom identifier that represent the service that
        /// applied this metadata.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public TimelineSkillCardSkillField(string id, TimelineSkillCardSkillTypeField type = TimelineSkillCardSkillTypeField.Service) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal TimelineSkillCardSkillField(string id, StringEnum<TimelineSkillCardSkillTypeField> type) {
            Type = TimelineSkillCardSkillTypeField.Service;
            Id = id;
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