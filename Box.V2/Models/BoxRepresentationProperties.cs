using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxRepresentationProperties
    {
        public const string FieldDimensions = "dimensions";
        public const string FieldPaged = "paged";
        public const string FieldThumb = "thumb";

        /// <summary>
        /// The available dimension generated for representation
        /// </summary>
        [JsonProperty(PropertyName = FieldDimensions)]
        public string Dimensions { get; private set; }

        /// <summary>
        /// Boolean to indicate whether the representation has been paged
        /// </summary>
        [JsonProperty(PropertyName = FieldPaged)]
        public bool Paged { get; private set; }

        /// <summary>
        /// Boolean to indicate whether representation is thumbnail or not
        /// </summary>
        [JsonProperty(PropertyName = FieldThumb)]
        public bool Thumb { get; private set; }
    }
}
