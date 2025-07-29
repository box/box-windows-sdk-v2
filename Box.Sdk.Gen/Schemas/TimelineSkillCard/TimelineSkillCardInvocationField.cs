using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class TimelineSkillCardInvocationField : ISerializable {
        /// <summary>
        /// The value will always be `skill_invocation`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TimelineSkillCardInvocationTypeField>))]
        public StringEnum<TimelineSkillCardInvocationTypeField> Type { get; }

        /// <summary>
        /// A custom identifier that represent the instance of
        /// the service that applied this metadata. For example,
        /// if your `image-recognition-service` runs on multiple
        /// nodes, this field can be used to identify the ID of
        /// the node that was used to apply the metadata.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public TimelineSkillCardInvocationField(string id, TimelineSkillCardInvocationTypeField type = TimelineSkillCardInvocationTypeField.SkillInvocation) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal TimelineSkillCardInvocationField(string id, StringEnum<TimelineSkillCardInvocationTypeField> type) {
            Type = TimelineSkillCardInvocationTypeField.SkillInvocation;
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