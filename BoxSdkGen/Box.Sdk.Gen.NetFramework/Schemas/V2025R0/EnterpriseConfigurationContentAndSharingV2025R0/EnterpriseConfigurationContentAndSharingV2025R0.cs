using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationContentAndSharingV2025R0 : ISerializable {
        [JsonPropertyName("enterprise_feature_settings")]
        public IReadOnlyList<EnterpriseFeatureSettingsItemV2025R0> EnterpriseFeatureSettings { get; set; }

        [JsonPropertyName("sharing_item_type")]
        public EnterpriseConfigurationItemStringV2025R0 SharingItemType { get; set; }

        [JsonPropertyName("shared_link_company_definition")]
        public EnterpriseConfigurationItemStringV2025R0 SharedLinkCompanyDefinition { get; set; }

        [JsonPropertyName("shared_link_access")]
        public EnterpriseConfigurationItemStringV2025R0 SharedLinkAccess { get; set; }

        [JsonPropertyName("shared_link_default_access")]
        public EnterpriseConfigurationItemStringV2025R0 SharedLinkDefaultAccess { get; set; }

        [JsonPropertyName("shared_link_default_permissions_selected")]
        public EnterpriseConfigurationContentAndSharingV2025R0SharedLinkDefaultPermissionsSelectedField SharedLinkDefaultPermissionsSelected { get; set; }

        [JsonPropertyName("is_open_custom_urls_disabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsOpenCustomUrlsDisabled { get; set; }

        [JsonPropertyName("is_custom_domain_hidden_in_shared_link")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsCustomDomainHiddenInSharedLink { get; set; }

        [JsonPropertyName("collaboration_permissions")]
        public EnterpriseConfigurationContentAndSharingV2025R0CollaborationPermissionsField CollaborationPermissions { get; set; }

        [JsonPropertyName("default_collaboration_role")]
        public EnterpriseConfigurationItemStringV2025R0 DefaultCollaborationRole { get; set; }

        [JsonPropertyName("is_invite_privilege_restricted")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsInvitePrivilegeRestricted { get; set; }

        [JsonPropertyName("collaboration_restrictions")]
        public EnterpriseConfigurationContentAndSharingV2025R0CollaborationRestrictionsField CollaborationRestrictions { get; set; }

        [JsonPropertyName("is_collaborator_invite_links_disabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsCollaboratorInviteLinksDisabled { get; set; }

        [JsonPropertyName("is_invite_group_collaborator_disabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsInviteGroupCollaboratorDisabled { get; set; }

        [JsonPropertyName("is_ownership_transfer_restricted")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsOwnershipTransferRestricted { get; set; }

        [JsonPropertyName("external_collaboration_status")]
        public EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationStatusField ExternalCollaborationStatus { get; set; }

        [JsonPropertyName("external_collaboration_allowlist_users")]
        public EnterpriseConfigurationContentAndSharingV2025R0ExternalCollaborationAllowlistUsersField ExternalCollaborationAllowlistUsers { get; set; }

        [JsonPropertyName("is_watermarking_enterprise_feature_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsWatermarkingEnterpriseFeatureEnabled { get; set; }

        [JsonPropertyName("is_root_content_creation_restricted")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsRootContentCreationRestricted { get; set; }

        [JsonPropertyName("is_tag_creation_restricted")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsTagCreationRestricted { get; set; }

        [JsonPropertyName("tag_creation_restriction")]
        public EnterpriseConfigurationItemStringV2025R0 TagCreationRestriction { get; set; }

        [JsonPropertyName("is_email_uploads_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsEmailUploadsEnabled { get; set; }

        [JsonPropertyName("is_custom_settings_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsCustomSettingsEnabled { get; set; }

        [JsonPropertyName("is_forms_login_required")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsFormsLoginRequired { get; set; }

        [JsonPropertyName("is_forms_branding_default_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsFormsBrandingDefaultEnabled { get; set; }

        [JsonPropertyName("is_cc_free_trial_active")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsCcFreeTrialActive { get; set; }

        [JsonPropertyName("is_file_request_editors_allowed")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsFileRequestEditorsAllowed { get; set; }

        [JsonPropertyName("is_file_request_branding_default_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsFileRequestBrandingDefaultEnabled { get; set; }

        [JsonPropertyName("is_file_request_login_required")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsFileRequestLoginRequired { get; set; }

        [JsonPropertyName("is_shared_links_expiration_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsSharedLinksExpirationEnabled { get; set; }

        [JsonPropertyName("shared_links_expiration_days")]
        public EnterpriseConfigurationItemIntegerV2025R0 SharedLinksExpirationDays { get; set; }

        [JsonPropertyName("is_public_shared_links_expiration_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsPublicSharedLinksExpirationEnabled { get; set; }

        [JsonPropertyName("public_shared_links_expiration_days")]
        public EnterpriseConfigurationItemIntegerV2025R0 PublicSharedLinksExpirationDays { get; set; }

        [JsonPropertyName("shared_expiration_target")]
        public EnterpriseConfigurationItemStringV2025R0 SharedExpirationTarget { get; set; }

        [JsonPropertyName("is_shared_links_expiration_notification_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsSharedLinksExpirationNotificationEnabled { get; set; }

        [JsonPropertyName("shared_links_expiration_notification_days")]
        public EnterpriseConfigurationItemIntegerV2025R0 SharedLinksExpirationNotificationDays { get; set; }

        [JsonPropertyName("is_shared_links_expiration_notification_prevented")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsSharedLinksExpirationNotificationPrevented { get; set; }

        [JsonPropertyName("is_auto_delete_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsAutoDeleteEnabled { get; set; }

        [JsonPropertyName("auto_delete_days")]
        public EnterpriseConfigurationItemIntegerV2025R0 AutoDeleteDays { get; set; }

        [JsonPropertyName("is_auto_delete_expiration_modification_prevented")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsAutoDeleteExpirationModificationPrevented { get; set; }

        [JsonPropertyName("auto_delete_target")]
        public EnterpriseConfigurationItemStringV2025R0 AutoDeleteTarget { get; set; }

        [JsonPropertyName("is_collaboration_expiration_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsCollaborationExpirationEnabled { get; set; }

        [JsonPropertyName("collaboration_expiration_days")]
        public EnterpriseConfigurationItemIntegerV2025R0 CollaborationExpirationDays { get; set; }

        [JsonPropertyName("is_collaboration_expiration_modification_prevented")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsCollaborationExpirationModificationPrevented { get; set; }

        [JsonPropertyName("is_collaboration_expiration_notification_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsCollaborationExpirationNotificationEnabled { get; set; }

        [JsonPropertyName("collaboration_expiration_target")]
        public EnterpriseConfigurationItemStringV2025R0 CollaborationExpirationTarget { get; set; }

        [JsonPropertyName("trash_auto_clear_time")]
        public EnterpriseConfigurationItemIntegerV2025R0 TrashAutoClearTime { get; set; }

        [JsonPropertyName("permanent_deletion_access")]
        public EnterpriseConfigurationItemStringV2025R0 PermanentDeletionAccess { get; set; }

        [JsonPropertyName("permanent_deletion_allowlist_users")]
        public EnterpriseConfigurationContentAndSharingV2025R0PermanentDeletionAllowlistUsersField PermanentDeletionAllowlistUsers { get; set; }

        public EnterpriseConfigurationContentAndSharingV2025R0() {
            
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}