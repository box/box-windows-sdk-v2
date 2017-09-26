using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxRepresentationStatus
    {
        public const string FieldStatus = "status";

        /// <summary>
        /// The status on generating the representation
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public string Status { get; private set; }
    }
}
