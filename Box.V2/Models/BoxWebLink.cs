using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxWebLink : BoxItem
    {
        public const string FieldUrl = "url";

        [JsonProperty(PropertyName = FieldUrl)]
        public virtual Uri Url { get; private set; }
    }
}
