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

        [JsonProperty(PropertyName = FieldTemplateKey)]
        public string TemplateKey { get; set; }

        [JsonProperty(PropertyName = FieldScope)]
        public string Scope { get; set; }

        [JsonProperty(PropertyName = FieldDisplayName)]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = FieldFields)]
        public List<BoxMetadataTemplateField> Fields { get; set; }
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

        [JsonProperty(PropertyName = FieldType)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = FieldKey)]
        public string Key { get; set; }

        [JsonProperty(PropertyName = FieldDisplayName)]
        public string DisplayName { get; set; }

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

        [JsonProperty(PropertyName = FieldKey)]
        public string Key { get; private set; }
    }

}
