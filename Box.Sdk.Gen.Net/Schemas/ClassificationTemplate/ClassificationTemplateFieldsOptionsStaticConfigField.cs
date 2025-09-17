using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ClassificationTemplateFieldsOptionsStaticConfigField : ISerializable {
        /// <summary>
        /// Additional information about the classification.
        /// 
        /// This is not an exclusive list of properties, and
        /// more object fields might be returned. These fields
        /// are used for internal Box Shield and Box Governance
        /// purposes and no additional value must be derived from
        /// these fields.
        /// </summary>
        [JsonPropertyName("classification")]
        public ClassificationTemplateFieldsOptionsStaticConfigClassificationField? Classification { get; init; }

        public ClassificationTemplateFieldsOptionsStaticConfigField() {
            
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