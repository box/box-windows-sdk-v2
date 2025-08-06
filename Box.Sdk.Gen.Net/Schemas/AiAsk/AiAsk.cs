using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiAsk : ISerializable {
        /// <summary>
        /// Box AI handles text documents with text representations up to 1MB in size, or a maximum of 25 files, 
        /// whichever comes first. If the text file size exceeds 1MB, the first 1MB of text representation will be processed. 
        /// Box AI handles image documents with a resolution of 1024 x 1024 pixels, with a maximum of 5 images or 5 pages 
        /// for multi-page images. If the number of image or image pages exceeds 5, the first 5 images or pages will 
        /// be processed. If you set mode parameter to `single_item_qa`, the items array can have one element only. 
        /// Currently Box AI does not support multi-modal requests. If both images and text are sent Box AI will only 
        /// process the text.
        /// </summary>
        [JsonPropertyName("mode")]
        [JsonConverter(typeof(StringEnumConverter<AiAskModeField>))]
        public StringEnum<AiAskModeField> Mode { get; }

        /// <summary>
        /// The prompt provided by the client to be answered by the LLM.
        /// The prompt's length is limited to 10000 characters.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; }

        /// <summary>
        /// The items to be processed by the LLM, often files.
        /// </summary>
        [JsonPropertyName("items")]
        public IReadOnlyList<AiItemAsk> Items { get; }

        /// <summary>
        /// The history of prompts and answers previously passed to the LLM. This provides additional context to the LLM in generating the response.
        /// </summary>
        [JsonPropertyName("dialogue_history")]
        public IReadOnlyList<AiDialogueHistory>? DialogueHistory { get; init; }

        /// <summary>
        /// A flag to indicate whether citations should be returned.
        /// </summary>
        [JsonPropertyName("include_citations")]
        public bool? IncludeCitations { get; init; }

        [JsonPropertyName("ai_agent")]
        public AiAgentAskOrAiAgentReference? AiAgent { get; init; }

        public AiAsk(AiAskModeField mode, string prompt, IReadOnlyList<AiItemAsk> items) {
            Mode = mode;
            Prompt = prompt;
            Items = items;
        }
        
        [JsonConstructorAttribute]
        internal AiAsk(StringEnum<AiAskModeField> mode, string prompt, IReadOnlyList<AiItemAsk> items) {
            Mode = mode;
            Prompt = prompt;
            Items = items;
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