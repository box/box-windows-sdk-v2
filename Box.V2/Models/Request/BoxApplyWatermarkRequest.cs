using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxApplyWatermarkRequest
    {
        /// <summary>
        /// Object containing watermark object params
        /// </summary>
        [JsonProperty(PropertyName = "watermark")]
        public BoxWatermarkRequest Watermark { get; set; }
    }
}
