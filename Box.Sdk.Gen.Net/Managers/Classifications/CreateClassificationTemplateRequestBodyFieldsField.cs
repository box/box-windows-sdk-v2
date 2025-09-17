using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateClassificationTemplateRequestBodyFieldsField : ISerializable {
        /// <summary>
        /// The type of the field
        /// that is always enum.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateClassificationTemplateRequestBodyFieldsTypeField>))]
        public StringEnum<CreateClassificationTemplateRequestBodyFieldsTypeField> Type { get; }

        /// <summary>
        /// Defines classifications 
        /// available in the enterprise.
        /// </summary>
        [JsonPropertyName("key")]
        [JsonConverter(typeof(StringEnumConverter<CreateClassificationTemplateRequestBodyFieldsKeyField>))]
        public StringEnum<CreateClassificationTemplateRequestBodyFieldsKeyField> Key { get; }

        /// <summary>
        /// A display name for the classification.
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonConverter(typeof(StringEnumConverter<CreateClassificationTemplateRequestBodyFieldsDisplayNameField>))]
        public StringEnum<CreateClassificationTemplateRequestBodyFieldsDisplayNameField> DisplayName { get; }

        /// <summary>
        /// Determines if the classification
        /// template is
        /// hidden or available on
        /// web and mobile
        /// devices.
        /// </summary>
        [JsonPropertyName("hidden")]
        public bool? Hidden { get; init; }

        /// <summary>
        /// The actual list of classifications that are present on
        /// this template.
        /// </summary>
        [JsonPropertyName("options")]
        public IReadOnlyList<CreateClassificationTemplateRequestBodyFieldsOptionsField> Options { get; }

        public CreateClassificationTemplateRequestBodyFieldsField(IReadOnlyList<CreateClassificationTemplateRequestBodyFieldsOptionsField> options, CreateClassificationTemplateRequestBodyFieldsTypeField type = CreateClassificationTemplateRequestBodyFieldsTypeField.Enum, CreateClassificationTemplateRequestBodyFieldsKeyField key = CreateClassificationTemplateRequestBodyFieldsKeyField.BoxSecurityClassificationKey, CreateClassificationTemplateRequestBodyFieldsDisplayNameField displayName = CreateClassificationTemplateRequestBodyFieldsDisplayNameField.Classification) {
            Type = type;
            Key = key;
            DisplayName = displayName;
            Options = options;
        }
        
        [JsonConstructorAttribute]
        internal CreateClassificationTemplateRequestBodyFieldsField(IReadOnlyList<CreateClassificationTemplateRequestBodyFieldsOptionsField> options, StringEnum<CreateClassificationTemplateRequestBodyFieldsTypeField> type, StringEnum<CreateClassificationTemplateRequestBodyFieldsKeyField> key, StringEnum<CreateClassificationTemplateRequestBodyFieldsDisplayNameField> displayName) {
            Type = CreateClassificationTemplateRequestBodyFieldsTypeField.Enum;
            Key = CreateClassificationTemplateRequestBodyFieldsKeyField.BoxSecurityClassificationKey;
            DisplayName = CreateClassificationTemplateRequestBodyFieldsDisplayNameField.Classification;
            Options = options;
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