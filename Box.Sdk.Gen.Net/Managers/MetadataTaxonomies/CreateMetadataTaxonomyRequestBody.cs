using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateMetadataTaxonomyRequestBody : ISerializable {
        /// <summary>
        /// The taxonomy key. If it is not provided in the request body, it will be 
        /// generated from the `displayName`. The `displayName` would be converted 
        /// to lower case, and all spaces and non-alphanumeric characters replaced 
        /// with underscores.
        /// </summary>
        [JsonPropertyName("key")]
        public string? Key { get; init; }

        /// <summary>
        /// The display name of the taxonomy.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; }

        /// <summary>
        /// The namespace of the metadata taxonomy to create.
        /// </summary>
        [JsonPropertyName("namespace")]
        public string NamespaceParam { get; }

        public CreateMetadataTaxonomyRequestBody(string displayName, string namespaceParam) {
            DisplayName = displayName;
            NamespaceParam = namespaceParam;
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