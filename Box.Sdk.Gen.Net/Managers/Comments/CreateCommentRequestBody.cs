using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateCommentRequestBody : ISerializable {
        /// <summary>
        /// The text of the comment.
        /// 
        /// To mention a user, use the `tagged_message`
        /// parameter instead.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; }

        /// <summary>
        /// The text of the comment, including `@[user_id:name]`
        /// somewhere in the message to mention another user, which
        /// will send them an email notification, letting them know
        /// they have been mentioned.
        /// 
        /// The `user_id` is the target user's ID, where the `name`
        /// can be any custom phrase. In the Box UI this name will
        /// link to the user's profile.
        /// 
        /// If you are not mentioning another user, use `message`
        /// instead.
        /// </summary>
        [JsonPropertyName("tagged_message")]
        public string? TaggedMessage { get; init; }

        /// <summary>
        /// The item to attach the comment to.
        /// </summary>
        [JsonPropertyName("item")]
        public CreateCommentRequestBodyItemField Item { get; }

        public CreateCommentRequestBody(string message, CreateCommentRequestBodyItemField item) {
            Message = message;
            Item = item;
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}