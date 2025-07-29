using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiAgentAsk : ISerializable {
        /// <summary>
        /// The type of AI agent used to handle queries.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiAgentAskTypeField>))]
        public StringEnum<AiAgentAskTypeField> Type { get; }

        [JsonPropertyName("long_text")]
        public AiAgentLongTextTool? LongText { get; init; }

        [JsonPropertyName("basic_text")]
        public AiAgentBasicTextTool? BasicText { get; init; }

        [JsonPropertyName("spreadsheet")]
        public AiAgentSpreadsheetTool? Spreadsheet { get; init; }

        [JsonPropertyName("long_text_multi")]
        public AiAgentLongTextTool? LongTextMulti { get; init; }

        [JsonPropertyName("basic_text_multi")]
        public AiAgentBasicTextTool? BasicTextMulti { get; init; }

        [JsonPropertyName("basic_image")]
        public AiAgentBasicTextTool? BasicImage { get; init; }

        [JsonPropertyName("basic_image_multi")]
        public AiAgentBasicTextTool? BasicImageMulti { get; init; }

        public AiAgentAsk(AiAgentAskTypeField type = AiAgentAskTypeField.AiAgentAsk) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiAgentAsk(StringEnum<AiAgentAskTypeField> type) {
            Type = AiAgentAskTypeField.AiAgentAsk;
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