using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataTaxonomyNode : ISerializable {
        /// <summary>
        /// A unique identifier of the metadata taxonomy node.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The display name of the metadata taxonomy node.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// An index of the level to which the node belongs.
        /// </summary>
        [JsonPropertyName("level")]
        public long Level { get; set; }

        /// <summary>
        /// The identifier of the parent node.
        /// </summary>
        [JsonPropertyName("parentId")]
        public string ParentId { get; set; }

        /// <summary>
        /// An array of identifiers for all ancestor nodes.  
        /// Not returned for root-level nodes.
        /// </summary>
        [JsonPropertyName("nodePath")]
        public IReadOnlyList<string> NodePath { get; set; }

        /// <summary>
        /// An array of objects for all ancestor nodes.  
        /// Not returned for root-level nodes.
        /// </summary>
        [JsonPropertyName("ancestors")]
        public IReadOnlyList<MetadataTaxonomyAncestor> Ancestors { get; set; }

        public MetadataTaxonomyNode(string id, string displayName, long level) {
            Id = id;
            DisplayName = displayName;
            Level = level;
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