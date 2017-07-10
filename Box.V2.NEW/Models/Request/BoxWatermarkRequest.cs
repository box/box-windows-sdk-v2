using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxWatermarkRequest
    {
        public const string DefaultImprintString = "default";

        private string imprint = DefaultImprintString;
        /// <summary>
        /// Currently, the value must be "default", as custom watermarks is not yet supported.
        /// </summary>
        [JsonProperty(PropertyName = "imprint")]
        public string Imprint
        {
            get
            {
                return imprint;
            }
            set
            {
                imprint = value;
            }
        }
    }
}
