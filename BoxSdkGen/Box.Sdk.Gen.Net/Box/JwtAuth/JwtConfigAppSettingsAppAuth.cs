using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class JwtConfigAppSettingsAppAuth : ISerializable {
        /// <summary>
        /// Public key ID
        /// </summary>
        [JsonPropertyName("publicKeyID")]
        public string PublicKeyId { get; }

        /// <summary>
        /// Private key
        /// </summary>
        [JsonPropertyName("privateKey")]
        public string PrivateKey { get; }

        /// <summary>
        /// Passphrase
        /// </summary>
        [JsonPropertyName("passphrase")]
        public string Passphrase { get; }

        public JwtConfigAppSettingsAppAuth(string publicKeyId, string privateKey, string passphrase) {
            PublicKeyId = publicKeyId;
            PrivateKey = privateKey;
            Passphrase = passphrase;
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