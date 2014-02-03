using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a comment
    /// </summary>
    public class BoxComment : BoxItem
    {
        public const string FieldIsReplyComment = "is_reply_comment";
        public const string FieldMessage = "message";
        public const string FieldTaggedMessage = "tagged_message";
        public const string FieldItem = "item";

        /// <summary>
        /// Whether or not this comment is a reply to another comment
        /// </summary>
        [JsonProperty(PropertyName = FieldIsReplyComment)]
        public bool IsReplyComment { get; set; }

        /// <summary>
        /// The comment text that the user typed
        /// </summary>
        [JsonProperty(PropertyName = FieldMessage)]
        public string Message { get; set; }

        /// <summary>
        /// The string representing the comment text with @mentions included. 
        /// @mention format is @[id:username]. Field is not included by default.
        /// </summary>
        [JsonProperty(PropertyName = FieldTaggedMessage)]
        public string TaggedMessage { get; set; }

        /// <summary>
        /// The object this comment was placed on
        /// </summary>
        [JsonProperty(PropertyName = FieldItem)]
        public BoxItem Item { get; set; }
    }
}



