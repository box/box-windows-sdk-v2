using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxEmail
    {
        public const string FieldAccess = "access";
        public const string FieldEmail = "email";

        [JsonProperty(PropertyName = FieldAccess)]
        public string Acesss { get; private set; }

        [JsonProperty(PropertyName = FieldEmail)]
        public string Address { get; private set; }
    }
}
