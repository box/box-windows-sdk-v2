using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitCreateRequestSmartV2026R0 : ISerializable {
        /// <summary>
        /// Discriminator. Must be `smart` for this variant.
        /// </summary>
        [JsonPropertyName("split_type")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitCreateRequestSmartV2026R0SplitTypeField>))]
        public StringEnum<DocumentSplitCreateRequestSmartV2026R0SplitTypeField> SplitType { get; set; }

        /// <summary>
        /// A natural language prompt that guides automatic page range detection.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        public DocumentSplitCreateRequestSmartV2026R0(string prompt, DocumentSplitCreateRequestSmartV2026R0SplitTypeField splitType = DocumentSplitCreateRequestSmartV2026R0SplitTypeField.Smart) {
            SplitType = splitType;
            Prompt = prompt;
        }
        
        [JsonConstructorAttribute]
        internal DocumentSplitCreateRequestSmartV2026R0(string prompt, StringEnum<DocumentSplitCreateRequestSmartV2026R0SplitTypeField> splitType) {
            SplitType = DocumentSplitCreateRequestSmartV2026R0SplitTypeField.Smart;
            Prompt = prompt;
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