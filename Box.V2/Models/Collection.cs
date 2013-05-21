using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class Collection<T> where T : class, new()
    {
        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = "entries")]
        public List<T> Entries { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }

    }
}
