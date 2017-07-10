using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a permission
    /// </summary>
    public class BoxPermission
    {
        /// <summary>
        /// Whether the item can be downloaded or not
        /// </summary>
        [JsonProperty("can_download")]
        public bool CanDownload { get; set; }

        /// <summary>
        /// Whether the item can be previewed or not
        /// </summary>
        [JsonProperty("can_preview")]
        public bool CanPreview { get; set; }
    }
}
