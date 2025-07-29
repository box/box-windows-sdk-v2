using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class HubCollaborationCreateRequestV2025R0AccessibleByField : ISerializable {
        /// <summary>
        /// The type of collaborator to invite.
        /// Possible values are `user` or `group`.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; }

        /// <summary>
        /// The ID of the user or group.
        /// 
        /// Alternatively, use `login` to specify a user by email
        /// address.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The email address of the user who gets access to the item.
        /// 
        /// Alternatively, use `id` to specify a user by user ID.
        /// </summary>
        [JsonPropertyName("login")]
        public string? Login { get; init; }

        public HubCollaborationCreateRequestV2025R0AccessibleByField(string type) {
            Type = type;
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