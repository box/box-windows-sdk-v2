using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class UserReferenceV2025R0 : ISerializable {
        /// <summary>
        /// The value is always `user`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<UserReferenceV2025R0TypeField>))]
        public StringEnum<UserReferenceV2025R0TypeField> Type { get; }

        /// <summary>
        /// The unique identifier for the user.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public UserReferenceV2025R0(string id, UserReferenceV2025R0TypeField type = UserReferenceV2025R0TypeField.User) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal UserReferenceV2025R0(string id, StringEnum<UserReferenceV2025R0TypeField> type) {
            Type = UserReferenceV2025R0TypeField.User;
            Id = id;
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