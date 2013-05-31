using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxCollaboration : BoxEntity
    {
        /// <summary>
        /// The user who created this collaboration
        /// </summary>
        [JsonProperty(PropertyName = "created_by")]
        public BoxUser CreatedBy { get; set; }

        /// <summary>
        /// The time this collaboration was created
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The time this collaboration was last modified
        /// </summary>
        [JsonProperty(PropertyName = "modified_at")]
        public DateTime? ModifiedAt { get; set; }
        
        /// <summary>
        /// The time this collaboration will expire
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime? ExpiresAt { get; set; }
        
        /// <summary>
        /// he status of this collab. Can be accepted, pending, or rejected
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        
        /// <summary>
        /// The user who the collaboration applies to
        /// </summary>
        [JsonProperty(PropertyName = "accessible_by")]
        public BoxUser AccessibleBy { get; set; }
        
        /// <summary>
        /// The level of access this user has. Can be editor, viewer, previewer, uploader, previewer uploader, 
        /// viewer uploader, or co-owner
        /// </summary>
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
        
        /// <summary>
        /// When the status of this collab was changed
        /// </summary>
        [JsonProperty(PropertyName = "acknowledged_at")]
        public DateTime? AcknowledgedAt { get; set; }
        
        /// <summary>
        /// The folder this discussion is related to
        /// </summary>
        [JsonProperty(PropertyName = "item")]
        public BoxFolder Item { get; set; }
    }
}
