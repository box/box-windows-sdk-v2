using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileVersionMini : FileVersionBase, ISerializable {
        /// <summary>
        /// The SHA1 hash of this version of the file.
        /// </summary>
        [JsonPropertyName("sha1")]
        public string? Sha1 { get; init; }

        public FileVersionMini(string id, FileVersionBaseTypeField type = FileVersionBaseTypeField.FileVersion) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal FileVersionMini(string id, StringEnum<FileVersionBaseTypeField> type) : base(id, type ?? new StringEnum<FileVersionBaseTypeField>(FileVersionBaseTypeField.FileVersion)) {
            
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}