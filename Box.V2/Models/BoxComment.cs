using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxComment : BoxEntity
    {
        /// <summary>
        /// For comments is ‘comment’
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Whether or not this comment is a reply to another comment
        /// </summary>
        [JsonProperty(PropertyName = "is_reply_comment")]
        public bool IsReplyComment { get; set; }

        /// <summary>
        /// The comment text that the user typed
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// The string representing the comment text with @mentions included. 
        /// @mention format is @[id:username]. Field is not included by default.
        /// </summary>
        [JsonProperty(PropertyName = "tagged_message")]
        public string TaggedMessage { get; set; }

        /// <summary>
        /// A mini user object representing the author of the comment
        /// </summary>
        [JsonProperty(PropertyName = "created_by")]
        public BoxUser CreatedBy { get; set; }

        /// <summary>
        /// The time this comment was created
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; set; }

        
        /// <summary>
        /// The time this comment was last modified
        /// </summary>
        [JsonProperty(PropertyName = "modified_at")]
        public DateTime? ModifiedAt { get; set; }
        

        /// <summary>
        /// The object this comment was placed on
        /// </summary>
        [JsonProperty(PropertyName = "item")]
        public BoxEntity Item { get; set; }
    }
}



