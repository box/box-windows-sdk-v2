using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenBatchCreateRequestV2025R0DestinationFolderField : ISerializable {
        /// <summary>
        /// The value will always be `folder`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<DocGenBatchCreateRequestV2025R0DestinationFolderTypeField>))]
        public StringEnum<DocGenBatchCreateRequestV2025R0DestinationFolderTypeField> Type { get; }

        /// <summary>
        /// ID of the folder.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public DocGenBatchCreateRequestV2025R0DestinationFolderField(string id, DocGenBatchCreateRequestV2025R0DestinationFolderTypeField type = DocGenBatchCreateRequestV2025R0DestinationFolderTypeField.Folder) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal DocGenBatchCreateRequestV2025R0DestinationFolderField(string id, StringEnum<DocGenBatchCreateRequestV2025R0DestinationFolderTypeField> type) {
            Type = DocGenBatchCreateRequestV2025R0DestinationFolderTypeField.Folder;
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