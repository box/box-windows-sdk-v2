using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class SharedLink
    {
        /// <summary>
        /// The Url of the shared link
        /// </summary>
        [JsonProperty(PropertyName="url")]
        public string Url { get; set; }
    }
}
