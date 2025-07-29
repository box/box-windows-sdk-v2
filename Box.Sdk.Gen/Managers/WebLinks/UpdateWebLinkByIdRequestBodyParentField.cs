using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateWebLinkByIdRequestBodyParentField : ISerializable {
        /// <summary>
        /// The ID of parent item.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The input for `user_id` is optional. Moving to non-root folder is not allowed when `user_id` is present. Parent folder id should be zero when `user_id` is provided.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; init; }

        public UpdateWebLinkByIdRequestBodyParentField() {
            
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