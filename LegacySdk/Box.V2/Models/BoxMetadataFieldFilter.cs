using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a filter on a metadata field.  Used for assigning a retention policy to a metadata template.
    /// </summary>
    public class BoxMetadataFieldFilter
    {
        public const string FieldField = "field";
        public const string FieldValue = "value";

        /// <summary>
        /// The field key
        /// </summary>
        [JsonProperty(PropertyName = FieldField)]
        public virtual string Field { get; private set; }

        /// <summary>
        /// The value to filter against
        /// </summary>
        [JsonProperty(PropertyName = FieldValue)]
        public virtual string Value { get; private set; }
    }
}
