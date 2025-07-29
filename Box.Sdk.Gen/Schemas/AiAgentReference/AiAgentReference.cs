using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentReference : ISerializable {
        /// <summary>
        /// The type of AI agent used to handle queries.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiAgentReferenceTypeField>))]
        public StringEnum<AiAgentReferenceTypeField> Type { get; }

        /// <summary>
        /// The ID of an Agent.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        public AiAgentReference(AiAgentReferenceTypeField type = AiAgentReferenceTypeField.AiAgentId) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiAgentReference(StringEnum<AiAgentReferenceTypeField> type) {
            Type = AiAgentReferenceTypeField.AiAgentId;
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