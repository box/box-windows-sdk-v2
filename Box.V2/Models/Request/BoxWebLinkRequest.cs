using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxWebLinkRequest : BoxItemRequest
    {
        /// <summary>
        /// URL you want the web link to point to. Must include http:// or https://
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public Uri Url { get; set; }
    }
}
