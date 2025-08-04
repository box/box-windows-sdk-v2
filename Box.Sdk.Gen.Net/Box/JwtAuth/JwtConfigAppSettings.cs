using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class JwtConfigAppSettings : ISerializable {
        /// <summary>
        /// App client ID
        /// </summary>
        [JsonPropertyName("clientID")]
        public string ClientId { get; }

        /// <summary>
        /// App client secret
        /// </summary>
        [JsonPropertyName("clientSecret")]
        public string ClientSecret { get; }

        /// <summary>
        /// App auth settings
        /// </summary>
        [JsonPropertyName("appAuth")]
        public JwtConfigAppSettingsAppAuth AppAuth { get; }

        public JwtConfigAppSettings(string clientId, string clientSecret, JwtConfigAppSettingsAppAuth appAuth) {
            ClientId = clientId;
            ClientSecret = clientSecret;
            AppAuth = appAuth;
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