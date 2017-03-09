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

        /// <summary>
        /// Marker (if present) to sent in the next request of List parts.
        /// </summary>
        [JsonProperty(PropertyName = "marker")]
        public string Marker { get; set; }
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
        public string PartId { get; set; }

        /// <summary>
        /// Offset in bytes for the file part that was uploaded.
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public long Offset { get; set; }

        /// <summary>
        /// Size in bytes for the file part that was uploaded.
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long Size { get; set; }
    }
}
