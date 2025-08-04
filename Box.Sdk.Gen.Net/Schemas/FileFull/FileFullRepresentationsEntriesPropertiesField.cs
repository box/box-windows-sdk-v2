using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileFullRepresentationsEntriesPropertiesField : ISerializable {
        /// <summary>
        /// The width by height size of this representation in pixels.
        /// </summary>
        [JsonPropertyName("dimensions")]
        public string? Dimensions { get; init; }

        /// <summary>
        /// Indicates if the representation is build up out of multiple
        /// pages.
        /// </summary>
        [JsonPropertyName("paged")]
        public string? Paged { get; init; }

        /// <summary>
        /// Indicates if the representation can be used as a thumbnail of
        /// the file.
        /// </summary>
        [JsonPropertyName("thumb")]
        public string? Thumb { get; init; }

        public FileFullRepresentationsEntriesPropertiesField() {
            
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