using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SearchResultWithSharedLink : ISerializable {
        /// <summary>
        /// The optional shared link through which the user has access to this
        /// item. This value is only returned for items for which the user has
        /// recently accessed the file through a shared link. For all other
        /// items this value will return `null`.
        /// </summary>
        [JsonPropertyName("accessible_via_shared_link")]
        public string? AccessibleViaSharedLink { get; init; }

        [JsonPropertyName("item")]
        public FileFullOrFolderFullOrWebLink? Item { get; init; }

        /// <summary>
        /// The result type. The value is always `search_result`.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        public SearchResultWithSharedLink() {
            
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