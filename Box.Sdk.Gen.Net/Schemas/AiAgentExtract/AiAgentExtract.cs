using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentExtract : ISerializable {
        /// <summary>
        /// The type of AI agent to be used for extraction.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiAgentExtractTypeField>))]
        public StringEnum<AiAgentExtractTypeField> Type { get; }

        [JsonPropertyName("long_text")]
        public AiAgentLongTextTool? LongText { get; init; }

        [JsonPropertyName("basic_text")]
        public AiAgentBasicTextTool? BasicText { get; init; }

        [JsonPropertyName("basic_image")]
        public AiAgentBasicTextTool? BasicImage { get; init; }

        public AiAgentExtract(AiAgentExtractTypeField type = AiAgentExtractTypeField.AiAgentExtract) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiAgentExtract(StringEnum<AiAgentExtractTypeField> type) {
            Type = AiAgentExtractTypeField.AiAgentExtract;
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