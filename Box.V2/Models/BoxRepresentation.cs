using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    public class BoxRepresentation
    {
        public const string FieldContent = "content";
        public const string FieldInfo = "info";
        public const string FieldProperties = "properties";
        public const string FieldRepresentation = "representation";
        public const string FieldStatus = "status";

        /// <summary>
        /// Contains the url template of the representation requested
        /// </summary>
        [JsonProperty(PropertyName = FieldContent)]
        public BoxRepresentationContent Content { get; set; }

        /// <summary>
        /// The url info regarding the representation
        /// </summary>
        [JsonProperty(PropertyName = FieldInfo)]
        public BoxRepresentationInfo Info { get; set; }

        /// <summary>
        /// The properties of the generated representation
        /// </summary>
        [JsonProperty(PropertyName = FieldProperties)]
        public BoxRepresentationProperties Properties { get; set; }

        /// <summary>
        /// The representation type requested 
        /// </summary>
        [JsonProperty(PropertyName = FieldRepresentation)]
        public string Representation { get; set; }

        /// <summary>
        /// The status state of the representation requested
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public BoxRepresentationStatus Status { get; set; }
    }
}
