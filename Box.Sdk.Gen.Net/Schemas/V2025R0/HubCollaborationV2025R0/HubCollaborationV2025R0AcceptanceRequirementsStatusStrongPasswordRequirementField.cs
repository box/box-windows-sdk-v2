using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubCollaborationV2025R0AcceptanceRequirementsStatusStrongPasswordRequirementField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isuser_has_strong_passwordSet")]
        protected bool _isUserHasStrongPasswordSet { get; set; }

        protected bool? _userHasStrongPassword { get; set; }

        /// <summary>
        /// Whether or not the enterprise that owns the content requires
        /// a strong password to collaborate on the content, or enforces
        /// an exposed password detection for the external collaborators.
        /// </summary>
        [JsonPropertyName("enterprise_has_strong_password_required_for_external_users")]
        public bool? EnterpriseHasStrongPasswordRequiredForExternalUsers { get; init; }

        /// <summary>
        /// Whether or not the user has a strong and not exposed password set
        /// for their account. The field is `null` when a strong password is
        /// not required.
        /// </summary>
        [JsonPropertyName("user_has_strong_password")]
        public bool? UserHasStrongPassword { get => _userHasStrongPassword; init { _userHasStrongPassword = value; _isUserHasStrongPasswordSet = true; } }

        public HubCollaborationV2025R0AcceptanceRequirementsStatusStrongPasswordRequirementField() {
            
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