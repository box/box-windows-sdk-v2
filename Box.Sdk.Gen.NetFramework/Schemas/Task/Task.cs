using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class Task : ISerializable {
        /// <summary>
        /// The unique identifier for this task.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `task`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TaskTypeField>))]
        public StringEnum<TaskTypeField> Type { get; set; }

        [JsonPropertyName("item")]
        public FileMini Item { get; set; }

        /// <summary>
        /// When the task is due.
        /// </summary>
        [JsonPropertyName("due_at")]
        public System.DateTimeOffset? DueAt { get; set; }

        /// <summary>
        /// The type of task the task assignee will be prompted to
        /// perform.
        /// </summary>
        [JsonPropertyName("action")]
        [JsonConverter(typeof(StringEnumConverter<TaskActionField>))]
        public StringEnum<TaskActionField> Action { get; set; }

        /// <summary>
        /// A message that will be included with the task.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("task_assignment_collection")]
        public TaskAssignments TaskAssignmentCollection { get; set; }

        /// <summary>
        /// Whether the task has been completed.
        /// </summary>
        [JsonPropertyName("is_completed")]
        public bool? IsCompleted { get; set; }

        [JsonPropertyName("created_by")]
        public UserMini CreatedBy { get; set; }

        /// <summary>
        /// When the task object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Defines which assignees need to complete this task before the task
        /// is considered completed.
        /// 
        /// * `all_assignees` requires all assignees to review or
        /// approve the task in order for it to be considered completed.
        /// * `any_assignee` accepts any one assignee to review or
        /// approve the task in order for it to be considered completed.
        /// </summary>
        [JsonPropertyName("completion_rule")]
        [JsonConverter(typeof(StringEnumConverter<TaskCompletionRuleField>))]
        public StringEnum<TaskCompletionRuleField> CompletionRule { get; set; }

        public Task() {
            
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