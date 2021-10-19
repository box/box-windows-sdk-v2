using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Request for creating a single task for single user on a single file
    /// </summary>
    public class BoxTaskUpdateRequest
    {
        /// <summary>
        /// Id of the task.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The action the task assignee will be prompted to do. Can be review.
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }

        /// <summary>
        /// An optional message to include with the task.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// The day at which this task is due.
        /// </summary>
        [JsonProperty(PropertyName = "due_at")]
        public DateTimeOffset? DueAt { get; set; }
    }
}
