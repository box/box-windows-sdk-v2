using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateMetadataTemplateRequestBodyFieldsField : ISerializable {
        /// <summary>
        /// The type of field. The basic fields are a `string` field for text, a
        /// `float` field for numbers, and a `date` fields to present the user with a
        /// date-time picker.
        /// 
        /// Additionally, metadata templates support an `enum` field for a basic list
        /// of items, and ` multiSelect` field for a similar list of items where the
        /// user can select more than one value.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateMetadataTemplateRequestBodyFieldsTypeField>))]
        public StringEnum<CreateMetadataTemplateRequestBodyFieldsTypeField> Type { get; }

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
        /// A list of options for this field. This is used in combination with the
        /// `enum` and `multiSelect` field types.
        /// </summary>
        [JsonPropertyName("options")]
        public IReadOnlyList<CreateMetadataTemplateRequestBodyFieldsOptionsField>? Options { get; init; }

        public CreateMetadataTemplateRequestBodyFieldsField(CreateMetadataTemplateRequestBodyFieldsTypeField type, string key, string displayName) {
            Type = type;
            Key = key;
            DisplayName = displayName;
        }
        
        [JsonConstructorAttribute]
        internal CreateMetadataTemplateRequestBodyFieldsField(StringEnum<CreateMetadataTemplateRequestBodyFieldsTypeField> type, string key, string displayName) {
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