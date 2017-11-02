using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a metadata template
    /// </summary>
    public class BoxMetadataTemplate
    {
        public const string FieldTemplateKey = "templateKey";
        public const string FieldScope = "scope";
        public const string FieldDisplayName = "displayName";
        public const string FieldFields = "fields";
        public const string FieldHidden = "hidden";

        /// <summary>
        /// A unique identifier for the template. The identifier must be unique across the scope of the enterprise to which the metadata template is being applied to. Defaults to a string derived from the displayName if no value is provided.
        /// </summary>
        [JsonProperty(PropertyName = FieldTemplateKey)]
        public string TemplateKey { get; set; }

        /// <summary>
        /// The scope of the object. Only the enterprise scope is currently supported.
        /// </summary>
        [JsonProperty(PropertyName = FieldScope)]
        public string Scope { get; set; }

        /// <summary>
        /// The display name of the template.
        /// </summary>
        [JsonProperty(PropertyName = FieldDisplayName)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The ordered set of key:value pairs for the template.
        /// </summary>
        [JsonProperty(PropertyName = FieldFields)]
        public List<BoxMetadataTemplateField> Fields { get; set; }

        /// <summary>
        /// Whether this template is hidden in the UI. Defaults to false.
        /// </summary>
        [JsonProperty(PropertyName = FieldHidden)]
        public bool? Hidden { get; set; }
    }

    /// <summary>
    /// Box representation of a metadata template field
    /// </summary>
    public class BoxMetadataTemplateField
    {
        public const string FieldType = "type";
        public const string FieldKey = "key";
        public const string FieldDisplayName = "displayName";
        public const string FieldOptions = "options";
        public const string FieldHidden = "hidden";

        /// <summary>
        /// The data type of the field's value. Currently, there are four attributes supported by templates: string, enum, float, and date (RFC 3339).
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        public string Type { get; set; }

        /// <summary>
        /// A unique identifier for the field.
        /// </summary>
        [JsonProperty(PropertyName = FieldKey)]
        public string Key { get; set; }

        /// <summary>
        /// The display name of the field.
        /// </summary>
        [JsonProperty(PropertyName = FieldDisplayName)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Whether this field is hidden in the UI. Defaults to false.
        /// </summary>
        [JsonProperty(PropertyName = FieldHidden)]
        public bool? Hidden { get; set; }

        /// <summary>
        /// For fields of type enum this contains the option values
        /// </summary>
        [JsonProperty(PropertyName = FieldOptions)]
        public List<BoxMetadataTemplateFieldOption> Options { get; set; }
    }

    /// <summary>
    /// Box representation of a metadata template field option
    /// </summary>
    public class BoxMetadataTemplateFieldOption
    {
        public const string FieldKey = "key";

        /// <summary>
        /// A unique possible value for the options type
        /// </summary>
        [JsonProperty(PropertyName = FieldKey)]
        public string Key { get; set; }
    }

}
