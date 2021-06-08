using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxRepresentationContent
    {
        public const string FieldUrlTemplate = "url_template";

        /// <summary>
        /// The available representations url template
        /// </summary>
        [JsonProperty(PropertyName = FieldUrlTemplate)]
        public virtual string UrlTemplate { get; private set; }
    }
}
