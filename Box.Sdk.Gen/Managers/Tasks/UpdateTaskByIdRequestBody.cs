using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateTaskByIdRequestBody : ISerializable {
        /// <summary>
        /// The action the task assignee will be prompted to do. Must be
        /// 
        /// * `review` defines an approval task that can be approved or
        /// rejected,
        /// * `complete` defines a general task which can be completed.
        /// </summary>
        [JsonPropertyName("action")]
        [JsonConverter(typeof(StringEnumConverter<UpdateTaskByIdRequestBodyActionField>))]
        public StringEnum<UpdateTaskByIdRequestBodyActionField>? Action { get; init; }

        /// <summary>
        /// The message included with the task.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; init; }

        /// <summary>
        /// When the task is due at.
        /// </summary>
        [JsonPropertyName("due_at")]
        public System.DateTimeOffset? DueAt { get; init; }

        /// <summary>
        /// Defines which assignees need to complete this task before the task
        /// is considered completed.
        /// 
        /// * `all_assignees` (default) requires all assignees to review or
        /// approve the the task in order for it to be considered completed.
        /// * `any_assignee` accepts any one assignee to review or
        /// approve the the task in order for it to be considered completed.
        /// </summary>
        [JsonPropertyName("completion_rule")]
        [JsonConverter(typeof(StringEnumConverter<UpdateTaskByIdRequestBodyCompletionRuleField>))]
        public StringEnum<UpdateTaskByIdRequestBodyCompletionRuleField>? CompletionRule { get; init; }

        public UpdateTaskByIdRequestBody() {
            
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