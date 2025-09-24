using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxWatermarkResponse : BoxEntity
    {
        public const string FieldWatermark = "watermark";

        /// <summary>
        /// Watermark wrapper
        /// </summary>
        [JsonProperty(PropertyName = FieldWatermark)]
        public virtual BoxWatermark Watermark { get; private set; }
    }
}
