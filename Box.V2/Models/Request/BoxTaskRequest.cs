using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    public class BoxTaskRequest
    {
        /// <summary>
        /// Id of the task this assignment is for.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Must be task.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get
            {
                return "task";
            }
        }
    }
}
