using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// The set of permissions that apply to this link
    /// </summary>
    public class BoxPermissionsRequest
    {
        /// <summary>
        /// Whether this link allows downloads. Can only be used with Open and Company
        /// </summary>
        [JsonProperty(PropertyName = "can_download")]
        public bool Download { get; set; }
        
        /// <summary>
        /// Whether this link allows previews. Can only be used with Open and Company
        /// </summary>
        [JsonProperty(PropertyName = "can_preview")]
        public bool Preview { get; set; }
    }
}
