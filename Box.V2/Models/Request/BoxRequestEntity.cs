using Box.V2.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxRequestEntity
    {
        /// <summary>
        /// The Entity's Id
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of the item 
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxType? Type { get; set; } 
    }
}


public enum BoxType
{
    file, 
    discussion, 
    comment,
    folder,
}
