using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class UserBase : ISerializable {
        /// <summary>
        /// The unique identifier for this user.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `user`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<UserBaseTypeField>))]
        public StringEnum<UserBaseTypeField> Type { get; }

        public UserBase(string id, UserBaseTypeField type = UserBaseTypeField.User) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal UserBase(string id, StringEnum<UserBaseTypeField> type) {
            Id = id;
            Type = UserBaseTypeField.User;
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