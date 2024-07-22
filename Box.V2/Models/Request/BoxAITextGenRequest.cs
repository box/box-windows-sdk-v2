using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Model used to send request to Box AI Text Gen API
    /// </summary>
    public class BoxAITextGenRequest
    {
        public const string FieldDialogueHistory = "dialogue_history";
        public const string FieldItems = "items";
        public const string FieldPrompt = "prompt";

        /// <summary>
        /// The history of prompts and answers previously passed to the LLM.This provides additional context to the LLM in generating the response.
        /// </summary>
        [JsonProperty(PropertyName = FieldDialogueHistory)]
        public List<BoxAIDialogueHistory> DialogueHistory { get; set; }

        /// <summary>
        /// The items to be processed by the LLM, often files.The array can include exactly one element.
        /// Note: Box AI handles documents with text representations up to 1MB in size.
        /// If the file size exceeds 1MB, the first 1MB of text representation will be processed.
        /// </summary>
        [JsonProperty(PropertyName = FieldItems)]
        public List<BoxAITextGenItem> Items { get; set; }

        /// <summary>
        /// The prompt provided by the client to be answered by the LLM. The prompt's length is limited to 10000 characters.
        /// </summary>
        [JsonProperty(PropertyName = FieldPrompt)]
        public string Prompt { get; set; }
    }

    /// <summary>
    /// The history of prompts and answers previously passed to the LLM.This provides additional context to the LLM in generating the response.
    /// </summary>
    public class BoxAIDialogueHistory
    {
        public const string FieldAnswer = "answer";
        public const string FieldCreatedAt = "created_at";
        public const string FieldPrompt = "prompt";

        /// <summary>
        /// The answer previously provided by the LLM.
        /// </summary>
        [JsonProperty(PropertyName = FieldAnswer)]
        public string Answer { get; set; }

        /// <summary>
        /// The ISO date formatted timestamp of when the previous answer to the prompt was created.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The prompt previously provided by the client and answered by the LLM.
        /// </summary>
        [JsonProperty(PropertyName = FieldPrompt)]
        public string Prompt { get; set; }
    }

    /// <summary>
    /// The items to be processed by the LLM, often files.The array can include exactly one element.
    /// Note: Box AI handles documents with text representations up to 1MB in size.
    /// If the file size exceeds 1MB, the first 1MB of text representation will be processed.
    /// </summary>
    public class BoxAITextGenItem
    {
        public const string FieldId = "id";
        public const string FieldType = "type";
        public const string FieldContent = "content";

        /// <summary>
        /// The id of the item.
        /// </summary>
        [JsonProperty(PropertyName = FieldId)]
        public string Id { get; set; }

        /// <summary>
        /// The type of the item. Value is always file.
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        public string Type => "file";

        /// <summary>
        /// The content to use as context for generating new text or editing existing text.
        /// </summary>
        [JsonProperty(PropertyName = FieldContent)]
        public string Content { get; set; }
    }
}
