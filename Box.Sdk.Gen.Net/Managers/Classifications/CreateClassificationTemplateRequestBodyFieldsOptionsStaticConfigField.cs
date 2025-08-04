using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateClassificationTemplateRequestBodyFieldsOptionsStaticConfigField : ISerializable {
        /// <summary>
        /// Additional information about the classification.
        /// </summary>
        [JsonPropertyName("classification")]
        public CreateClassificationTemplateRequestBodyFieldsOptionsStaticConfigClassificationField? Classification { get; init; }

        public CreateClassificationTemplateRequestBodyFieldsOptionsStaticConfigField() {
            
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