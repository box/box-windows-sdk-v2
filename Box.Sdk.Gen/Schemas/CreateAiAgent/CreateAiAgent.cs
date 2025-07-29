using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class CreateAiAgent : ISerializable {
        /// <summary>
        /// The type of agent used to handle queries.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateAiAgentTypeField>))]
        public StringEnum<CreateAiAgentTypeField> Type { get; }

        /// <summary>
        /// The name of the AI Agent.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// The state of the AI Agent. Possible values are: `enabled`, `disabled`, and `enabled_for_selected_users`.
        /// </summary>
        [JsonPropertyName("access_state")]
        public string AccessState { get; }

        /// <summary>
        /// The icon reference of the AI Agent. It should have format of the URL `https://cdn01.boxcdn.net/app-assets/aistudio/avatars/<file_name>`
        /// where possible values of `file_name` are: `logo_boxAi.png`,`logo_stamp.png`,`logo_legal.png`,`logo_finance.png`,`logo_config.png`,`logo_handshake.png`,`logo_analytics.png`,`logo_classification.png`.
        /// </summary>
        [JsonPropertyName("icon_reference")]
        public string? IconReference { get; init; }

        /// <summary>
        /// List of allowed users or groups.
        /// </summary>
        [JsonPropertyName("allowed_entities")]
        public IReadOnlyList<AiAgentAllowedEntity>? AllowedEntities { get; init; }

        [JsonPropertyName("ask")]
        public AiStudioAgentAsk? Ask { get; init; }

        [JsonPropertyName("text_gen")]
        public AiStudioAgentTextGen? TextGen { get; init; }

        [JsonPropertyName("extract")]
        public AiStudioAgentExtract? Extract { get; init; }

        public CreateAiAgent(string name, string accessState, CreateAiAgentTypeField type = CreateAiAgentTypeField.AiAgent) {
            Type = type;
            Name = name;
            AccessState = accessState;
        }
        
        [JsonConstructorAttribute]
        internal CreateAiAgent(string name, string accessState, StringEnum<CreateAiAgentTypeField> type) {
            Type = CreateAiAgentTypeField.AiAgent;
            Name = name;
            AccessState = accessState;
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