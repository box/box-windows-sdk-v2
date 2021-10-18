using Newtonsoft.Json;

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
        /// Constructor for creating new BoxTrackingCodes with a given name and value, such that they can be created and passed in BoxUserRequests
        /// </summary>
        /// <param name="name">Name of a tracking code registered by the enterprise administrator.</param>
        /// <param name="value">Value of the tracking code.</param>
        public BoxTrackingCode(string name, string value)
        {
            //Per description below, this should always be tracking_code
            Type = "tracking_code";
            Name = name;
            Value = value;
        }

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
