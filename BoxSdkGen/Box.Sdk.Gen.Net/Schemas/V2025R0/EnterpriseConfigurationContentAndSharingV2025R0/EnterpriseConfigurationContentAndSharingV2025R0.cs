using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationContentAndSharingV2025R0 : ISerializable {
        [JsonPropertyName("enterprise_feature_settings")]
        public IReadOnlyList<EnterpriseFeatureSettingsItemV2025R0>? EnterpriseFeatureSettings { get; init; }

        [JsonPropertyName("sharing_item_type")]
        public EnterpriseConfigurationItemStringV2025R0? SharingItemType { get; init; }

        [JsonPropertyName("shared_link_company_definition")]
        public EnterpriseConfigurationItemStringV2025R0? SharedLinkCompanyDefinition { get; init; }

        [JsonPropertyName("shared_link_access")]
        public EnterpriseConfigurationItemStringV2025R0? SharedLinkAccess { get; init; }

        [JsonPropertyName("shared_link_default_access")]
        public EnterpriseConfigurationItemStringV2025R0? SharedLinkDefaultAccess { get; init; }

        [JsonPropertyName("shared_link_default_permissions_selected")]
        public EnterpriseConfigurationContentAndSharingV2025R0SharedLinkDefaultPermissionsSelectedField? SharedLinkDefaultPermissionsSelected { get; init; }

        [JsonPropertyName("is_open_custom_urls_disabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsOpenCustomUrlsDisabled { get; init; }

        [JsonPropertyName("is_custom_domain_hidden_in_shared_link")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsCustomDomainHiddenInSharedLink { get; init; }

        [JsonPropertyName("collaboration_permissions")]
        public EnterpriseConfigurationContentAndSharingV2025R0CollaborationPermissionsField? CollaborationPermissions { get; init; }

        [JsonPropertyName("default_collaboration_role")]
        public EnterpriseConfigurationItemStringV2025R0? DefaultCollaborationRole { get; init; }

        [JsonPropertyName("is_invite_privilege_restricted")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsInvitePrivilegeRestricted { get; init; }

        [JsonPropertyName("collaboration_restrictions")]
        public EnterpriseConfigurationContentAndSharingV2025R0CollaborationRestrictionsField? CollaborationRestrictions { get; init; }

        [JsonPropertyName("is_collaborator_invite_links_disabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsCollaboratorInviteLinksDisabled { get; init; }

        [JsonPropertyName("is_invite_group_collaborator_disabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsInviteGroupCollaboratorDisabled { get; init; }

        [JsonPropertyName("is_ownership_transfer_restricted")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsOwnershipTransferRestricted { get; init; }

        [JsonPropertyName("external_collaboration_status")]
        public EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationStatusField? ExternalCollaborationStatus { get; init; }

        [JsonPropertyName("external_collaboration_allowlist_users")]
        public EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationAllowlistUsersField? ExternalCollaborationAllowlistUsers { get; init; }

        [JsonPropertyName("is_watermarking_enterprise_feature_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsWatermarkingEnterpriseFeatureEnabled { get; init; }

        [JsonPropertyName("is_root_content_creation_restricted")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsRootContentCreationRestricted { get; init; }

        [JsonPropertyName("is_tag_creation_restricted")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsTagCreationRestricted { get; init; }

        [JsonPropertyName("tag_creation_restriction")]
        public EnterpriseConfigurationItemStringV2025R0? TagCreationRestriction { get; init; }

        [JsonPropertyName("is_email_uploads_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsEmailUploadsEnabled { get; init; }

        [JsonPropertyName("is_custom_settings_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsCustomSettingsEnabled { get; init; }

        [JsonPropertyName("is_forms_login_required")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsFormsLoginRequired { get; init; }

        [JsonPropertyName("is_forms_branding_default_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsFormsBrandingDefaultEnabled { get; init; }

        [JsonPropertyName("is_cc_free_trial_active")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsCcFreeTrialActive { get; init; }

        [JsonPropertyName("is_file_request_editors_allowed")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsFileRequestEditorsAllowed { get; init; }

        [JsonPropertyName("is_file_request_branding_default_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsFileRequestBrandingDefaultEnabled { get; init; }

        [JsonPropertyName("is_file_request_login_required")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsFileRequestLoginRequired { get; init; }

        [JsonPropertyName("is_shared_links_expiration_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsSharedLinksExpirationEnabled { get; init; }

        [JsonPropertyName("shared_links_expiration_days")]
        public EnterpriseConfigurationItemIntegerV2025R0? SharedLinksExpirationDays { get; init; }

        [JsonPropertyName("is_public_shared_links_expiration_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsPublicSharedLinksExpirationEnabled { get; init; }

        [JsonPropertyName("public_shared_links_expiration_days")]
        public EnterpriseConfigurationItemIntegerV2025R0? PublicSharedLinksExpirationDays { get; init; }

        [JsonPropertyName("shared_expiration_target")]
        public EnterpriseConfigurationItemStringV2025R0? SharedExpirationTarget { get; init; }

        [JsonPropertyName("is_shared_links_expiration_notification_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsSharedLinksExpirationNotificationEnabled { get; init; }

        [JsonPropertyName("shared_links_expiration_notification_days")]
        public EnterpriseConfigurationItemIntegerV2025R0? SharedLinksExpirationNotificationDays { get; init; }

        [JsonPropertyName("is_shared_links_expiration_notification_prevented")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsSharedLinksExpirationNotificationPrevented { get; init; }

        [JsonPropertyName("is_auto_delete_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsAutoDeleteEnabled { get; init; }

        [JsonPropertyName("auto_delete_days")]
        public EnterpriseConfigurationItemIntegerV2025R0? AutoDeleteDays { get; init; }

        [JsonPropertyName("is_auto_delete_expiration_modification_prevented")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsAutoDeleteExpirationModificationPrevented { get; init; }

        [JsonPropertyName("auto_delete_target")]
        public EnterpriseConfigurationItemStringV2025R0? AutoDeleteTarget { get; init; }

        [JsonPropertyName("is_collaboration_expiration_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsCollaborationExpirationEnabled { get; init; }

        [JsonPropertyName("collaboration_expiration_days")]
        public EnterpriseConfigurationItemIntegerV2025R0? CollaborationExpirationDays { get; init; }

        [JsonPropertyName("is_collaboration_expiration_modification_prevented")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsCollaborationExpirationModificationPrevented { get; init; }

        [JsonPropertyName("is_collaboration_expiration_notification_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsCollaborationExpirationNotificationEnabled { get; init; }

        [JsonPropertyName("collaboration_expiration_target")]
        public EnterpriseConfigurationItemStringV2025R0? CollaborationExpirationTarget { get; init; }

        [JsonPropertyName("trash_auto_clear_time")]
        public EnterpriseConfigurationItemIntegerV2025R0? TrashAutoClearTime { get; init; }

        [JsonPropertyName("permanent_deletion_access")]
        public EnterpriseConfigurationItemStringV2025R0? PermanentDeletionAccess { get; init; }

        [JsonPropertyName("permanent_deletion_allowlist_users")]
        public EnterpriseConfigurationContentAndSharingV2025R0PermanentDeletionAllowlistUsersField? PermanentDeletionAllowlistUsers { get; init; }

        public EnterpriseConfigurationContentAndSharingV2025R0() {
            
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