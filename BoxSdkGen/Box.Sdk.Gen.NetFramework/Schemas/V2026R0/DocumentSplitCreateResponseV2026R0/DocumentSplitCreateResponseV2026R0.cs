using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitCreateResponseV2026R0 : ISerializable {
        /// <summary>
        /// The value will always be `document_split`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitCreateResponseV2026R0TypeField>))]
        public StringEnum<DocumentSplitCreateResponseV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The unique identifier of the created Document Split job.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        public DocumentSplitCreateResponseV2026R0(string id, DocumentSplitCreateResponseV2026R0TypeField type = DocumentSplitCreateResponseV2026R0TypeField.DocumentSplit) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal DocumentSplitCreateResponseV2026R0(string id, StringEnum<DocumentSplitCreateResponseV2026R0TypeField> type) {
            Type = DocumentSplitCreateResponseV2026R0TypeField.DocumentSplit;
            Id = id;
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}