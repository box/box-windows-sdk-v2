using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    /// <summary>
    /// The request for adding a user to a group
    /// </summary>
    public class BoxGroupMembershipRequest
    {
        /// <summary>
        /// The User to add to the group. Only the Id should be set for the user
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public BoxRequestEntity User { get; set; }

        /// <summary>
        /// The group to add the user to. Only group Id should be set for the specified group.
        /// </summary>
        [JsonProperty(PropertyName = "group")]
        public BoxGroupRequest Group { get; set; }

        /// <summary>
        /// The role of the user in this group. Default is member.
        /// </summary>
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
    }
}
