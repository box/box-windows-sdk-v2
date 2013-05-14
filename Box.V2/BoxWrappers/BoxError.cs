using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2
{
    public class BoxError
    {
        /// <summary>
        /// The error received. This value will always be present in the event of an error
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        /// <summary>
        /// Description of what happened. Provides additional information to the error
        /// </summary>
        [JsonProperty(PropertyName = "error_description")]
        public string Description { get; set; }
    }
}
