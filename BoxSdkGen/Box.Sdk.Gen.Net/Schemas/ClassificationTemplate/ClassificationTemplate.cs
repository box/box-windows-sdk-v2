using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;

namespace Box.Sdk.Gen.Schemas {
    public class ClassificationTemplate : ISerializable {
        /// <summary>
        /// The ID of the classification template.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `metadata_template`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ClassificationTemplateTypeField>))]
        public StringEnum<ClassificationTemplateTypeField> Type { get; }

        /// <summary>
        /// The scope of the classification template. This is in the format
        /// `enterprise_{id}` where the `id` is the enterprise ID.
        /// </summary>
        [JsonPropertyName("scope")]
        public string Scope { get; }

        /// <summary>
        /// The value will always be `securityClassification-6VMVochwUWo`.
        /// </summary>
        [JsonPropertyName("templateKey")]
        [JsonConverter(typeof(StringEnumConverter<ClassificationTemplateTemplateKeyField>))]
        public StringEnum<ClassificationTemplateTemplateKeyField> TemplateKey { get; }

        /// <summary>
        /// The name of this template as shown in web and mobile interfaces.
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonConverter(typeof(StringEnumConverter<ClassificationTemplateDisplayNameField>))]
        public StringEnum<ClassificationTemplateDisplayNameField> DisplayName { get; }

        /// <summary>
        /// Determines if the
        /// template is always available in web and mobile interfaces.
        /// </summary>
        [JsonPropertyName("hidden")]
        public bool? Hidden { get; init; }

        /// <summary>
        /// Determines if 
        /// classifications are
        /// copied along when the file or folder is
        /// copied.
        /// </summary>
        [JsonPropertyName("copyInstanceOnItemCopy")]
        public bool? CopyInstanceOnItemCopy { get; init; }

        /// <summary>
        /// A list of fields for this classification template. This includes
        /// only one field, the `Box__Security__Classification__Key`, which defines
        /// the different classifications available in this enterprise.
        /// </summary>
        [JsonPropertyName("fields")]
        public IReadOnlyList<ClassificationTemplateFieldsField> Fields { get; }

        public ClassificationTemplate(string id, string scope, IReadOnlyList<ClassificationTemplateFieldsField> fields, ClassificationTemplateTypeField type = ClassificationTemplateTypeField.MetadataTemplate, ClassificationTemplateTemplateKeyField templateKey = ClassificationTemplateTemplateKeyField.SecurityClassification6VmVochwUWo, ClassificationTemplateDisplayNameField displayName = ClassificationTemplateDisplayNameField.Classification) {
            Id = id;
            Type = type;
            Scope = scope;
            TemplateKey = templateKey;
            DisplayName = displayName;
            Fields = fields;
        }
        
        [JsonConstructorAttribute]
        internal ClassificationTemplate(string id, string scope, IReadOnlyList<ClassificationTemplateFieldsField> fields, StringEnum<ClassificationTemplateTypeField> type, StringEnum<ClassificationTemplateTemplateKeyField> templateKey, StringEnum<ClassificationTemplateDisplayNameField> displayName) {
            Id = id;
            Type = ClassificationTemplateTypeField.MetadataTemplate;
            Scope = scope;
            TemplateKey = ClassificationTemplateTemplateKeyField.SecurityClassification6VmVochwUWo;
            DisplayName = ClassificationTemplateDisplayNameField.Classification;
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