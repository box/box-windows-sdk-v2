using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ClassificationTemplateFieldsOptionsStaticConfigClassificationField : ISerializable {
        /// <summary>
        /// A longer description of the classification.
        /// </summary>
        [JsonPropertyName("classificationDefinition")]
        public string? ClassificationDefinition { get; init; }

        /// <summary>
        /// An internal Box identifier used to assign a color to
        /// a classification label.
        /// 
        /// Mapping between a `colorID` and a color may change
        /// without notice. Currently, the color mappings are as
        /// follows.
        /// 
        /// * `0`: Yellow.
        /// * `1`: Orange.
        /// * `2`: Watermelon red.
        /// * `3`: Purple rain.
        /// * `4`: Light blue.
        /// * `5`: Dark blue.
        /// * `6`: Light green.
        /// * `7`: Gray.
        /// </summary>
        [JsonPropertyName("colorID")]
        public long? ColorId { get; init; }

        public ClassificationTemplateFieldsOptionsStaticConfigClassificationField() {
            
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