using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxEmailRequest
    {
        [JsonProperty(PropertyName = "access")]
        public string Acesss { get; private set; }

        //[JsonProperty(PropertyName = "email")]
        //public string Address { get; private set; }
    }
}
