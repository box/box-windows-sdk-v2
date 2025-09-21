using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class PostOAuth2Revoke : ISerializable {
        /// <summary>
        /// The Client ID of the application requesting to revoke the
        /// access token.
        /// </summary>
        [JsonPropertyName("client_id")]
        public string? ClientId { get; init; }

        /// <summary>
        /// The client secret of the application requesting to revoke
        /// an access token.
        /// </summary>
        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; init; }

        /// <summary>
        /// The access token to revoke.
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token { get; init; }

        public PostOAuth2Revoke() {
            
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