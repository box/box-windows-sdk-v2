using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making email alias requests
    /// </summary>
    public class BoxEmailAliasRequest
    {
        private const string FieldEmail = "email";

        [JsonProperty(PropertyName = FieldEmail)]
        public string Email { get; set; }
    }
}
