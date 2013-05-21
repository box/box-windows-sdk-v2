using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class MiniFolder : BoxEntity
    {
        /// <summary>
        /// A unique ID for use with the /events endpoint
        /// </summary>
        [JsonProperty(PropertyName = "sequence_id")]
        public string SequenceId { get; set; }

        /// <summary>
        /// The name of the folder
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
