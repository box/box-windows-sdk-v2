using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxFolderRequest : BoxRequestEntity
    {
        /// <summary>
        /// The name of the item
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The folder that contains this one
        /// </summary>
        [JsonProperty(PropertyName = "parent")]
        public BoxRequestEntity Parent { get; set; }
    }
}
