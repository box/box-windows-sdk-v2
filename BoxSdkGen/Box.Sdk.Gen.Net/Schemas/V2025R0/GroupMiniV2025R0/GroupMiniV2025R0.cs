using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class GroupMiniV2025R0 : GroupBaseV2025R0, ISerializable {
        /// <summary>
        /// The name of the group.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The type of the group.
        /// </summary>
        [JsonPropertyName("group_type")]
        [JsonConverter(typeof(StringEnumConverter<GroupMiniV2025R0GroupTypeField>))]
        public StringEnum<GroupMiniV2025R0GroupTypeField>? GroupType { get; init; }

        public GroupMiniV2025R0(string id, GroupBaseV2025R0TypeField type = GroupBaseV2025R0TypeField.Group) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal GroupMiniV2025R0(string id, StringEnum<GroupBaseV2025R0TypeField> type) : base(id, type ?? new StringEnum<GroupBaseV2025R0TypeField>(GroupBaseV2025R0TypeField.Group)) {
            
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