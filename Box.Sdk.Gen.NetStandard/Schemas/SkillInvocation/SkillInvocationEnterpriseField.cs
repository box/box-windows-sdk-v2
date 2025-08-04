using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SkillInvocationEnterpriseField : ISerializable {
        /// <summary>
        /// The unique identifier for this enterprise.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `enterprise`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<SkillInvocationEnterpriseTypeField>))]
        public StringEnum<SkillInvocationEnterpriseTypeField> Type { get; set; }

        /// <summary>
        /// The name of the enterprise.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public SkillInvocationEnterpriseField() {
            
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