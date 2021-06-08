using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents the Parts uploaded in a Session.
    /// </summary>
    public class BoxSessionParts
    {
        /// <summary>
        /// List of session parts uploaded.
        /// </summary>
        [JsonProperty(PropertyName = "parts")]
        public virtual IEnumerable<BoxSessionPartInfo> Parts { get; set; }
    }

    /// <summary>
    /// Response of upload part.
    /// </summary>
    public class BoxUploadPartResponse
    {
        /// <summary>
        /// List of session parts uploaded.
        /// </summary>
        [JsonProperty(PropertyName = "part")]
        public virtual BoxSessionPartInfo Part { get; set; }
    }

    /// <summary>
    /// Represents a single part of a session.
    /// </summary>
    public class BoxSessionPartInfo
    {
        /// <summary>
        /// String representing the Unique 8 digit part ID.
        /// </summary>
        [JsonProperty(PropertyName = "part_id")]
        public virtual string PartId { get; set; }

        /// <summary>
        /// Offset in bytes for the file part that was uploaded.
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public virtual long Offset { get; set; }

        /// <summary>
        /// Size in bytes for the file part that was uploaded.
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public virtual long Size { get; set; }

        /// <summary>
        /// String with hexadecimal representation of part’s SHA-1.
        /// </summary>
        [JsonProperty(PropertyName = "sha1")]
        public virtual string Sha1 { get; set; }
    }
}
