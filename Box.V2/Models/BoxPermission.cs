using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxPermission
    {


        [JsonProperty("can_download")]
        public bool CanDownload { get; set; }

        [JsonProperty("can_preview")]
        public bool CanPreview { get; set; }
    }
}
