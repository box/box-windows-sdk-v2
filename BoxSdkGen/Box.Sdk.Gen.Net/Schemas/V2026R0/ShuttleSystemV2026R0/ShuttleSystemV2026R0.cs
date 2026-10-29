using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleSystemV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier for the shuttle system.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `shuttle_system`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleSystemV2026R0TypeField>))]
        public StringEnum<ShuttleSystemV2026R0TypeField> Type { get; }

        /// <summary>
        /// The display name of the shuttle system.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// Identifies the kind of source or destination this system represents, for
        /// example `box`, `windows`, `sharepoint`, or `amazon_s3`. New system types
        /// are added over time, so clients must tolerate values they do not
        /// recognize rather than treating this as a fixed set.
        /// </summary>
        [JsonPropertyName("system_type")]
        public string SystemType { get; }

        /// <summary>
        /// A server-computed display label for the system.
        /// </summary>
        [JsonPropertyName("label")]
        public string? Label { get; init; }

        /// <summary>
        /// The current connection state of the shuttle system.
        /// </summary>
        [JsonPropertyName("state")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleSystemV2026R0StateField>))]
        public StringEnum<ShuttleSystemV2026R0StateField> State { get; }

        public ShuttleSystemV2026R0(string id, string name, string systemType, ShuttleSystemV2026R0StateField state, ShuttleSystemV2026R0TypeField type = ShuttleSystemV2026R0TypeField.ShuttleSystem) {
            Id = id;
            Type = type;
            Name = name;
            SystemType = systemType;
            State = state;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleSystemV2026R0(string id, string name, string systemType, StringEnum<ShuttleSystemV2026R0StateField> state, StringEnum<ShuttleSystemV2026R0TypeField> type) {
            Id = id;
            Type = ShuttleSystemV2026R0TypeField.ShuttleSystem;
            Name = name;
            SystemType = systemType;
            State = state;
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