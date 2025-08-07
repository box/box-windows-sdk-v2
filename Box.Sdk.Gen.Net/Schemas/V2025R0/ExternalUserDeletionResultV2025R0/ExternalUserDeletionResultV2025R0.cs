using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ExternalUserDeletionResultV2025R0 : ISerializable {
        /// <summary>
        /// The ID of the external user.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; }

        /// <summary>
        /// HTTP status code for a specific user's deletion request.
        /// </summary>
        [JsonPropertyName("status")]
        public long Status { get; }

        /// <summary>
        /// Deletion request status details.
        /// </summary>
        [JsonPropertyName("detail")]
        public string? Detail { get; init; }

        public ExternalUserDeletionResultV2025R0(string userId, long status) {
            UserId = userId;
            Status = status;
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