using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class HubDocumentBlockV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isparent_idSet")]
        protected bool _isParentIdSet { get; set; }

        protected string _parentId { get; set; }

        /// <summary>
        /// The unique identifier for this block.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The unique identifier of the parent block. Null for direct children of the page.
        /// </summary>
        [JsonPropertyName("parent_id")]
        public string ParentId { get => _parentId; set { _parentId = value; _isParentIdSet = true; } }

        public HubDocumentBlockV2025R0(string id) {
            Id = id;
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