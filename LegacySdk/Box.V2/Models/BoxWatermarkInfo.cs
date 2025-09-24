using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxWatermarkInfo
    {
        public const string FieldIsWatermarked = "is_watermarked";

        /// <summary>
        ///  Whether it is watermarked or not.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsWatermarked)]
        public virtual bool IsWatermarked { get; private set; }
    }
}
