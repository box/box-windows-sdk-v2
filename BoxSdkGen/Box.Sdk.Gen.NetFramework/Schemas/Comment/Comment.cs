using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class Comment : CommentBase, ISerializable {
        /// <summary>
        /// Whether or not this comment is a reply to another
        /// comment.
        /// </summary>
        [JsonPropertyName("is_reply_comment")]
        public bool? IsReplyComment { get; set; }

        /// <summary>
        /// The text of the comment, as provided by the user.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("created_by")]
        public UserMini CreatedBy { get; set; }

        /// <summary>
        /// The time this comment was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The time this comment was last modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; set; }

        [JsonPropertyName("item")]
        public CommentItemField Item { get; set; }

        public Comment() {
            
        }
        internal new string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}