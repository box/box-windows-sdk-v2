using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileFullRepresentationsEntriesStatusField : ISerializable {
        /// <summary>
        /// The status of the representation.
        /// 
        /// * `success` defines the representation as ready to be viewed.
        /// * `viewable` defines a video to be ready for viewing.
        /// * `pending` defines the representation as to be generated. Retry
        ///   this endpoint to re-check the status.
        /// * `none` defines that the representation will be created when
        ///   requested. Request the URL defined in the `info` object to
        ///   trigger this generation.
        /// </summary>
        [JsonPropertyName("state")]
        [JsonConverter(typeof(StringEnumConverter<FileFullRepresentationsEntriesStatusStateField>))]
        public StringEnum<FileFullRepresentationsEntriesStatusStateField>? State { get; init; }

        public FileFullRepresentationsEntriesStatusField() {
            
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