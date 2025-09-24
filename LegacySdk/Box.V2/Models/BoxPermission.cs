using Newtonsoft.Json;

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
        public virtual bool CanDownload { get; set; }

        /// <summary>
        /// Whether the item can be previewed or not
        /// </summary>
        [JsonProperty("can_preview")]
        public virtual bool CanPreview { get; set; }

        /// <summary>
        /// Whether the item can be edited or not
        /// </summary>
        [JsonProperty("can_edit")]
        public virtual bool? CanEdit { get; set; }
    }
}
