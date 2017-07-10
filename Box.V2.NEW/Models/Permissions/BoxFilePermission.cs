using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxFilePermission : BoxItemPermission
    {

        /// <summary>
        /// Permission to view the file preview
        /// </summary>
        [JsonProperty(PropertyName = "can_preview")]
        public bool CanPreview { get; private set; }
    }
}
