using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TemplateSignerInputDimensionsField : ISerializable {
        /// <summary>
        /// Relative width to the page the input is on, ranging from 0 to 1.
        /// </summary>
        [JsonPropertyName("width")]
        public double? Width { get; init; }

        /// <summary>
        /// Relative height to the page the input is on, ranging from 0 to 1.
        /// </summary>
        [JsonPropertyName("height")]
        public double? Height { get; init; }

        public TemplateSignerInputDimensionsField() {
            
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