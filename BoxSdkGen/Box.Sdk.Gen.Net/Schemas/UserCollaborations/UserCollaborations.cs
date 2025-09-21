using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class UserCollaborations : UserBase, ISerializable {
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

        /// <summary>
        /// If set to `false`, the user is either deactivated or deleted.
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool? IsActive { get; init; }

        public UserCollaborations(string id, UserBaseTypeField type = UserBaseTypeField.User) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal UserCollaborations(string id, StringEnum<UserBaseTypeField> type) : base(id, type ?? new StringEnum<UserBaseTypeField>(UserBaseTypeField.User)) {
            
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