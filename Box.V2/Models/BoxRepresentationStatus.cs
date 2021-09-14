using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxRepresentationStatus
    {
        public const string FieldState = "state";

        /// <summary>
        /// The status on generating the representation
        /// </summary>
        [JsonProperty(PropertyName = FieldState)]
        public virtual string State { get; private set; }
    }
}
