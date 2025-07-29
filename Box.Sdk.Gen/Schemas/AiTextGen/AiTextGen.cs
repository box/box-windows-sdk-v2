using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AiTextGen : ISerializable {
        /// <summary>
        /// The prompt provided by the client to be answered by the LLM. The prompt's length is limited to 10000 characters.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; }

        /// <summary>
        /// The items to be processed by the LLM, often files.
        /// The array can include **exactly one** element.
        /// 
        /// **Note**: Box AI handles documents with text representations up to 1MB in size.
        /// If the file size exceeds 1MB, the first 1MB of text representation will be processed.
        /// </summary>
        [JsonPropertyName("items")]
        public IReadOnlyList<AiTextGenItemsField> Items { get; }

        /// <summary>
        /// The history of prompts and answers previously passed to the LLM. This parameter provides the additional context to the LLM when generating the response.
        /// </summary>
        [JsonPropertyName("dialogue_history")]
        public IReadOnlyList<AiDialogueHistory>? DialogueHistory { get; init; }

        [JsonPropertyName("ai_agent")]
        public AiAgentReferenceOrAiAgentTextGen? AiAgent { get; init; }

        public AiTextGen(string prompt, IReadOnlyList<AiTextGenItemsField> items) {
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