using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class UserBaseV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier for this user.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `user`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<UserBaseV2026R0TypeField>))]
        public StringEnum<UserBaseV2026R0TypeField> Type { get; set; }

        public UserBaseV2026R0(string id, UserBaseV2026R0TypeField type = UserBaseV2026R0TypeField.User) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal UserBaseV2026R0(string id, StringEnum<UserBaseV2026R0TypeField> type) {
            Id = id;
            Type = UserBaseV2026R0TypeField.User;
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}