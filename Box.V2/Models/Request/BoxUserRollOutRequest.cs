using Newtonsoft.Json;
using System.Collections.Generic;


namespace Box.V2.Models
{
    /// <summary>
    /// A request class for rolling users out of box
    /// </summary>
    public class BoxUserRollOutRequest : BoxRequestEntity
    {
        /// <summary>
        /// Setting this to null will roll the user out of the enterprise and make them a free user
        /// </summary>
        [JsonProperty(PropertyName = "enterprise")]
        public string Enterprise { get; set; }

    }
}
