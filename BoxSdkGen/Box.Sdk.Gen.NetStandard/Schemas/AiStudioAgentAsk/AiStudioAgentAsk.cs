using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiStudioAgentAsk : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_iscustom_instructionsSet")]
        protected bool _isCustomInstructionsSet { get; set; }

        protected string _customInstructions { get; set; }

        /// <summary>
        /// The type of AI agent used to handle queries.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiStudioAgentAskTypeField>))]
        public StringEnum<AiStudioAgentAskTypeField> Type { get; set; }

        /// <summary>
        /// The state of the AI Agent capability. Possible values are: `enabled` and `disabled`.
        /// </summary>
        [JsonPropertyName("access_state")]
        public string AccessState { get; set; }

        /// <summary>
        /// The description of the AI agent.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Custom instructions for the AI agent.
        /// </summary>
        [JsonPropertyName("custom_instructions")]
        public string CustomInstructions { get => _customInstructions; set { _customInstructions = value; _isCustomInstructionsSet = true; } }

        /// <summary>
        /// Suggested questions for the AI agent. If null, suggested question will be generated. If empty, no suggested questions will be displayed.
        /// </summary>
        [JsonPropertyName("suggested_questions")]
        public IReadOnlyList<string> SuggestedQuestions { get; set; }

        [JsonPropertyName("long_text")]
        public AiStudioAgentLongTextTool LongText { get; set; }

        [JsonPropertyName("basic_text")]
        public AiStudioAgentBasicTextTool BasicText { get; set; }

        [JsonPropertyName("basic_image")]
        public AiStudioAgentBasicTextTool BasicImage { get; set; }

        [JsonPropertyName("spreadsheet")]
        public AiStudioAgentSpreadsheetTool Spreadsheet { get; set; }

        [JsonPropertyName("long_text_multi")]
        public AiStudioAgentLongTextTool LongTextMulti { get; set; }

        [JsonPropertyName("basic_text_multi")]
        public AiStudioAgentBasicTextTool BasicTextMulti { get; set; }

        [JsonPropertyName("basic_image_multi")]
        public AiStudioAgentBasicTextTool BasicImageMulti { get; set; }

        public AiStudioAgentAsk(string accessState, string description, AiStudioAgentAskTypeField type = AiStudioAgentAskTypeField.AiAgentAsk) {
            Type = type;
            AccessState = accessState;
            Description = description;
        }
        
        [JsonConstructorAttribute]
        internal AiStudioAgentAsk(string accessState, string description, StringEnum<AiStudioAgentAskTypeField> type) {
            Type = AiStudioAgentAskTypeField.AiAgentAsk;
            AccessState = accessState;
            Description = description;
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