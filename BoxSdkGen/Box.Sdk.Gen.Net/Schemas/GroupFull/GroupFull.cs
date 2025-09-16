using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class GroupFull : Group, ISerializable {
        /// <summary>
        /// Keeps track of which external source this group is
        /// coming from (e.g. "Active Directory", "Google Groups",
        /// "Facebook Groups").  Setting this will
        /// also prevent Box users from editing the group name
        /// and its members directly via the Box web application.
        /// This is desirable for one-way syncing of groups.
        /// </summary>
        [JsonPropertyName("provenance")]
        public string? Provenance { get; init; }

        /// <summary>
        /// An arbitrary identifier that can be used by
        /// external group sync tools to link this Box Group to
        /// an external group. Example values of this field
        /// could be an Active Directory Object ID or a Google
        /// Group ID.  We recommend you use of this field in
        /// order to avoid issues when group names are updated in
        /// either Box or external systems.
        /// </summary>
        [JsonPropertyName("external_sync_identifier")]
        public string? ExternalSyncIdentifier { get; init; }

        /// <summary>
        /// Human readable description of the group.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// Specifies who can invite the group to collaborate
        /// on items.
        /// 
        /// When set to `admins_only` the enterprise admin, co-admins,
        /// and the group's admin can invite the group.
        /// 
        /// When set to `admins_and_members` all the admins listed
        /// above and group members can invite the group.
        /// 
        /// When set to `all_managed_users` all managed users in the
        /// enterprise can invite the group.
        /// </summary>
        [JsonPropertyName("invitability_level")]
        [JsonConverter(typeof(StringEnumConverter<GroupFullInvitabilityLevelField>))]
        public StringEnum<GroupFullInvitabilityLevelField>? InvitabilityLevel { get; init; }

        /// <summary>
        /// Specifies who can view the members of the group
        /// (Get Memberships for Group).
        /// 
        /// * `admins_only` - the enterprise admin, co-admins, group's
        ///   group admin.
        /// * `admins_and_members` - all admins and group members.
        /// * `all_managed_users` - all managed users in the
        ///   enterprise.
        /// </summary>
        [JsonPropertyName("member_viewability_level")]
        [JsonConverter(typeof(StringEnumConverter<GroupFullMemberViewabilityLevelField>))]
        public StringEnum<GroupFullMemberViewabilityLevelField>? MemberViewabilityLevel { get; init; }

        [JsonPropertyName("permissions")]
        public GroupFullPermissionsField? Permissions { get; init; }

        public GroupFull(string id, GroupBaseTypeField type = GroupBaseTypeField.Group) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal GroupFull(string id, StringEnum<GroupBaseTypeField> type) : base(id, type ?? new StringEnum<GroupBaseTypeField>(GroupBaseTypeField.Group)) {
            
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