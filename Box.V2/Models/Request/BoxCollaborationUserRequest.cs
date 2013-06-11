using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxCollaborationUserRequest : BoxRequestEntity
    {

        /// <summary>
        /// An email address (does not need to be a Box user)
        /// </summary>
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

    }
}
