using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class GroupBase : ISerializable {
        /// <summary>
        /// The unique identifier for this object.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `group`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<GroupBaseTypeField>))]
        public StringEnum<GroupBaseTypeField> Type { get; }

        public GroupBase(string id, GroupBaseTypeField type = GroupBaseTypeField.Group) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal GroupBase(string id, StringEnum<GroupBaseTypeField> type) {
            Id = id;
            Type = GroupBaseTypeField.Group;
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