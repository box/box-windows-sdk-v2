using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiStudioAgentExtract : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_iscustom_instructionsSet")]
        protected bool _isCustomInstructionsSet { get; set; }

        protected string? _customInstructions { get; set; }

        /// <summary>
        /// The type of AI agent to be used for metadata extraction.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiStudioAgentExtractTypeField>))]
        public StringEnum<AiStudioAgentExtractTypeField> Type { get; }

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
        public AiStudioAgentLongTextTool? LongText { get; init; }

        [JsonPropertyName("basic_text")]
        public AiStudioAgentBasicTextTool? BasicText { get; init; }

        [JsonPropertyName("basic_image")]
        public AiStudioAgentBasicTextTool? BasicImage { get; init; }

        public AiStudioAgentExtract(string accessState, string description, AiStudioAgentExtractTypeField type = AiStudioAgentExtractTypeField.AiAgentExtract) {
            Type = type;
            AccessState = accessState;
            Description = description;
        }
        
        [JsonConstructorAttribute]
        internal AiStudioAgentExtract(string accessState, string description, StringEnum<AiStudioAgentExtractTypeField> type) {
            Type = AiStudioAgentExtractTypeField.AiAgentExtract;
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