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

        /// <summary>
        /// Boolean value, set to true to try again to make a callout to get representations endpoint if initial call did not return
        /// complete representation object for file. Set to false if you do not wish to make a callout to representations endpoint again
        /// </summary>
        [JsonProperty(PropertyName = "handle_retry")]
        public bool HandleRetry { get; set; }
    }
}
