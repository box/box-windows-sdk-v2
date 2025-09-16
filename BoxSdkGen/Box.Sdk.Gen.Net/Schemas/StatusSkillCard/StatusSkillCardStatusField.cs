using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class StatusSkillCardStatusField : ISerializable {
        /// <summary>
        /// A code for the status of this Skill invocation. By
        /// default each of these will have their own accompanied
        /// messages. These can be adjusted by setting the `message`
        /// value on this object.
        /// </summary>
        [JsonPropertyName("code")]
        [JsonConverter(typeof(StringEnumConverter<StatusSkillCardStatusCodeField>))]
        public StringEnum<StatusSkillCardStatusCodeField> Code { get; }

        /// <summary>
        /// A custom message that can be provided with this status.
        /// This will be shown in the web app to the end user.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; init; }

        public StatusSkillCardStatusField(StatusSkillCardStatusCodeField code) {
            Code = code;
        }
        
        [JsonConstructorAttribute]
        internal StatusSkillCardStatusField(StringEnum<StatusSkillCardStatusCodeField> code) {
            Code = code;
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