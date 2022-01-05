using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// Request to update a task assignment.
    /// </summary>
    public class BoxTaskAssignmentUpdateRequest
    {
        /// <summary>
        /// Id of the task assignment.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// A message from the assignee about this task.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// The login email address for the user this assignment is for.
        /// </summary>
        [JsonProperty(PropertyName = "resolution_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ResolutionStateType? ResolutionState { get; set; }
    }
}
