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
        public EnterpriseConfigurationItemBooleanV2025R0 IsManagedUserSignupEnabled { get; set; }

        [JsonPropertyName("is_managed_user_signup_notification_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsManagedUserSignupNotificationEnabled { get; set; }

        [JsonPropertyName("is_managed_user_signup_corporate_email_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsManagedUserSignupCorporateEmailEnabled { get; set; }

        [JsonPropertyName("is_new_user_notification_daily_digest_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsNewUserNotificationDailyDigestEnabled { get; set; }

        [JsonPropertyName("is_managed_user_email_change_disabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsManagedUserEmailChangeDisabled { get; set; }

        [JsonPropertyName("is_multi_factor_auth_required")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsMultiFactorAuthRequired { get; set; }

        [JsonPropertyName("is_weak_password_prevention_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsWeakPasswordPreventionEnabled { get; set; }

        [JsonPropertyName("is_password_leak_detection_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsPasswordLeakDetectionEnabled { get; set; }

        [JsonPropertyName("last_password_reset_at")]
        public EnterpriseConfigurationSecurityV2025R0LastPasswordResetAtField LastPasswordResetAt { get; set; }

        [JsonPropertyName("is_password_request_notification_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsPasswordRequestNotificationEnabled { get; set; }

        [JsonPropertyName("is_password_change_notification_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsPasswordChangeNotificationEnabled { get; set; }

        [JsonPropertyName("is_strong_password_for_ext_collab_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsStrongPasswordForExtCollabEnabled { get; set; }

        [JsonPropertyName("is_managed_user_migration_disabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsManagedUserMigrationDisabled { get; set; }

        [JsonPropertyName("join_link")]
        public EnterpriseConfigurationItemStringV2025R0 JoinLink { get; set; }

        [JsonPropertyName("join_url")]
        public EnterpriseConfigurationItemStringV2025R0 JoinUrl { get; set; }

        [JsonPropertyName("failed_login_attempts_to_trigger_admin_notification")]
        public EnterpriseConfigurationItemIntegerV2025R0 FailedLoginAttemptsToTriggerAdminNotification { get; set; }

        [JsonPropertyName("password_min_length")]
        public EnterpriseConfigurationItemIntegerV2025R0 PasswordMinLength { get; set; }

        [JsonPropertyName("password_min_uppercase_characters")]
        public EnterpriseConfigurationItemIntegerV2025R0 PasswordMinUppercaseCharacters { get; set; }

        [JsonPropertyName("password_min_numeric_characters")]
        public EnterpriseConfigurationItemIntegerV2025R0 PasswordMinNumericCharacters { get; set; }

        [JsonPropertyName("password_min_special_characters")]
        public EnterpriseConfigurationItemIntegerV2025R0 PasswordMinSpecialCharacters { get; set; }

        [JsonPropertyName("password_reset_frequency")]
        public EnterpriseConfigurationItemStringV2025R0 PasswordResetFrequency { get; set; }

        [JsonPropertyName("previous_password_reuse_limit")]
        public EnterpriseConfigurationItemStringV2025R0 PreviousPasswordReuseLimit { get; set; }

        [JsonPropertyName("session_duration")]
        public EnterpriseConfigurationItemStringV2025R0 SessionDuration { get; set; }

        [JsonPropertyName("external_collab_multi_factor_auth_settings")]
        public EnterpriseConfigurationSecurityV2025R0ExternalCollabMultiFactorAuthSettingsField ExternalCollabMultiFactorAuthSettings { get; set; }

        [JsonPropertyName("keysafe")]
        public EnterpriseConfigurationSecurityV2025R0KeysafeField Keysafe { get; set; }

        [JsonPropertyName("is_custom_session_duration_enabled")]
        public EnterpriseConfigurationItemBooleanV2025R0 IsCustomSessionDurationEnabled { get; set; }

        [JsonPropertyName("custom_session_duration_value")]
        public EnterpriseConfigurationItemStringV2025R0 CustomSessionDurationValue { get; set; }

        [JsonPropertyName("custom_session_duration_groups")]
        public EnterpriseConfigurationSecurityV2025R0CustomSessionDurationGroupsField CustomSessionDurationGroups { get; set; }

        [JsonPropertyName("multi_factor_auth_type")]
        public EnterpriseConfigurationItemStringV2025R0 MultiFactorAuthType { get; set; }

        [JsonPropertyName("enforced_mfa_frequency")]
        public EnterpriseConfigurationSecurityV2025R0EnforcedMfaFrequencyField EnforcedMfaFrequency { get; set; }

        public EnterpriseConfigurationSecurityV2025R0() {
            
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