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
        public virtual BoxSortBy By { get; private set; }

        [JsonProperty(PropertyName = "sort")]
        public virtual string Sort { get; private set; }

        [JsonProperty(PropertyName = "direction")]
        public virtual BoxSortDirection Direction { get; private set; }
    }

}
