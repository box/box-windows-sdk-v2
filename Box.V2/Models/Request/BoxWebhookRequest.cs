using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxWebhookRequest : BoxItemRequest
    {
        /// <summary>
        /// The target file or folder.
        /// </summary>
        [JsonProperty(PropertyName = "target")]
        public BoxRequestEntity Target { get; set; }

        /// <summary>
        /// Event types that trigger notifications for the target. For a list of triggers see https://docs.box.com/reference#event-triggers.
        /// </summary>
        [JsonProperty(PropertyName = "triggers")]
        public IList<string> Triggers { get; set; }

        /// <summary>
        /// HTTPS URL to receive the webhook notification.
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
    }
}
