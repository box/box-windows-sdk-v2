using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making folder requests
    /// </summary>
    public class BoxFolderRequest : BoxItemRequest
    {
        /// <summary>
        /// The email-to-upload address for this folder
        /// </summary>
        [JsonProperty(PropertyName = "folder_upload_email")]
        public BoxEmailRequest FolderUploadEmail { get; set; }

        /// <summary>
        /// The user who owns the folder. Only used when moving a collaborated folder that you are not the owner of to a folder you are the owner of. 
        /// Not a substitute for changing folder owners, please reference collaborations <see cref="http://developers.box.com/docs/#collaborations"/>
        /// to accomplish folder ownership changes.
        /// </summary>
        [JsonProperty(PropertyName = "owned_by")]
        public BoxRequestEntity OwnedBy { get; private set; }

        /// <summary>
        /// Whether Box Sync clients will sync this folder. Values of synced or not_synced can be sent, while partially_synced may also be returned.
        /// </summary>
        [JsonProperty(PropertyName = "sync_state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxSyncStateType? SyncState { get; set; }

    }

}
