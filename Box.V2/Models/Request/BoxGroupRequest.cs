using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    /// <summary>
    /// The request class for making group requests.
    /// </summary>
    public class BoxGroupRequest
    {
        /// <summary>
        /// The id of the group.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of the group.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Specifies who can invite this group to folders. Retrieved through the fields parameter.
        /// </summary>
        [JsonProperty(PropertyName = "invitability_level")]
        public string InvitabilityLevel { get; set; }

        /// <summary>
        /// Specifies who can view the members of this group. Retrieved through the fields parameter.
        /// </summary>
        [JsonProperty(PropertyName = "member_viewability_level")]
        public string MemberViewabilityLevel { get; set; }
    }
}
