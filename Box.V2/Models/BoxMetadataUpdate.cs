using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Box.V2.Models
{
    public class BoxMetadataUpdate
    {
        public const string FieldOp = "op";
        public const string FieldPath = "path";
        public const string FieldValue = "value";
        public const string FieldFrom = "from";

        [JsonProperty(PropertyName = FieldOp)]
        [JsonConverter(typeof(StringEnumConverter))]
        public MetadataUpdateOp? Op { get; set; }

        [JsonProperty(PropertyName = FieldPath)]
        public string Path { get; set; }

        [JsonProperty(PropertyName = FieldValue)]
        public string Value { get; set; }

        [JsonProperty(PropertyName = FieldFrom)]
        public string From { get; set; }
    }
}
