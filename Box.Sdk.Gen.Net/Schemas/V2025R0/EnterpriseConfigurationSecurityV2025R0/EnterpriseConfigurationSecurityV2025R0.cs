using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class EnterpriseConfigurationSecurityV2025R0 : ISerializable {
        [JsonPropertyName("is_managed_user_signup_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsManagedUserSignupEnabled { get; init; }

        [JsonPropertyName("is_managed_user_signup_notification_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsManagedUserSignupNotificationEnabled { get; init; }

        [JsonPropertyName("is_managed_user_signup_corporate_email_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsManagedUserSignupCorporateEmailEnabled { get; init; }

        [JsonPropertyName("is_new_user_notification_daily_digest_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsNewUserNotificationDailyDigestEnabled { get; init; }

        [JsonPropertyName("is_managed_user_email_change_disabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsManagedUserEmailChangeDisabled { get; init; }

        [JsonPropertyName("is_multi_factor_auth_required")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsMultiFactorAuthRequired { get; init; }

        [JsonPropertyName("is_weak_password_prevention_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsWeakPasswordPreventionEnabled { get; init; }

        [JsonPropertyName("is_password_leak_detection_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsPasswordLeakDetectionEnabled { get; init; }

        [JsonPropertyName("last_password_reset_at")]
        public EnterpriseConfigurationSecurityV2025R0LastPasswordResetAtField? LastPasswordResetAt { get; init; }

        [JsonPropertyName("is_password_request_notification_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsPasswordRequestNotificationEnabled { get; init; }

        [JsonPropertyName("is_password_change_notification_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsPasswordChangeNotificationEnabled { get; init; }

        [JsonPropertyName("is_strong_password_for_ext_collab_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsStrongPasswordForExtCollabEnabled { get; init; }

        [JsonPropertyName("is_managed_user_migration_disabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsManagedUserMigrationDisabled { get; init; }

        [JsonPropertyName("join_link")]
        public EnterpriseConfigurationItemStringV2025R0? JoinLink { get; init; }

        [JsonPropertyName("join_url")]
        public EnterpriseConfigurationItemStringV2025R0? JoinUrl { get; init; }

        [JsonPropertyName("failed_login_attempts_to_trigger_admin_notification")]
        public EnterpriseConfigurationItemIntegerV2025R0? FailedLoginAttemptsToTriggerAdminNotification { get; init; }

        [JsonPropertyName("password_min_length")]
        public EnterpriseConfigurationItemIntegerV2025R0? PasswordMinLength { get; init; }

        [JsonPropertyName("password_min_uppercase_characters")]
        public EnterpriseConfigurationItemIntegerV2025R0? PasswordMinUppercaseCharacters { get; init; }

        [JsonPropertyName("password_min_numeric_characters")]
        public EnterpriseConfigurationItemIntegerV2025R0? PasswordMinNumericCharacters { get; init; }

        [JsonPropertyName("password_min_special_characters")]
        public EnterpriseConfigurationItemIntegerV2025R0? PasswordMinSpecialCharacters { get; init; }

        [JsonPropertyName("password_reset_frequency")]
        public EnterpriseConfigurationItemStringV2025R0? PasswordResetFrequency { get; init; }

        [JsonPropertyName("previous_password_reuse_limit")]
        public EnterpriseConfigurationItemStringV2025R0? PreviousPasswordReuseLimit { get; init; }

        [JsonPropertyName("session_duration")]
        public EnterpriseConfigurationItemStringV2025R0? SessionDuration { get; init; }

        [JsonPropertyName("external_collab_multi_factor_auth_settings")]
        public EnterpriseConfigurationSecurityV2025R0ExternalCollabMultiFactorAuthSettingsField? ExternalCollabMultiFactorAuthSettings { get; init; }

        [JsonPropertyName("keysafe")]
        public EnterpriseConfigurationSecurityV2025R0KeysafeField? Keysafe { get; init; }

        [JsonPropertyName("is_custom_session_duration_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0? IsCustomSessionDurationEnabled { get; init; }

        [JsonPropertyName("custom_session_duration_value")]
        public EnterpriseConfigurationItemStringV2025R0? CustomSessionDurationValue { get; init; }

        [JsonPropertyName("custom_session_duration_groups")]
        public EnterpriseConfigurationSecurityV2025R0CustomSessionDurationGroupsField? CustomSessionDurationGroups { get; init; }

        [JsonPropertyName("multi_factor_auth_type")]
        public EnterpriseConfigurationItemStringV2025R0? MultiFactorAuthType { get; init; }

        [JsonPropertyName("enforced_mfa_frequency")]
        public EnterpriseConfigurationSecurityV2025R0EnforcedMfaFrequencyField? EnforcedMfaFrequency { get; init; }

        public EnterpriseConfigurationSecurityV2025R0() {
            
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