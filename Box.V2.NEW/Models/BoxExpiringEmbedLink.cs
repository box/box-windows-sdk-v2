using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxExpiringEmbedLink
    {
        public const string FieldUrl = "url";

        [JsonProperty(PropertyName = FieldUrl)]
        public Uri Url { get; private set; }
    }
}
