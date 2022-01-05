using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxItemPermission
    {
        /// <summary>
        /// Permission to download item
        /// </summary>
        [JsonProperty(PropertyName = "can_download")]
        public bool CanDownload { get; private set; }

        /// <summary>
        /// Permission to upload item
        /// </summary>
        [JsonProperty(PropertyName = "can_upload")]
        public bool CanUpload { get; private set; }

        /// <summary>
        /// Permission to comment on item
        /// </summary>
        [JsonProperty(PropertyName = "can_comment")]
        public bool CanComment { get; private set; }

        /// <summary>
        /// Permission to rename the item
        /// </summary>
        [JsonProperty(PropertyName = "can_rename")]
        public bool CanRename { get; private set; }

        /// <summary>
        /// Permission to delete the item
        /// </summary>
        [JsonProperty(PropertyName = "can_delete")]
        public bool CanDelete { get; private set; }

        /// <summary>
        /// Permission to share item
        /// </summary>
        [JsonProperty(PropertyName = "can_share")]
        public bool CanShare { get; private set; }

        /// <summary>
        /// Permission to change the access on the share
        /// </summary>
        [JsonProperty(PropertyName = "can_set_share_access")]
        public bool CanSetShareAccess { get; private set; }
    }
}
