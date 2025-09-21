using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateWebLinkRequestBody : ISerializable {
        /// <summary>
        /// The URL that this web link links to. Must start with
        /// `"http://"` or `"https://"`.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; }

        /// <summary>
        /// The parent folder to create the web link within.
        /// </summary>
        [JsonPropertyName("parent")]
        public CreateWebLinkRequestBodyParentField Parent { get; }

        /// <summary>
        /// Name of the web link. Defaults to the URL if not set.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// Description of the web link.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        public CreateWebLinkRequestBody(string url, CreateWebLinkRequestBodyParentField parent) {
            Url = url;
            Parent = parent;
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