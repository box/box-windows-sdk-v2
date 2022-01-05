using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        public const string FieldEnumOptionKey = "enumOptionKey";
        public const string FieldEnumOptionKeys = "enumOptionKeys";

        [JsonProperty(PropertyName = FieldOp)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual MetadataTemplateUpdateOp? Op { get; set; }

        [JsonProperty(PropertyName = FieldData)]
        public virtual object Data { get; set; }

        [JsonProperty(PropertyName = FieldFieldKey)]
        public virtual string FieldKey { get; set; }

        [JsonProperty(PropertyName = FieldFieldKeys)]
        public virtual List<string> FieldKeys { get; set; }

        [JsonProperty(PropertyName = FieldEnumOptionKey)]
        public virtual string EnumOptionKey { get; set; }

        [JsonProperty(PropertyName = FieldEnumOptionKeys)]
        public virtual List<string> EnumOptionKeys { get; set; }
    }
}
