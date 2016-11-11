using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxTask : BoxEntity
    {
        public const string FieldDueAt = "due_at";
        public const string FieldItem = "item";
        public const string FieldAction = "action";
        public const string FieldMessage = "message";
        public const string FieldIsCompleted = "is_completed";
        public const string FieldCreatedBy = "created_by";
        public const string FieldTaskAssignmentCollection = "task_assignment_collection";
        
        /// <summary>
        /// Date of task completion
        /// </summary>
        [JsonProperty(PropertyName = FieldDueAt)]
        public string DueAt { get; private set; }

        /// <summary>
        /// Mini file object. The file associated with this task
        /// </summary>
        [JsonProperty(PropertyName = FieldItem)]
        public BoxItem Item { get; private set; }

        /// <summary>
        /// Gets the action.
        /// </summary>
        [JsonProperty(PropertyName = FieldAction)]
        public string Action { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        [JsonProperty(PropertyName = FieldMessage)]
        public string Message { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this task is completed.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsCompleted)]
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// The user who created this item
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public BoxUser CreatedBy { get; private set; }

        /// <summary>
        /// Gets the task assignments.
        /// </summary>
        [JsonProperty(PropertyName = FieldTaskAssignmentCollection)]
        public BoxCollection<BoxTaskAssignment> TaskAssignments { get; private set; }
    }
}
