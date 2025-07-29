using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public class BaseUrls : ISerializable {
        [JsonPropertyName("base_url")]
        public string BaseUrl { get; }

        [JsonPropertyName("upload_url")]
        public string UploadUrl { get; }

        [JsonPropertyName("oauth2_url")]
        public string Oauth2Url { get; }

        public BaseUrls(string baseUrl = "https://api.box.com", string uploadUrl = "https://upload.box.com/api", string oauth2Url = "https://account.box.com/api/oauth2") {
            BaseUrl = baseUrl;
            UploadUrl = uploadUrl;
            Oauth2Url = oauth2Url;
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