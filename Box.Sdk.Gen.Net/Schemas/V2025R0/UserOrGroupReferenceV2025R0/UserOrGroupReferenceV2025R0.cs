using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class UserOrGroupReferenceV2025R0 : ISerializable {
        /// <summary>
        /// The type `user` or `group`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<UserOrGroupReferenceV2025R0TypeField>))]
        public StringEnum<UserOrGroupReferenceV2025R0TypeField>? Type { get; init; }

        /// <summary>
        /// The identifier of the user or group.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        public UserOrGroupReferenceV2025R0() {
            
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