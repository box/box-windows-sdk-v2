using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateClassificationTemplateRequestBody : ISerializable {
        /// <summary>
        /// The scope in which to create the classifications. This should
        /// be `enterprise` or `enterprise_{id}` where `id` is the unique
        /// ID of the enterprise.
        /// </summary>
        [JsonPropertyName("scope")]
        [JsonConverter(typeof(StringEnumConverter<CreateClassificationTemplateRequestBodyScopeField>))]
        public StringEnum<CreateClassificationTemplateRequestBodyScopeField> Scope { get; }

        /// <summary>
        /// Defines the list of metadata templates.
        /// </summary>
        [JsonPropertyName("templateKey")]
        [JsonConverter(typeof(StringEnumConverter<CreateClassificationTemplateRequestBodyTemplateKeyField>))]
        public StringEnum<CreateClassificationTemplateRequestBodyTemplateKeyField> TemplateKey { get; }

        /// <summary>
        /// The name of the
        /// template as shown in web and mobile interfaces.
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonConverter(typeof(StringEnumConverter<CreateClassificationTemplateRequestBodyDisplayNameField>))]
        public StringEnum<CreateClassificationTemplateRequestBodyDisplayNameField> DisplayName { get; }

        /// <summary>
        /// Determines if the classification template is
        /// hidden or available on web and mobile
        /// devices.
        /// </summary>
        [JsonPropertyName("hidden")]
        public bool? Hidden { get; init; }

        /// <summary>
        /// Determines if classifications are
        /// copied along when the file or folder is
        /// copied.
        /// </summary>
        [JsonPropertyName("copyInstanceOnItemCopy")]
        public bool? CopyInstanceOnItemCopy { get; init; }

        /// <summary>
        /// The classification template requires exactly
        /// one field, which holds
        /// all the valid classification values.
        /// </summary>
        [JsonPropertyName("fields")]
        public IReadOnlyList<CreateClassificationTemplateRequestBodyFieldsField> Fields { get; }

        public CreateClassificationTemplateRequestBody(IReadOnlyList<CreateClassificationTemplateRequestBodyFieldsField> fields, CreateClassificationTemplateRequestBodyScopeField scope = CreateClassificationTemplateRequestBodyScopeField.Enterprise, CreateClassificationTemplateRequestBodyTemplateKeyField templateKey = CreateClassificationTemplateRequestBodyTemplateKeyField.SecurityClassification6VmVochwUWo, CreateClassificationTemplateRequestBodyDisplayNameField displayName = CreateClassificationTemplateRequestBodyDisplayNameField.Classification) {
            Scope = scope;
            TemplateKey = templateKey;
            DisplayName = displayName;
            Fields = fields;
        }
        
        [JsonConstructorAttribute]
        internal CreateClassificationTemplateRequestBody(IReadOnlyList<CreateClassificationTemplateRequestBodyFieldsField> fields, StringEnum<CreateClassificationTemplateRequestBodyScopeField> scope, StringEnum<CreateClassificationTemplateRequestBodyTemplateKeyField> templateKey, StringEnum<CreateClassificationTemplateRequestBodyDisplayNameField> displayName) {
            Scope = CreateClassificationTemplateRequestBodyScopeField.Enterprise;
            TemplateKey = CreateClassificationTemplateRequestBodyTemplateKeyField.SecurityClassification6VmVochwUWo;
            DisplayName = CreateClassificationTemplateRequestBodyDisplayNameField.Classification;
            Fields = fields;
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