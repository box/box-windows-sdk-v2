using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SkillInvocationTokenField : ISerializable {
        /// <summary>
        /// The basics of an access token.
        /// </summary>
        [JsonPropertyName("read")]
        public SkillInvocationTokenReadField? Read { get; init; }

        /// <summary>
        /// The basics of an access token.
        /// </summary>
        [JsonPropertyName("write")]
        public SkillInvocationTokenWriteField? Write { get; init; }

        public SkillInvocationTokenField() {
            
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