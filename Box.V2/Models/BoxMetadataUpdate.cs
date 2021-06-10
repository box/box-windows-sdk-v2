using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a metadata update
    /// </summary>
    public class BoxMetadataUpdate
    {
        public const string FieldOp = "op";
        public const string FieldPath = "path";
        public const string FieldValue = "value";
        public const string FieldFrom = "from";

        /// <summary>
        /// The operation type. Must be add, replace, remove , test, move, or copy.
        /// </summary>
        [JsonProperty(PropertyName = FieldOp)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual MetadataUpdateOp? Op { get; set; }

        /// <summary>
        /// The path that designates the key, in the format of a JSON-Pointer. Since all keys are located at the root of the metadata instance, the key must be prefixed with a /. Special characters ~ and / in the key must be escaped according to JSON-Pointer specification. The value at the path must exist for the operation to be successful.
        /// </summary>
        [JsonProperty(PropertyName = FieldPath)]
        public virtual string Path { get; set; }

        /// <summary>
        /// The value to be set or tested. Required for add, replace, and test operations. For add, if value already exists, then previous value will be overwritten by the new value. For replace, the metadata value must exist before replacing.For test, the value of the existing metadata instance must match the specified value.
        /// </summary>
        [JsonProperty(PropertyName = FieldValue)]
        public virtual object Value { get; set; }

        /// <summary>
        /// Required for move or copy. The path that designates the source key, in the format of a JSON-Pointer, formatted in the same way as path. Used in conjunction with path: from specifies the source, path specifies the destination.
        /// </summary>
        [JsonProperty(PropertyName = FieldFrom)]
        public virtual string From { get; set; }
    }
}
