using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxWatermarkResponse : BoxEntity
    {
        public const string FieldWatermark = "watermark";

        /// <summary>
        /// Watermark wrapper
        /// </summary>
        [JsonProperty(PropertyName = FieldWatermark)]
        public BoxWatermark Watermark { get; private set; }
    }
}
