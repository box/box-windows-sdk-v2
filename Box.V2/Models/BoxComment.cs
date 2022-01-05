using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a comment
    /// </summary>
    public class BoxComment : BoxEntity
    {
        public const string FieldIsReplyComment = "is_reply_comment";
        public const string FieldMessage = "message";
        public const string FieldTaggedMessage = "tagged_message";
        public const string FieldItem = "item";
        public const string FieldCreatedBy = "created_by";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";

        /// <summary>
        /// Whether or not this comment is a reply to another comment
        /// </summary>
        [JsonProperty(PropertyName = FieldIsReplyComment)]
        public virtual bool IsReplyComment { get; set; }

        /// <summary>
        /// The comment text that the user typed
        /// </summary>
        [JsonProperty(PropertyName = FieldMessage)]
        public virtual string Message { get; set; }

        /// <summary>
        /// The string representing the comment text with @mentions included. 
        /// @mention format is @[id:username]. Field is not included by default.
        /// </summary>
        [JsonProperty(PropertyName = FieldTaggedMessage)]
        public virtual string TaggedMessage { get; set; }

        /// <summary>
        /// A mini user object representing the author of the comment
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public virtual BoxUser CreatedBy { get; set; }
        /// <summary>
        /// The time this comment was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; set; }
        /// <summary>
        /// The time this comment was last modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; set; }

        /// <summary>
        /// The object this comment was placed on
        /// </summary>
        [JsonProperty(PropertyName = FieldItem)]
        public virtual BoxEntity Item { get; set; }
    }
}



