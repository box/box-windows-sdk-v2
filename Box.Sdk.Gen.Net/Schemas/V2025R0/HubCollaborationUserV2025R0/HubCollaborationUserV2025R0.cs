using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubCollaborationUserV2025R0 : UserBaseV2025R0, ISerializable {
        /// <summary>
        /// The display name of this user. If the collaboration status is `pending`, an empty string is returned.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The primary email address of this user. If the collaboration status is `pending`, an empty string is returned.
        /// </summary>
        [JsonPropertyName("login")]
        public string? Login { get; init; }

        public HubCollaborationUserV2025R0(string id, UserBaseV2025R0TypeField type = UserBaseV2025R0TypeField.User) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal HubCollaborationUserV2025R0(string id, StringEnum<UserBaseV2025R0TypeField> type) : base(id, type ?? new StringEnum<UserBaseV2025R0TypeField>(UserBaseV2025R0TypeField.User)) {
            
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