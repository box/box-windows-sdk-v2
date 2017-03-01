using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Represents the Parts uploaded in a Session.
    /// </summary>
    public class BoxSessionParts
    {
        public BoxSessionParts(List<BoxSessionPartInfo> parts)
        {
            Parts = parts;
        }

        /// <summary>
        /// List of session parts uploaded.
        /// </summary>
        [JsonProperty(PropertyName = "parts")]
        public List<BoxSessionPartInfo> Parts { get; set; }
    }

    /// <summary>
    /// Represents a single part of a session.
    /// </summary>
    public class BoxSessionPartInfo
    {
        /// <summary>
        /// String representing the 8 digit part ID.
        /// </summary>
        [JsonProperty(PropertyName = "part_id")]
        public string PartId { get; set; }

        /// <summary>
        /// File part offset in bytes.
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public long Offset { get; set; }

        /// <summary>
        /// File part Size in bytes.
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long Size { get; set; }
    }
}
