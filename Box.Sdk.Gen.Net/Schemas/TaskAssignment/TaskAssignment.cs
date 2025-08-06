using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TaskAssignment : ISerializable {
        /// <summary>
        /// The unique identifier for this task assignment.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The value will always be `task_assignment`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TaskAssignmentTypeField>))]
        public StringEnum<TaskAssignmentTypeField>? Type { get; init; }

        [JsonPropertyName("item")]
        public FileMini? Item { get; init; }

        [JsonPropertyName("assigned_to")]
        public UserMini? AssignedTo { get; init; }

        /// <summary>
        /// A message that will is included with the task
        /// assignment. This is visible to the assigned user in the web and mobile
        /// UI.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; init; }

        /// <summary>
        /// The date at which this task assignment was
        /// completed. This will be `null` if the task is not completed yet.
        /// </summary>
        [JsonPropertyName("completed_at")]
        public System.DateTimeOffset? CompletedAt { get; init; }

        /// <summary>
        /// The date at which this task was assigned to the user.
        /// </summary>
        [JsonPropertyName("assigned_at")]
        public System.DateTimeOffset? AssignedAt { get; init; }

        /// <summary>
        /// The date at which the assigned user was reminded of this task
        /// assignment.
        /// </summary>
        [JsonPropertyName("reminded_at")]
        public System.DateTimeOffset? RemindedAt { get; init; }

        /// <summary>
        /// The current state of the assignment. The available states depend on
        /// the `action` value of the task object.
        /// </summary>
        [JsonPropertyName("resolution_state")]
        [JsonConverter(typeof(StringEnumConverter<TaskAssignmentResolutionStateField>))]
        public StringEnum<TaskAssignmentResolutionStateField>? ResolutionState { get; init; }

        [JsonPropertyName("assigned_by")]
        public UserMini? AssignedBy { get; init; }

        public TaskAssignment() {
            
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