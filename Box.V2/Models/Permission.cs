using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class Permission
    {
        [JsonProperty("can_download")]
        public bool CanDownload { get; set; }

        [JsonProperty("can_preview")]
        public bool CanPreview { get; set; }
    }
}
