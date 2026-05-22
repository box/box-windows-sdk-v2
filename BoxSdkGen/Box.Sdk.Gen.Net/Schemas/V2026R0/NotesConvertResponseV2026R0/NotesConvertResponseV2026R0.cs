using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class NotesConvertResponseV2026R0 : ISerializable {
        /// <summary>
        /// The Box resource type; always `file` for a Box file.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<NotesConvertResponseV2026R0TypeField>))]
        public StringEnum<NotesConvertResponseV2026R0TypeField> Type { get; }

        /// <summary>
        /// Box file ID of the created `.boxnote` file.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public NotesConvertResponseV2026R0(string id, NotesConvertResponseV2026R0TypeField type = NotesConvertResponseV2026R0TypeField.File) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal NotesConvertResponseV2026R0(string id, StringEnum<NotesConvertResponseV2026R0TypeField> type) {
            Type = NotesConvertResponseV2026R0TypeField.File;
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