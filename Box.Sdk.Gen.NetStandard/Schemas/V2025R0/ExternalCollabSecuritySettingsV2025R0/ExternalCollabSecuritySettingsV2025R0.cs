using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ExternalCollabSecuritySettingsV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isstateSet")]
        protected bool _isStateSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isscheduled_statusSet")]
        protected bool _isScheduledStatusSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isscheduled_atSet")]
        protected bool _isScheduledAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isfactor_type_settingsSet")]
        protected bool _isFactorTypeSettingsSet { get; set; }

        protected string _state { get; set; }

        protected string _scheduledStatus { get; set; }

        protected System.DateTimeOffset? _scheduledAt { get; set; }

        protected string _factorTypeSettings { get; set; }

        /// <summary>
        /// List of domains that are not allowed for external collaboration. Applies if state is `denylist`.
        /// </summary>
        [JsonPropertyName("denylist_domains")]
        public IReadOnlyList<string> DenylistDomains { get; set; }

        /// <summary>
        /// List of email addresses that are not allowed for external collaboration. Applies if state is `denylist`.
        /// </summary>
        [JsonPropertyName("denylist_emails")]
        public IReadOnlyList<string> DenylistEmails { get; set; }

        /// <summary>
        /// List of domains that are allowed for external collaboration. Applies if state is `allowlist`.
        /// </summary>
        [JsonPropertyName("allowlist_domains")]
        public IReadOnlyList<string> AllowlistDomains { get; set; }

        /// <summary>
        /// List of email addresses that are allowed for external collaboration. Applies if state is `allowlist`.
        /// </summary>
        [JsonPropertyName("allowlist_emails")]
        public IReadOnlyList<string> AllowlistEmails { get; set; }

        /// <summary>
        /// The state of the external collaboration security settings. Possible values include `enabled`, `disabled`, `allowlist`, and `denylist`.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get => _state; set { _state = value; _isStateSet = true; } }

        /// <summary>
        /// The status of the scheduling to apply external collaboration security settings. Possible values include `in_progress`, `scheduled`, `completed`, `failed`, and `scheduled_immediate`.
        /// </summary>
        [JsonPropertyName("scheduled_status")]
        public string ScheduledStatus { get => _scheduledStatus; set { _scheduledStatus = value; _isScheduledStatusSet = true; } }

        /// <summary>
        /// Scheduled at.
        /// </summary>
        [JsonPropertyName("scheduled_at")]
        public System.DateTimeOffset? ScheduledAt { get => _scheduledAt; set { _scheduledAt = value; _isScheduledAtSet = true; } }

        /// <summary>
        /// Factor type for the external collaborators authentication. Possible values include `totp`, `any`, or `unknown`.
        /// </summary>
        [JsonPropertyName("factor_type_settings")]
        public string FactorTypeSettings { get => _factorTypeSettings; set { _factorTypeSettings = value; _isFactorTypeSettingsSet = true; } }

        public ExternalCollabSecuritySettingsV2025R0() {
            
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