using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// An object representing the request to create a shared link
    /// </summary>
    public class BoxSharedLinkRequest
    {
        /// <summary>
        /// The level of access required for this shared link. Can be open, company, collaborators, or null which will be the default value
        /// </summary>
        [JsonProperty(PropertyName = "access", NullValueHandling = NullValueHandling.Include)]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxSharedLinkAccessType? Access { get; set; }

        private bool IsUnsharedAtSet = false;
        private DateTime? _unsharedAt;

        /// <summary>
        /// The day that this link should be disabled at. Timestamps are rounded off to the given day.
        /// </summary>
        [JsonProperty(PropertyName = "unshared_at", NullValueHandling = NullValueHandling.Include)]
        public DateTime? UnsharedAt
        {
            get
            {
                return _unsharedAt;
            }

            set
            {
                _unsharedAt = value;
                IsUnsharedAtSet = true;
            }
        }

        public bool ShouldSerializeUnsharedAt()
        {
            return IsUnsharedAtSet;
        }

        /// <summary>
        /// The set of permissions that apply to this link
        /// </summary>
        [JsonProperty(PropertyName = "permissions")]
        public BoxPermissionsRequest Permissions { get; set; }

        /// <summary>
        /// The password to require before viewing this link
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}

