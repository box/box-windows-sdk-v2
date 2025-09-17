using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UploadFileRequestBodyAttributesField : ISerializable {
        /// <summary>
        /// The name of the file.
        /// 
        /// File names must be unique within their parent folder. The name check is case-insensitive, so a file
        /// named `New File` cannot be created in a parent folder that already contains a folder named `new file`.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// The parent folder to upload the file to.
        /// </summary>
        [JsonPropertyName("parent")]
        public UploadFileRequestBodyAttributesParentField Parent { get; }

        /// <summary>
        /// Defines the time the file was originally created at.
        /// 
        /// If not set, the upload time will be used.
        /// </summary>
        [JsonPropertyName("content_created_at")]
        public System.DateTimeOffset? ContentCreatedAt { get; init; }

        /// <summary>
        /// Defines the time the file was last modified at.
        /// 
        /// If not set, the upload time will be used.
        /// </summary>
        [JsonPropertyName("content_modified_at")]
        public System.DateTimeOffset? ContentModifiedAt { get; init; }

        public UploadFileRequestBodyAttributesField(string name, UploadFileRequestBodyAttributesParentField parent) {
            Name = name;
            Parent = parent;
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