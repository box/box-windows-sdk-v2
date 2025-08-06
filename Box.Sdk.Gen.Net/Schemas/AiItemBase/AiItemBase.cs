using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class AiItemBase : ISerializable {
        /// <summary>
        /// The ID of the file.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The type of the item. Currently the value can be `file` only.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<AiItemBaseTypeField>))]
        public StringEnum<AiItemBaseTypeField> Type { get; }

        /// <summary>
        /// The content of the item, often the text representation.
        /// </summary>
        [JsonPropertyName("content")]
        public string? Content { get; init; }

        public AiItemBase(string id, AiItemBaseTypeField type = AiItemBaseTypeField.File) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal AiItemBase(string id, StringEnum<AiItemBaseTypeField> type) {
            Id = id;
            Type = AiItemBaseTypeField.File;
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