using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileMini : FileBase, ISerializable {
        [JsonPropertyName("sequence_id")]
        public string SequenceId { get; set; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The SHA1 hash of the file. This can be used to compare the contents
        /// of a file on Box with a local file.
        /// </summary>
        [JsonPropertyName("sha1")]
        public string Sha1 { get; set; }

        [JsonPropertyName("file_version")]
        public FileVersionMini FileVersion { get; set; }

        public FileMini(string id, FileBaseTypeField type = FileBaseTypeField.File) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal FileMini(string id, StringEnum<FileBaseTypeField> type) : base(id, type ?? new StringEnum<FileBaseTypeField>(FileBaseTypeField.File)) {
            
        }
        internal new string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}