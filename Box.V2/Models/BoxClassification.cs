using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxClassification
    {
        public const string FieldName = "name";
        public const string FieldDefinition = "definition";
        public const string FieldColor = "color";

        /// <summary>
        /// The name of the classification
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual string Name { get; private set; }

        /// <summary>
        /// The meaning of the classification
        /// </summary>
        [JsonProperty(PropertyName = FieldDefinition)]
        public virtual string Definition { get; set; }

        /// <summary>
        /// The color that is used to display the classification label in a user-interface
        /// </summary>
        [JsonProperty(PropertyName = FieldColor)]
        public virtual string Color { get; private set; }
    }
}
