using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System.Collections.ObjectModel;
using System.Text.Json;
using System;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FolderFull : Folder, ISerializable {
        [JsonPropertyName("sync_state")]
        [JsonConverter(typeof(StringEnumConverter<FolderFullSyncStateField>))]
        public StringEnum<FolderFullSyncStateField>? SyncState { get; init; }

        /// <summary>
        /// Specifies if this folder has any other collaborators.
        /// </summary>
        [JsonPropertyName("has_collaborations")]
        public bool? HasCollaborations { get; init; }

        [JsonPropertyName("permissions")]
        public FolderFullPermissionsField? Permissions { get; init; }

        [JsonPropertyName("tags")]
        public IReadOnlyList<string>? Tags { get; init; }

        [JsonPropertyName("can_non_owners_invite")]
        public bool? CanNonOwnersInvite { get; init; }

        /// <summary>
        /// Specifies if this folder is owned by a user outside of the
        /// authenticated enterprise.
        /// </summary>
        [JsonPropertyName("is_externally_owned")]
        public bool? IsExternallyOwned { get; init; }

        [JsonPropertyName("metadata")]
        public FolderFullMetadataField? Metadata { get; init; }

        [JsonPropertyName("is_collaboration_restricted_to_enterprise")]
        public bool? IsCollaborationRestrictedToEnterprise { get; init; }

        /// <summary>
        /// A list of access levels that are available
        /// for this folder.
        /// 
        /// For some folders, like the root folder, this will always
        /// be an empty list as sharing is not allowed at that level.
        /// </summary>
        [JsonPropertyName("allowed_shared_link_access_levels")]
        [JsonConverter(typeof(StringEnumListConverter<FolderFullAllowedSharedLinkAccessLevelsField>))]
        public IReadOnlyList<StringEnum<FolderFullAllowedSharedLinkAccessLevelsField>>? AllowedSharedLinkAccessLevels { get; init; }

        /// <summary>
        /// A list of the types of roles that user can be invited at
        /// when sharing this folder.
        /// </summary>
        [JsonPropertyName("allowed_invitee_roles")]
        [JsonConverter(typeof(StringEnumListConverter<FolderFullAllowedInviteeRolesField>))]
        public IReadOnlyList<StringEnum<FolderFullAllowedInviteeRolesField>>? AllowedInviteeRoles { get; init; }

        [JsonPropertyName("watermark_info")]
        public FolderFullWatermarkInfoField? WatermarkInfo { get; init; }

        /// <summary>
        /// Specifies if the folder can be accessed
        /// with the direct shared link or a shared link
        /// to a parent folder.
        /// </summary>
        [JsonPropertyName("is_accessible_via_shared_link")]
        public bool? IsAccessibleViaSharedLink { get; init; }

        /// <summary>
        /// Specifies if collaborators who are not owners
        /// of this folder are restricted from viewing other
        /// collaborations on this folder.
        /// 
        /// It also restricts non-owners from inviting new
        /// collaborators.
        /// </summary>
        [JsonPropertyName("can_non_owners_view_collaborators")]
        public bool? CanNonOwnersViewCollaborators { get; init; }

        [JsonPropertyName("classification")]
        public FolderFullClassificationField? Classification { get; init; }

        /// <summary>
        /// This field will return true if the folder or any ancestor of the
        /// folder is associated with at least one app item. Note that this will
        /// return true even if the context user does not have access to the
        /// app item(s) associated with the folder.
        /// </summary>
        [JsonPropertyName("is_associated_with_app_item")]
        public bool? IsAssociatedWithAppItem { get; init; }

        public FolderFull(string id, FolderBaseTypeField type = FolderBaseTypeField.Folder) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal FolderFull(string id, StringEnum<FolderBaseTypeField> type) : base(id, type ?? new StringEnum<FolderBaseTypeField>(FolderBaseTypeField.Folder)) {
            
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