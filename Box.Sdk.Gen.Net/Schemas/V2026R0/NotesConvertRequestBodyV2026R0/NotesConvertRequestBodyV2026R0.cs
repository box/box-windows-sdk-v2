using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class NotesConvertRequestBodyV2026R0 : ISerializable {
        /// <summary>
        /// The content to convert to a note. See the `content_format` field for supported formats.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; }

        /// <summary>
        /// Format of the content to convert.
        /// </summary>
        [JsonPropertyName("content_format")]
        [JsonConverter(typeof(StringEnumConverter<NotesConvertRequestBodyV2026R0ContentFormatField>))]
        public StringEnum<NotesConvertRequestBodyV2026R0ContentFormatField> ContentFormat { get; }

        [JsonPropertyName("parent")]
        public FolderReferenceV2026R0 Parent { get; }

        /// <summary>
        /// The name for the created note. The `.boxnote` extension is appended automatically.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        public NotesConvertRequestBodyV2026R0(string content, FolderReferenceV2026R0 parent, string name, NotesConvertRequestBodyV2026R0ContentFormatField contentFormat = NotesConvertRequestBodyV2026R0ContentFormatField.Markdown) {
            Content = content;
            ContentFormat = contentFormat;
            Parent = parent;
            Name = name;
        }
        
        [JsonConstructorAttribute]
        internal NotesConvertRequestBodyV2026R0(string content, FolderReferenceV2026R0 parent, string name, StringEnum<NotesConvertRequestBodyV2026R0ContentFormatField> contentFormat) {
            Content = content;
            ContentFormat = NotesConvertRequestBodyV2026R0ContentFormatField.Markdown;
            Parent = parent;
            Name = name;
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