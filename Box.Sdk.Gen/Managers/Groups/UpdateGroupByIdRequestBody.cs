using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateGroupByIdRequestBody : ISerializable {
        /// <summary>
        /// The name of the new group to be created. Must be unique within the
        /// enterprise.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// Keeps track of which external source this group is
        /// coming, for example `Active Directory`, or `Okta`.
        /// 
        /// Setting this will also prevent Box admins from editing
        /// the group name and its members directly via the Box
        /// web application.
        /// 
        /// This is desirable for one-way syncing of groups.
        /// </summary>
        [JsonPropertyName("provenance")]
        public string? Provenance { get; init; }

        /// <summary>
        /// An arbitrary identifier that can be used by
        /// external group sync tools to link this Box Group to
        /// an external group.
        /// 
        /// Example values of this field
        /// could be an **Active Directory Object ID** or a **Google
        /// Group ID**.
        /// 
        /// We recommend you use of this field in
        /// order to avoid issues when group names are updated in
        /// either Box or external systems.
        /// </summary>
        [JsonPropertyName("external_sync_identifier")]
        public string? ExternalSyncIdentifier { get; init; }

        /// <summary>
        /// A human readable description of the group.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// Specifies who can invite the group to collaborate
        /// on folders.
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
        [JsonConverter(typeof(StringEnumConverter<UpdateGroupByIdRequestBodyInvitabilityLevelField>))]
        public StringEnum<UpdateGroupByIdRequestBodyInvitabilityLevelField>? InvitabilityLevel { get; init; }

        /// <summary>
        /// Specifies who can see the members of the group.
        /// 
        /// * `admins_only` - the enterprise admin, co-admins, group's
        ///   group admin.
        /// * `admins_and_members` - all admins and group members.
        /// * `all_managed_users` - all managed users in the
        ///   enterprise.
        /// </summary>
        [JsonPropertyName("member_viewability_level")]
        [JsonConverter(typeof(StringEnumConverter<UpdateGroupByIdRequestBodyMemberViewabilityLevelField>))]
        public StringEnum<UpdateGroupByIdRequestBodyMemberViewabilityLevelField>? MemberViewabilityLevel { get; init; }

        public UpdateGroupByIdRequestBody() {
            
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