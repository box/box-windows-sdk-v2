using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    public class BoxZipRequest
    {
        /// <summary>
        /// The name of the zip file to be created
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The list of files or folders to be part of the created zip
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<BoxZipRequestItem> Items { get; set; }
    }
}
