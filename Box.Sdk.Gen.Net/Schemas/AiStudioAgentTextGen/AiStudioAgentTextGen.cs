using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiStudioAgentTextGen : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_iscustom_instructionsSet")]
        protected bool _isCustomInstructionsSet { get; set; }

        protected string? _customInstructions { get; set; }

        /// <summary>
        /// The type of AI agent used for generating text.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiStudioAgentTextGenTypeField>))]
        public StringEnum<AiStudioAgentTextGenTypeField> Type { get; }

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

        /// <summary>
        /// Suggested questions for the AI agent. If null, suggested question will be generated. If empty, no suggested questions will be displayed.
        /// </summary>
        [JsonPropertyName("suggested_questions")]
        public IReadOnlyList<string>? SuggestedQuestions { get; init; }

        [JsonPropertyName("basic_gen")]
        public AiStudioAgentBasicGenTool? BasicGen { get; init; }

        public AiStudioAgentTextGen(string accessState, string description, AiStudioAgentTextGenTypeField type = AiStudioAgentTextGenTypeField.AiAgentTextGen) {
            Type = type;
            AccessState = accessState;
            Description = description;
        }
        
        [JsonConstructorAttribute]
        internal AiStudioAgentTextGen(string accessState, string description, StringEnum<AiStudioAgentTextGenTypeField> type) {
            Type = AiStudioAgentTextGenTypeField.AiAgentTextGen;
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