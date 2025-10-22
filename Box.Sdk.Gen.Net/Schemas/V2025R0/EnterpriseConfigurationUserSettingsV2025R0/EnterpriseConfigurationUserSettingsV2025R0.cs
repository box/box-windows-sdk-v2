using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationUserSettingsV2025R0 : ISerializable {
        [JsonPropertyName("enterprise_feature_settings")]
        public IReadOnlyList<EnterpriseFeatureSettingsItemV2025R0>? EnterpriseFeatureSettings { get; init; }

        [JsonPropertyName("user_invites_expiration_time_frame")]
        public EnterpriseConfigurationItemStringV2025R0? UserInvitesExpirationTimeFrame { get; init; }

        [JsonPropertyName("is_username_change_restricted")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsUsernameChangeRestricted { get; init; }

        [JsonPropertyName("is_box_sync_restricted_for_new_users")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsBoxSyncRestrictedForNewUsers { get; init; }

        [JsonPropertyName("is_view_all_users_enabled_for_new_users")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsViewAllUsersEnabledForNewUsers { get; init; }

        [JsonPropertyName("is_device_limit_exemption_enabled_for_new_users")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsDeviceLimitExemptionEnabledForNewUsers { get; init; }

        [JsonPropertyName("is_external_collaboration_restricted_for_new_users")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsExternalCollaborationRestrictedForNewUsers { get; init; }

        [JsonPropertyName("is_unlimited_storage_enabled_for_new_users")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsUnlimitedStorageEnabledForNewUsers { get; init; }

        [JsonPropertyName("new_user_default_storage_limit")]
        public EnterpriseConfigurationItemIntegerV2025R0? NewUserDefaultStorageLimit { get; init; }

        [JsonPropertyName("new_user_default_timezone")]
        public EnterpriseConfigurationItemStringV2025R0? NewUserDefaultTimezone { get; init; }

        [JsonPropertyName("new_user_default_language")]
        public EnterpriseConfigurationItemStringV2025R0? NewUserDefaultLanguage { get; init; }

        [JsonPropertyName("is_enterprise_sso_required")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsEnterpriseSsoRequired { get; init; }

        [JsonPropertyName("is_enterprise_sso_in_testing")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsEnterpriseSsoInTesting { get; init; }

        [JsonPropertyName("is_sso_auto_add_groups_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsSsoAutoAddGroupsEnabled { get; init; }

        [JsonPropertyName("is_sso_auto_add_user_to_groups_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsSsoAutoAddUserToGroupsEnabled { get; init; }

        [JsonPropertyName("is_sso_auto_remove_user_from_groups_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsSsoAutoRemoveUserFromGroupsEnabled { get; init; }

        [JsonPropertyName("user_tracking_codes")]
        public EnterpriseConfigurationUserSettingsV2025R0UserTrackingCodesField? UserTrackingCodes { get; init; }

        [JsonPropertyName("number_of_user_tracking_codes_remaining")]
        public EnterpriseConfigurationItemIntegerV2025R0? NumberOfUserTrackingCodesRemaining { get; init; }

        [JsonPropertyName("is_instant_login_restricted")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsInstantLoginRestricted { get; init; }

        public EnterpriseConfigurationUserSettingsV2025R0() {
            
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