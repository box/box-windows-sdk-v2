using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxSharedLink
    {
        /// <summary>
        /// The Url of the shared link
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; private set; }

        [JsonProperty(PropertyName = "download_url")]
        public string DownloadUrl { get; set; }

        [JsonProperty(PropertyName = "vanity_url")]
        public string VanityUrl { get; set; }

        [JsonProperty(PropertyName = "is_password_enabled")]
        public bool IsPasswordEnabled { get; set; }

        [JsonProperty(PropertyName = "unshared_at")]
        public DateTime? UnsharedAt { get; set; }

        [JsonProperty(PropertyName = "download_count")]
        public int DownloadCount { get; set; }

        [JsonProperty(PropertyName = "preview_count")]
        public int PreviewCount { get; set; }

        [JsonProperty(PropertyName = "access")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxSharedLinkAccessType? Access { get; set; }

        [JsonProperty(PropertyName = "permissions")]
        public BoxPermission Permissions { get; set; }
    }
}
