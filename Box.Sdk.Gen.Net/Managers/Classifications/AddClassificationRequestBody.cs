using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class AddClassificationRequestBody : ISerializable {
        /// <summary>
        /// The type of change to perform on the classification
        /// object.
        /// </summary>
        [JsonPropertyName("op")]
        [JsonConverter(typeof(StringEnumConverter<AddClassificationRequestBodyOpField>))]
        public StringEnum<AddClassificationRequestBodyOpField> Op { get; }

        /// <summary>
        /// Defines classifications 
        /// available in the enterprise.
        /// </summary>
        [JsonPropertyName("fieldKey")]
        [JsonConverter(typeof(StringEnumConverter<AddClassificationRequestBodyFieldKeyField>))]
        public StringEnum<AddClassificationRequestBodyFieldKeyField> FieldKey { get; }

        /// <summary>
        /// The details of the classification to add.
        /// </summary>
        [JsonPropertyName("data")]
        public AddClassificationRequestBodyDataField Data { get; }

        public AddClassificationRequestBody(AddClassificationRequestBodyDataField data, AddClassificationRequestBodyOpField op = AddClassificationRequestBodyOpField.AddEnumOption, AddClassificationRequestBodyFieldKeyField fieldKey = AddClassificationRequestBodyFieldKeyField.BoxSecurityClassificationKey) {
            Op = op;
            FieldKey = fieldKey;
            Data = data;
        }
        
        [JsonConstructorAttribute]
        internal AddClassificationRequestBody(AddClassificationRequestBodyDataField data, StringEnum<AddClassificationRequestBodyOpField> op, StringEnum<AddClassificationRequestBodyFieldKeyField> fieldKey) {
            Op = AddClassificationRequestBodyOpField.AddEnumOption;
            FieldKey = AddClassificationRequestBodyFieldKeyField.BoxSecurityClassificationKey;
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