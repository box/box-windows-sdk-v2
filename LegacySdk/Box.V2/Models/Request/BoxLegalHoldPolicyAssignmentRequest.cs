using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxLegalHoldPolicyAssignmentRequest
    {
        /// <summary>
        /// Id of Policy to create Assignment for.
        /// </summary>
        [JsonProperty(PropertyName = "policy_id")]
        public string PolicyId { get; set; }

        /// <summary>
        /// Target entity. Can be 'file_version', 'file', 'folder', or 'user'.
        /// </summary>
        [JsonProperty(PropertyName = "assign_to")]
        public BoxRequestEntity AssignTo { get; set; }
    }
}
