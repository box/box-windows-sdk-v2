using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class GroupBaseV2025R0 : ISerializable {
        /// <summary>
        /// The unique identifier for this object.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `group`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<GroupBaseV2025R0TypeField>))]
        public StringEnum<GroupBaseV2025R0TypeField> Type { get; }

        public GroupBaseV2025R0(string id, GroupBaseV2025R0TypeField type = GroupBaseV2025R0TypeField.Group) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal GroupBaseV2025R0(string id, StringEnum<GroupBaseV2025R0TypeField> type) {
            Id = id;
            Type = GroupBaseV2025R0TypeField.Group;
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