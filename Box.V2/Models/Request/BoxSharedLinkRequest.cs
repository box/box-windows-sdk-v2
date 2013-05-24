using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// An object representing this item’s shared link and associated permissions
    /// </summary>
    public class BoxSharedLinkRequest : BoxRequestEntity
    {
        /// <summary>
        /// The level of access required for this shared link. Can be open, company, collaborators
        /// </summary>
        [JsonProperty(PropertyName = "access")]
        public string Access { get; set; }

        /// <summary>
        /// The day that this link should be disabled at. Timestamps are rounded off to the given day.
        /// </summary>
        [JsonProperty(PropertyName = "unshared_at")]
        public DateTime? UnsharedAt { get; set; }

        /// <summary>
        /// The set of permissions that apply to this link
        /// </summary>
        [JsonProperty(PropertyName = "permissions")]
        public BoxPermissionsRequest Permissions { get; set; }
    }
}
