using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// A request class for making comment requests
    /// </summary>
    public class BoxCommentRequest
    {
        /// <summary>
        /// The item that this comment will be placed on.
        /// </summary>
        [JsonProperty(PropertyName = "item")]
        public BoxRequestEntity Item { get; set; }

        /// <summary>
        /// The text body of the comment
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// The text body of the comment, including @[userid:Username] (id is user_id and username is display name) somewhere in the message to mention the user,
        /// which will send them a direct email, letting them know they’ve been mentioned in a comment.
        /// </summary>
        [JsonProperty(PropertyName = "tagged_message")]
        public string TaggedMessage { get; set; }

    }
}
