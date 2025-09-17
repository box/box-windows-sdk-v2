using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubCollaborationV2025R0AcceptanceRequirementsStatusTwoFactorAuthenticationRequirementField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isuser_has_two_factor_authentication_enabledSet")]
        protected bool _isUserHasTwoFactorAuthenticationEnabledSet { get; set; }

        protected bool? _userHasTwoFactorAuthenticationEnabled { get; set; }

        /// <summary>
        /// Whether or not the enterprise that owns the content requires
        /// two-factor authentication to be enabled in order to
        /// collaborate on the content.
        /// </summary>
        [JsonPropertyName("enterprise_has_two_factor_auth_enabled")]
        public bool? EnterpriseHasTwoFactorAuthEnabled { get; init; }

        /// <summary>
        /// Whether or not the user has two-factor authentication
        /// enabled. The field is `null` when two-factor
        /// authentication is not required.
        /// </summary>
        [JsonPropertyName("user_has_two_factor_authentication_enabled")]
        public bool? UserHasTwoFactorAuthenticationEnabled { get => _userHasTwoFactorAuthenticationEnabled; init { _userHasTwoFactorAuthenticationEnabled = value; _isUserHasTwoFactorAuthenticationEnabledSet = true; } }

        public HubCollaborationV2025R0AcceptanceRequirementsStatusTwoFactorAuthenticationRequirementField() {
            
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