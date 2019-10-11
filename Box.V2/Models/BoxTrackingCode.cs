using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a tracking code
    /// </summary>
    public class BoxTrackingCode
    {
        public const string FieldType = "type";
        public const string FieldName = "name";
        public const string FieldValue = "value";

        /// <summary>
        /// The type of the tracking code, should be tracking_code
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        public virtual string Type { get; private set; }

        /// <summary>
        /// The name of the tracking code
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual string Name { get; private set; }

        /// <summary>
        /// The value of the tracking code
        /// </summary>
        [JsonProperty(PropertyName = FieldValue)]
        public virtual string Value { get; private set; }
    }
}
