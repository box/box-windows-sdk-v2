using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxCollection<T> where T : class, new()
    {
        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; private set; }

        [JsonProperty(PropertyName = "entries")]
        public List<T> Entries { get; private set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; private set; }

        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; private set; }

    }
}
