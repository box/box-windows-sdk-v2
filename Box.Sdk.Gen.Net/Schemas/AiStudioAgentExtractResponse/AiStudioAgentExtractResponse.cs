using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiStudioAgentExtractResponse : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_iscustom_instructionsSet")]
        protected bool _isCustomInstructionsSet { get; set; }

        protected string? _customInstructions { get; set; }

        /// <summary>
        /// The type of AI agent to be used for metadata extraction.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiStudioAgentExtractResponseTypeField>))]
        public StringEnum<AiStudioAgentExtractResponseTypeField> Type { get; }

        /// <summary>
        /// The state of the AI Agent capability. Possible values are: `enabled` and `disabled`.
        /// </summary>
        [JsonPropertyName("access_state")]
        public string AccessState { get; }

        /// <summary>
        /// The description of the AI agent.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; }

        /// <summary>
        /// Custom instructions for the AI agent.
        /// </summary>
        [JsonPropertyName("custom_instructions")]
        public string? CustomInstructions { get => _customInstructions; init { _customInstructions = value; _isCustomInstructionsSet = true; } }

        [JsonPropertyName("long_text")]
        public AiStudioAgentLongTextToolResponse? LongText { get; init; }

        [JsonPropertyName("basic_text")]
        public AiStudioAgentBasicTextToolResponse? BasicText { get; init; }

        [JsonPropertyName("basic_image")]
        public AiStudioAgentBasicTextToolResponse? BasicImage { get; init; }

        public AiStudioAgentExtractResponse(string accessState, string description, AiStudioAgentExtractResponseTypeField type = AiStudioAgentExtractResponseTypeField.AiAgentExtract) {
            Type = type;
            AccessState = accessState;
            Description = description;
        }
        
        [JsonConstructorAttribute]
        internal AiStudioAgentExtractResponse(string accessState, string description, StringEnum<AiStudioAgentExtractResponseTypeField> type) {
            Type = AiStudioAgentExtractResponseTypeField.AiAgentExtract;
            AccessState = accessState;
            Description = description;
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