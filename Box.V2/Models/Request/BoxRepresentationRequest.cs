using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxRepresentationRequest
    {
        /// <summary>
        /// The id of the Box file a representation is requested on
        /// </summary>
        [JsonProperty(PropertyName = "file_id")]
        public string FileId { get; set; }

        /// <summary>
        /// The representation type requested for a Box file
        /// </summary>
        [JsonProperty(PropertyName = "x-rep-hints")]
        public string XRepHints { get; set; }

        /// <summary>
        /// String value that should be set to either inline(open in browser) or attachment(download from browser)
        /// </summary>
        [JsonProperty(PropertyName = "set_content_disposition_type")]
        public string SetContentDispositionType { get; set; }

        /// <summary>
        /// Sets the downloaded representation's file name, if not defined the name will be the default Box file name
        /// </summary>
        [JsonProperty(PropertyName = "set_content_diposition_filename")]
        public string SetContentDispositionFilename { get; set; }
    }
}
