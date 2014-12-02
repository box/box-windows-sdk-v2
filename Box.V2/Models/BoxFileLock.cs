using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a file
    /// </summary>
    public class BoxFileLock : BoxItem
    {
        public const string FieldExpiresAt = "expires_at";
        public const string FieldIsDownloadPrevented = "is_download_prevented";

        /// <summary>
        /// The expiration date of this lock
        /// </summary>
        [JsonProperty(PropertyName = FieldExpiresAt)]
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Is download prevented for this lock?
        /// </summary>
        [JsonProperty(PropertyName = FieldIsDownloadPrevented)]
        public bool IsDownloadPrevented { get; set; }
    }
}
