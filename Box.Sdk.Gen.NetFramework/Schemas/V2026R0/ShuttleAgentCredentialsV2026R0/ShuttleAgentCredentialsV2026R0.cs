using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleAgentCredentialsV2026R0 : ISerializable {
        /// <summary>
        /// The installation token to paste into the Windows agent installer to
        /// register the agent.
        /// </summary>
        [JsonPropertyName("registration_token")]
        public string RegistrationToken { get; set; }

        /// <summary>
        /// When the registration token expires and can no longer be used.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public System.DateTimeOffset ExpiresAt { get; set; }

        public ShuttleAgentCredentialsV2026R0(string registrationToken, System.DateTimeOffset expiresAt) {
            RegistrationToken = registrationToken;
            ExpiresAt = expiresAt;
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