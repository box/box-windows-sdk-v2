using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateClassificationRequestBody : ISerializable {
        /// <summary>
        /// The type of change to perform on the classification
        /// object.
        /// </summary>
        [JsonPropertyName("op")]
        [JsonConverter(typeof(StringEnumConverter<UpdateClassificationRequestBodyOpField>))]
        public StringEnum<UpdateClassificationRequestBodyOpField> Op { get; }

        /// <summary>
        /// Defines classifications 
        /// available in the enterprise.
        /// </summary>
        [JsonPropertyName("fieldKey")]
        [JsonConverter(typeof(StringEnumConverter<UpdateClassificationRequestBodyFieldKeyField>))]
        public StringEnum<UpdateClassificationRequestBodyFieldKeyField> FieldKey { get; }

        /// <summary>
        /// The original label of the classification to change.
        /// </summary>
        [JsonPropertyName("enumOptionKey")]
        public string EnumOptionKey { get; }

        /// <summary>
        /// The details of the updated classification.
        /// </summary>
        [JsonPropertyName("data")]
        public UpdateClassificationRequestBodyDataField Data { get; }

        public UpdateClassificationRequestBody(string enumOptionKey, UpdateClassificationRequestBodyDataField data, UpdateClassificationRequestBodyOpField op = UpdateClassificationRequestBodyOpField.EditEnumOption, UpdateClassificationRequestBodyFieldKeyField fieldKey = UpdateClassificationRequestBodyFieldKeyField.BoxSecurityClassificationKey) {
            Op = op;
            FieldKey = fieldKey;
            EnumOptionKey = enumOptionKey;
            Data = data;
        }
        
        [JsonConstructorAttribute]
        internal UpdateClassificationRequestBody(string enumOptionKey, UpdateClassificationRequestBodyDataField data, StringEnum<UpdateClassificationRequestBodyOpField> op, StringEnum<UpdateClassificationRequestBodyFieldKeyField> fieldKey) {
            Op = UpdateClassificationRequestBodyOpField.EditEnumOption;
            FieldKey = UpdateClassificationRequestBodyFieldKeyField.BoxSecurityClassificationKey;
            EnumOptionKey = enumOptionKey;
            Data = data;
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