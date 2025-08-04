using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SkillInvocation : ISerializable {
        /// <summary>
        /// The value will always be `skill_invocation`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<SkillInvocationTypeField>))]
        public StringEnum<SkillInvocationTypeField> Type { get; set; }

        /// <summary>
        /// Unique identifier for the invocation request.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("skill")]
        public SkillInvocationSkillField Skill { get; set; }

        /// <summary>
        /// The read-only and read-write access tokens for this item.
        /// </summary>
        [JsonPropertyName("token")]
        public SkillInvocationTokenField Token { get; set; }

        /// <summary>
        /// The details status of this event.
        /// </summary>
        [JsonPropertyName("status")]
        public SkillInvocationStatusField Status { get; set; }

        /// <summary>
        /// The time this invocation was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Action that triggered the invocation.
        /// </summary>
        [JsonPropertyName("trigger")]
        public string Trigger { get; set; }

        [JsonPropertyName("enterprise")]
        public SkillInvocationEnterpriseField Enterprise { get; set; }

        [JsonPropertyName("source")]
        public FileOrFolder Source { get; set; }

        [JsonPropertyName("event")]
        public Event Event { get; set; }

        public SkillInvocation() {
            
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