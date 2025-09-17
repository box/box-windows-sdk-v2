using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CopyFolderRequestBody : ISerializable {
        /// <summary>
        /// An optional new name for the copied folder.
        /// 
        /// There are some restrictions to the file name. Names containing
        /// non-printable ASCII characters, forward and backward slashes
        /// (`/`, `\`), as well as names with trailing spaces are
        /// prohibited.
        /// 
        /// Additionally, the names `.` and `..` are
        /// not allowed either.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The destination folder to copy the folder to.
        /// </summary>
        [JsonPropertyName("parent")]
        public CopyFolderRequestBodyParentField Parent { get; }

        public CopyFolderRequestBody(CopyFolderRequestBodyParentField parent) {
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