using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TemplateSignerInputCoordinatesField : ISerializable {
        /// <summary>
        /// Relative x coordinate to the page the input is on, ranging from 0 to 1.
        /// </summary>
        [JsonPropertyName("x")]
        public double? X { get; init; }

        /// <summary>
        /// Relative y coordinate to the page the input is on, ranging from 0 to 1.
        /// </summary>
        [JsonPropertyName("y")]
        public double? Y { get; init; }

        public TemplateSignerInputCoordinatesField() {
            
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