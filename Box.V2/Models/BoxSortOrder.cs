using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxSortOrder
    {
        [JsonProperty(PropertyName = "by")]
        public BoxSortBy By { get; private set; }

        [JsonProperty(PropertyName = "direction")]
        public BoxSortDirection Direction { get; private set; }
    }
}
