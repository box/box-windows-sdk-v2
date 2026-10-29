using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleSystemCreateRequestV2026R0 : ISerializable {
        /// <summary>
        /// The display name for the new shuttle system.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// The public system type identifier for the new shuttle system. Only
        /// `windows` is currently supported through the public API.
        /// </summary>
        [JsonPropertyName("system_type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleSystemCreateRequestV2026R0SystemTypeField>))]
        public StringEnum<ShuttleSystemCreateRequestV2026R0SystemTypeField> SystemType { get; }

        public ShuttleSystemCreateRequestV2026R0(string name, ShuttleSystemCreateRequestV2026R0SystemTypeField systemType = ShuttleSystemCreateRequestV2026R0SystemTypeField.Windows) {
            Name = name;
            SystemType = systemType;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleSystemCreateRequestV2026R0(string name, StringEnum<ShuttleSystemCreateRequestV2026R0SystemTypeField> systemType) {
            Name = name;
            SystemType = ShuttleSystemCreateRequestV2026R0SystemTypeField.Windows;
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