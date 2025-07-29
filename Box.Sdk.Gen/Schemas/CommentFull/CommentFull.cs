using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class CommentFull : Comment, ISerializable {
        /// <summary>
        /// The string representing the comment text with
        /// @mentions included. @mention format is @[id:username]
        /// where `id` is user's Box ID and `username` is
        /// their display name.
        /// </summary>
        [JsonPropertyName("tagged_message")]
        public string? TaggedMessage { get; init; }

        public CommentFull() {
            
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}