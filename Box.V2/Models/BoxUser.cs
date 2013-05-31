using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxUser : BoxEntity
    {
        /// <summary>
        /// The name of this user
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The email address this user uses to login
        /// </summary>
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }
    }
}
