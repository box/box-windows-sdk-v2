using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class GroupMembership : ISerializable {
        /// <summary>
        /// The unique identifier for this group membership.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The value will always be `group_membership`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<GroupMembershipTypeField>))]
        public StringEnum<GroupMembershipTypeField>? Type { get; init; }

        [JsonPropertyName("user")]
        public UserMini? User { get; init; }

        [JsonPropertyName("group")]
        public GroupMini? Group { get; init; }

        /// <summary>
        /// The role of the user in the group.
        /// </summary>
        [JsonPropertyName("role")]
        [JsonConverter(typeof(StringEnumConverter<GroupMembershipRoleField>))]
        public StringEnum<GroupMembershipRoleField>? Role { get; init; }

        /// <summary>
        /// The time this membership was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// The time this membership was last modified.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; init; }

        public GroupMembership() {
            
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