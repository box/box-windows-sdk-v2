using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataTemplate : ISerializable {
        /// <summary>
        /// The ID of the metadata template.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `metadata_template`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<MetadataTemplateTypeField>))]
        public StringEnum<MetadataTemplateTypeField> Type { get; }

        /// <summary>
        /// The scope of the metadata template can either be `global` or
        /// `enterprise_*`. The `global` scope is used for templates that are
        /// available to any Box enterprise. The `enterprise_*` scope represents
        /// templates that have been created within a specific enterprise, where `*`
        /// will be the ID of that enterprise.
        /// </summary>
        [JsonPropertyName("scope")]
        public string? Scope { get; init; }

        /// <summary>
        /// A unique identifier for the template. This identifier is unique across
        /// the `scope` of the enterprise to which the metadata template is being
        /// applied, yet is not necessarily unique across different enterprises.
        /// </summary>
        [JsonPropertyName("templateKey")]
        public string? TemplateKey { get; init; }

        /// <summary>
        /// The display name of the template. This can be seen in the Box web app
        /// and mobile apps.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string? DisplayName { get; init; }

        /// <summary>
        /// Defines if this template is visible in the Box web app UI, or if
        /// it is purely intended for usage through the API.
        /// </summary>
        [JsonPropertyName("hidden")]
        public bool? Hidden { get; init; }

        /// <summary>
        /// An ordered list of template fields which are part of the template. Each
        /// field can be a regular text field, date field, number field, as well as a
        /// single or multi-select list.
        /// </summary>
        [JsonPropertyName("fields")]
        public IReadOnlyList<MetadataTemplateFieldsField>? Fields { get; init; }

        /// <summary>
        /// Whether or not to include the metadata when a file or folder is copied.
        /// </summary>
        [JsonPropertyName("copyInstanceOnItemCopy")]
        public bool? CopyInstanceOnItemCopy { get; init; }

        public MetadataTemplate(string id, MetadataTemplateTypeField type = MetadataTemplateTypeField.MetadataTemplate) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal MetadataTemplate(string id, StringEnum<MetadataTemplateTypeField> type) {
            Id = id;
            Type = MetadataTemplateTypeField.MetadataTemplate;
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