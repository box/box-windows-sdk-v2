using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

        private bool _isUnsharedAtSet = false;
        private DateTimeOffset? _unsharedAt;

        /// <summary>
        /// The day that this link should be disabled at. Timestamps are rounded off to the given day.
        /// </summary>
        [JsonProperty(PropertyName = "unshared_at", NullValueHandling = NullValueHandling.Include)]
        public DateTimeOffset? UnsharedAt
        {
            get
            {
                return _unsharedAt;
            }

            set
            {
                _unsharedAt = value;
                _isUnsharedAtSet = true;
            }
        }

        public bool ShouldSerializeUnsharedAt()
        {
            return _isUnsharedAtSet;
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

