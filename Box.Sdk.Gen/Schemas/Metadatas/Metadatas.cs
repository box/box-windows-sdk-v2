using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class Metadatas : ISerializable {
        /// <summary>
        /// A list of metadata instances, as applied to this file or folder.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<Metadata>? Entries { get; init; }

        /// <summary>
        /// The limit that was used for this page of results.
        /// </summary>
        [JsonPropertyName("limit")]
        public long? Limit { get; init; }

        public Metadatas() {
            
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