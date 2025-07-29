using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataTemplateFieldsField : ISerializable {
        /// <summary>
        /// The type of field. The basic fields are a `string` field for text, a
        /// `float` field for numbers, and a `date` fields to present the user with a
        /// date-time picker.
        /// 
        /// Additionally, metadata templates support an `enum` field for a basic list
        /// of items, and ` multiSelect` field for a similar list of items where the
        /// user can select more than one value.
        /// 
        /// **Note**: The `integer` value is deprecated.
        /// It is still present in the response,
        /// but cannot be used in the POST request.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<MetadataTemplateFieldsTypeField>))]
        public StringEnum<MetadataTemplateFieldsTypeField> Type { get; }

        /// <summary>
        /// A unique identifier for the field. The identifier must
        /// be unique within the template to which it belongs.
        /// </summary>
        [JsonPropertyName("key")]
        public string Key { get; }

        /// <summary>
        /// The display name of the field as it is shown to the user in the web and
        /// mobile apps.
        /// </summary>
        [JsonPropertyName("displayName")]
        public string DisplayName { get; }

        /// <summary>
        /// A description of the field. This is not shown to the user.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// Whether this field is hidden in the UI for the user and can only be set
        /// through the API instead.
        /// </summary>
        [JsonPropertyName("hidden")]
        public bool? Hidden { get; init; }

        /// <summary>
        /// A list of options for this field. This is used in combination
        /// with the `enum` and `multiSelect` field types.
        /// </summary>
        [JsonPropertyName("options")]
        public IReadOnlyList<MetadataTemplateFieldsOptionsField>? Options { get; init; }

        /// <summary>
        /// The unique ID of the metadata template field.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        public MetadataTemplateFieldsField(MetadataTemplateFieldsTypeField type, string key, string displayName) {
            Type = type;
            Key = key;
            DisplayName = displayName;
        }
        
        [JsonConstructorAttribute]
        internal MetadataTemplateFieldsField(StringEnum<MetadataTemplateFieldsTypeField> type, string key, string displayName) {
            Type = type;
            Key = key;
            DisplayName = displayName;
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