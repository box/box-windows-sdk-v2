using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class FolderReferenceV2026R0 : ISerializable {
        /// <summary>
        /// The value will always be `folder`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<FolderReferenceV2026R0TypeField>))]
        public StringEnum<FolderReferenceV2026R0TypeField> Type { get; }

        /// <summary>
        /// ID of the folder.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public FolderReferenceV2026R0(string id, FolderReferenceV2026R0TypeField type = FolderReferenceV2026R0TypeField.Folder) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal FolderReferenceV2026R0(string id, StringEnum<FolderReferenceV2026R0TypeField> type) {
            Type = FolderReferenceV2026R0TypeField.Folder;
            Id = id;
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