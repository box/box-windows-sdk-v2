using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobV2026R0SourceSystemField : ISerializable {
        /// <summary>
        /// The value will always be `shuttle_system`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobV2026R0SourceSystemTypeField>))]
        public StringEnum<ShuttleJobV2026R0SourceSystemTypeField> Type { get; }

        /// <summary>
        /// The unique identifier for the source system.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The human-readable name of the source system.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        public ShuttleJobV2026R0SourceSystemField(string id, string name, ShuttleJobV2026R0SourceSystemTypeField type = ShuttleJobV2026R0SourceSystemTypeField.ShuttleSystem) {
            Type = type;
            Id = id;
            Name = name;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleJobV2026R0SourceSystemField(string id, string name, StringEnum<ShuttleJobV2026R0SourceSystemTypeField> type) {
            Type = ShuttleJobV2026R0SourceSystemTypeField.ShuttleSystem;
            Id = id;
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