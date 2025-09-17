using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class TrackingCode : ISerializable {
        /// <summary>
        /// The value will always be `tracking_code`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TrackingCodeTypeField>))]
        public StringEnum<TrackingCodeTypeField>? Type { get; init; }

        /// <summary>
        /// The name of the tracking code, which must be preconfigured in
        /// the Admin Console.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The value of the tracking code.
        /// </summary>
        [JsonPropertyName("value")]
        public string? Value { get; init; }

        public TrackingCode() {
            
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