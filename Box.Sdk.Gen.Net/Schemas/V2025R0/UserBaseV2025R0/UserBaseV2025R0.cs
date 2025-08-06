using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class UserBaseV2025R0 : ISerializable {
        /// <summary>
        /// The unique identifier for this user.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `user`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<UserBaseV2025R0TypeField>))]
        public StringEnum<UserBaseV2025R0TypeField> Type { get; }

        public UserBaseV2025R0(string id, UserBaseV2025R0TypeField type = UserBaseV2025R0TypeField.User) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal UserBaseV2025R0(string id, StringEnum<UserBaseV2025R0TypeField> type) {
            Id = id;
            Type = UserBaseV2025R0TypeField.User;
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