using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// AI response
    /// </summary>
    public class BoxAIResponse
    {
        public const string FieldAnswer = "answer";
        public const string FieldCompletionReason = "completion_reason";
        public const string FieldCreatedAt = "created_at";

        /// <summary>
        /// The answer provided by the LLM.
        /// </summary>
        [JsonProperty(PropertyName = FieldAnswer)]
        public string Answer { get; set; }

        /// <summary>
        /// The reason the response finishes.
        /// </summary>
        [JsonProperty(PropertyName = FieldCompletionReason)]
        public string CompletionReason { get; set; }

        /// <summary>
        /// The ISO date formatted timestamp of when the answer to the prompt was created.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
