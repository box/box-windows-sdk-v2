using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        [Obsolete("CanPreview is now deprecated in the API and cannot be altered. Results will always be true")]
        [JsonProperty(PropertyName = "can_preview")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxPermissionType? Preview { get; set; }

        /// <summary>
        /// Defines if the shared link allows for the item to be edited.
        /// This value can only be true if can_download is also true and if the item has a type of file
        /// </summary>
        [JsonProperty(PropertyName = "can_edit")]
        public bool? Edit { get; set; }
    }

    /// <summary>
    /// The available permissions for the request
    /// </summary>
    public enum BoxPermissionType
    {
        Open,
        Company
    }
}
