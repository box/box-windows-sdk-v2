using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class Email
    {
        [JsonProperty(PropertyName="access")]
        public string Acesss { get; set; }

        [JsonProperty(PropertyName="email")]
        public string Address { get; set; }
    }
}
