using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FolderMini : FolderBase, ISerializable {
        [JsonPropertyName("sequence_id")]
        public string? SequenceId { get; init; }

        /// <summary>
        /// The name of the folder.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        public FolderMini(string id, FolderBaseTypeField type = FolderBaseTypeField.Folder) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal FolderMini(string id, StringEnum<FolderBaseTypeField> type) : base(id, type ?? new StringEnum<FolderBaseTypeField>(FolderBaseTypeField.Folder)) {
            
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