using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateFolderMetadataByIdRequestBody : ISerializable {
        /// <summary>
        /// The type of change to perform on the template. Some
        /// of these are hazardous as they will change existing templates.
        /// </summary>
        [JsonPropertyName("op")]
        [JsonConverter(typeof(StringEnumConverter<UpdateFolderMetadataByIdRequestBodyOpField>))]
        public StringEnum<UpdateFolderMetadataByIdRequestBodyOpField>? Op { get; init; }

        /// <summary>
        /// The location in the metadata JSON object
        /// to apply the changes to, in the format of a
        /// [JSON-Pointer](https://tools.ietf.org/html/rfc6901).
        /// 
        /// The path must always be prefixed with a `/` to represent the root
        /// of the template. The characters `~` and `/` are reserved
        /// characters and must be escaped in the key.
        /// </summary>
        [JsonPropertyName("path")]
        public string? Path { get; init; }

        [JsonPropertyName("value")]
        public MetadataInstanceValue? Value { get; init; }

        /// <summary>
        /// The location in the metadata JSON object to move or copy a value
        /// from. Required for `move` or `copy` operations and must be in the
        /// format of a [JSON-Pointer](https://tools.ietf.org/html/rfc6901).
        /// </summary>
        [JsonPropertyName("from")]
        public string? From { get; init; }

        public UpdateFolderMetadataByIdRequestBody() {
            
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