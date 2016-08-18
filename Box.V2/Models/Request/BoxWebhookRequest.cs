using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxWebhookRequest : BoxItemRequest
    {
        [JsonProperty(PropertyName = "target")]
        public BoxRequestEntity Target { get; set; }

        [JsonProperty(PropertyName = "triggers")]
        public IList<string> Triggers { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
    }
}
