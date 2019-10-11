using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxRepresentationInfo
    {
        public const string FieldUrl = "url";

        /// <summary>
        /// The available representations information
        /// </summary>
        [JsonProperty(PropertyName = FieldUrl)]
        public virtual string Url { get; private set; }
    }
}
