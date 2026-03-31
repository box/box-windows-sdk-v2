using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class HubDocumentPageV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isparent_idSet")]
        protected bool _isParentIdSet { get; set; }

        protected string _parentId { get; set; }

        /// <summary>
        /// The unique identifier for this page.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of this resource. The value is always `page`.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// The unique identifier of the parent page. Null for root-level pages.
        /// </summary>
        [JsonPropertyName("parent_id")]
        public string ParentId { get => _parentId; set { _parentId = value; _isParentIdSet = true; } }

        /// <summary>
        /// The title text of the page. Includes rich text formatting.
        /// </summary>
        [JsonPropertyName("title_fragment")]
        public string TitleFragment { get; set; }

        public HubDocumentPageV2025R0(string id, string type, string titleFragment) {
            Id = id;
            Type = type;
            TitleFragment = titleFragment;
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