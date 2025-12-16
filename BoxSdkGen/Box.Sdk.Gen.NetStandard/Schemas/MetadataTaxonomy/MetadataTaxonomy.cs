using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataTaxonomy : ISerializable {
        /// <summary>
        /// A unique identifier of the metadata taxonomy.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// A unique identifier of the metadata taxonomy. The identifier must be unique within 
        /// the namespace to which it belongs.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <summary>
        /// The display name of the metadata taxonomy. This can be seen in the Box web app.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// A namespace that the metadata taxonomy is associated with.
        /// </summary>
        [JsonPropertyName("namespace")]
        public string NamespaceParam { get; set; }

        /// <summary>
        /// Levels of the metadata taxonomy.
        /// </summary>
        [JsonPropertyName("levels")]
        public IReadOnlyList<MetadataTaxonomyLevel> Levels { get; set; }

        public MetadataTaxonomy(string id, string displayName, string namespaceParam) {
            Id = id;
            DisplayName = displayName;
            NamespaceParam = namespaceParam;
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