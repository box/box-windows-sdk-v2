using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class CollaborationAcceptanceRequirementsStatusField : ISerializable {
        [JsonPropertyName("terms_of_service_requirement")]
        public CollaborationAcceptanceRequirementsStatusTermsOfServiceRequirementField? TermsOfServiceRequirement { get; init; }

        [JsonPropertyName("strong_password_requirement")]
        public CollaborationAcceptanceRequirementsStatusStrongPasswordRequirementField? StrongPasswordRequirement { get; init; }

        [JsonPropertyName("two_factor_authentication_requirement")]
        public CollaborationAcceptanceRequirementsStatusTwoFactorAuthenticationRequirementField? TwoFactorAuthenticationRequirement { get; init; }

        public CollaborationAcceptanceRequirementsStatusField() {
            
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