using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleReportOutputV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier of the generated Box file or folder.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The Box object type of the generated file or folder.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleReportOutputV2026R0TypeField>))]
        public StringEnum<ShuttleReportOutputV2026R0TypeField> Type { get; }

        public ShuttleReportOutputV2026R0(string id, ShuttleReportOutputV2026R0TypeField type) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleReportOutputV2026R0(string id, StringEnum<ShuttleReportOutputV2026R0TypeField> type) {
            Id = id;
            Type = type;
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