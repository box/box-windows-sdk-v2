using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileFullRepresentationsEntriesField : ISerializable {
        /// <summary>
        /// An object containing the URL that can be used to actually fetch
        /// the representation.
        /// </summary>
        [JsonPropertyName("content")]
        public FileFullRepresentationsEntriesContentField? Content { get; init; }

        /// <summary>
        /// An object containing the URL that can be used to fetch more info
        /// on this representation.
        /// </summary>
        [JsonPropertyName("info")]
        public FileFullRepresentationsEntriesInfoField? Info { get; init; }

        /// <summary>
        /// An object containing the size and type of this presentation.
        /// </summary>
        [JsonPropertyName("properties")]
        public FileFullRepresentationsEntriesPropertiesField? Properties { get; init; }

        /// <summary>
        /// Indicates the file type of the returned representation.
        /// </summary>
        [JsonPropertyName("representation")]
        public string? Representation { get; init; }

        /// <summary>
        /// An object containing the status of this representation.
        /// </summary>
        [JsonPropertyName("status")]
        public FileFullRepresentationsEntriesStatusField? Status { get; init; }

        public FileFullRepresentationsEntriesField() {
            
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