using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Box.V2.Models
{
    public class BoxMetadataTemplate
    {
        public const string FieldTemplateKey = "templateKey";
        public const string FieldScope = "scope";
        public const string FieldDisplayName = "displayName";
        public const string FieldFields = "fields";

        [JsonProperty(PropertyName = FieldTemplateKey)]
        public string TemplateKey { get; private set; }

        [JsonProperty(PropertyName = FieldScope)]
        public string Scope { get; private set; }

        [JsonProperty(PropertyName = FieldDisplayName)]
        public string DisplayName { get; private set; }

        [JsonProperty(PropertyName = FieldFields)]
        public List<BoxMetadataTemplateField> Fields { get; private set; }
    }

    public class BoxMetadataTemplateField
    {
        public const string FieldType = "type";
        public const string FieldKey = "key";
        public const string FieldDisplayName = "displayName";
        public const string FieldOptions = "options";

        [JsonProperty(PropertyName = FieldType)]
        public string Type { get; private set; }

        [JsonProperty(PropertyName = FieldKey)]
        public string Key { get; private set; }

        [JsonProperty(PropertyName = FieldDisplayName)]
        public string DisplayName { get; private set; }

        [JsonProperty(PropertyName = FieldOptions)]
        public List<BoxMetadataTemplateFieldOption> Options { get; private set; }
    }

    public class BoxMetadataTemplateFieldOption
    {
        public const string FieldKey = "key";

        [JsonProperty(PropertyName = FieldKey)]
        public string Key { get; private set; }
    }

}
