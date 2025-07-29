using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileOrFolderScope : ISerializable {
        /// <summary>
        /// The scopes for the resource access.
        /// </summary>
        [JsonPropertyName("scope")]
        [JsonConverter(typeof(StringEnumConverter<FileOrFolderScopeScopeField>))]
        public StringEnum<FileOrFolderScopeScopeField>? Scope { get; init; }

        [JsonPropertyName("object")]
        public FileMiniOrFolderMini? Object { get; init; }

        public FileOrFolderScope() {
            
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