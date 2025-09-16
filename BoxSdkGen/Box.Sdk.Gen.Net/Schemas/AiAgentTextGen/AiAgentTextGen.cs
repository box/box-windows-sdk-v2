using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentTextGen : ISerializable {
        /// <summary>
        /// The type of AI agent used for generating text.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiAgentTextGenTypeField>))]
        public StringEnum<AiAgentTextGenTypeField> Type { get; }

        [JsonPropertyName("basic_gen")]
        public AiAgentBasicGenTool? BasicGen { get; init; }

        public AiAgentTextGen(AiAgentTextGenTypeField type = AiAgentTextGenTypeField.AiAgentTextGen) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiAgentTextGen(StringEnum<AiAgentTextGenTypeField> type) {
            Type = AiAgentTextGenTypeField.AiAgentTextGen;
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