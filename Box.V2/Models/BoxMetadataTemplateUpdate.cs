using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a metadata template update
    /// </summary>
    public class BoxMetadataTemplateUpdate
    {
        public const string FieldOp = "op";
        public const string FieldData = "data";
        public const string FieldFieldKey = "fieldKey";
        public const string FieldFieldKeys = "fieldKeys";
        public const string FieldEnumOptionKeys = "enumOptionKeys";

        [JsonProperty(PropertyName = FieldOp)]
        [JsonConverter(typeof(StringEnumConverter))]
        public MetadataTemplateUpdateOp? Op { get; set; }

        [JsonProperty(PropertyName = FieldData)]
        public Object Data { get; set; }

        [JsonProperty(PropertyName = FieldFieldKey)]
        public string FieldKey { get; set; }

        [JsonProperty(PropertyName = FieldFieldKeys)]
        public List<string> FieldKeys { get; set; }

        [JsonProperty(PropertyName = FieldEnumOptionKeys)]
        public List<string> EnumOptionKeys { get; set; }
    }
}
