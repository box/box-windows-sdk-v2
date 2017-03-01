using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxSessionPartsInfoRequest
    {
        public BoxSessionPartsInfoRequest(List<BoxSessionPartInfo> parts)
        {
            Parts = parts;
        }

        /// <summary>
        /// List of session parts uploaded
        /// </summary>
        [JsonProperty(PropertyName = "parts")]
        public List<BoxSessionPartInfo> Parts { get; set; }
    }

    public class BoxSessionPartInfo
    {
        /// <summary>
        /// String representing the 8 digit part ID
        /// </summary>
        [JsonProperty(PropertyName = "part_id")]
        public string PartId { get; set; }

        /// <summary>
        /// File part offset
        /// </summary>
        [JsonProperty(PropertyName = "offset")]
        public long Offset { get; set; }

        /// <summary>
        /// File part Size
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long Size { get; set; }
    }
}
