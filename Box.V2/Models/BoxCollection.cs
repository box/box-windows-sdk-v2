using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public abstract class BoxCollection
    {
        public const string FieldTotalCount = "total_count";
        public const string FieldEntries = "entries";
        public const string FieldOffset = "offset";
        public const string FieldLimit = "limit";
    }
    
    public class BoxCollection<T> : BoxCollection 
        where T : class, new()
    {
        [JsonProperty(PropertyName = FieldTotalCount)]
        public int TotalCount { get; private set; }

        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; private set; }

        [JsonProperty(PropertyName = FieldOffset)]
        public int Offset { get; private set; }

        [JsonProperty(PropertyName = FieldLimit)]
        public int Limit { get; private set; }

    }
}
