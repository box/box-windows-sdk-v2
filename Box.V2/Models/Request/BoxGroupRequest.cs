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
        /// A description of the group.
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Used to track the external source where the group is coming from.
        /// </summary>
        [JsonProperty(PropertyName = "provenance")]
        public string Provenance { get; set; }

        /// <summary>
        /// Used as a group identifier for groups coming from an external source.
        /// </summary>
        [JsonProperty(PropertyName = "external_sync_identifier")]
        public string ExternalSyncIdentifier { get; set; }

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
