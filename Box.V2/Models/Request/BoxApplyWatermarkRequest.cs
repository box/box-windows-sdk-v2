using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxApplyWatermarkRequest
    {
        private BoxWatermarkRequest watermark = new BoxWatermarkRequest();
        /// <summary>
        /// Object containing watermark object params
        /// </summary>
        [JsonProperty(PropertyName = "watermark")]
        public BoxWatermarkRequest Watermark 
        { 
            get
            {
                return watermark;
            }
            set
            {
                watermark = value;
            }
        }
    }
}
