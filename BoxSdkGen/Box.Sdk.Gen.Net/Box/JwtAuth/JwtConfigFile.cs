using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class JwtConfigFile : ISerializable {
        /// <summary>
        /// Enterprise ID
        /// </summary>
        [JsonPropertyName("enterpriseID")]
        public string? EnterpriseId { get; init; }

        /// <summary>
        /// User ID
        /// </summary>
        [JsonPropertyName("userID")]
        public string? UserId { get; init; }

        /// <summary>
        /// App settings
        /// </summary>
        [JsonPropertyName("boxAppSettings")]
        public JwtConfigAppSettings BoxAppSettings { get; }

        public JwtConfigFile(JwtConfigAppSettings boxAppSettings) {
            BoxAppSettings = boxAppSettings;
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