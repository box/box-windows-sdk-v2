using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxWatermarkRequest
    {
        public const string DefaultImprintString = "default";

        /// <summary>
        /// Currently, the value must be "default", as custom watermarks is not yet supported.
        /// </summary>
        [JsonProperty(PropertyName = "imprint")]
        public string Imprint { get; set; } = DefaultImprintString;
    }
}
