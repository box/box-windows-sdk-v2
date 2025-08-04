using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class OAuth2Error : ISerializable {
        /// <summary>
        /// The type of the error returned.
        /// </summary>
        [JsonPropertyName("error")]
        public string? Error { get; init; }

        /// <summary>
        /// The type of the error returned.
        /// </summary>
        [JsonPropertyName("error_description")]
        public string? ErrorDescription { get; init; }

        public OAuth2Error() {
            
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