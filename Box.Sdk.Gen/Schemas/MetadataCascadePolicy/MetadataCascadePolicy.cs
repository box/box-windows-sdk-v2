using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class MetadataCascadePolicy : ISerializable {
        /// <summary>
        /// The ID of the metadata cascade policy object.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `metadata_cascade_policy`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<MetadataCascadePolicyTypeField>))]
        public StringEnum<MetadataCascadePolicyTypeField> Type { get; }

        /// <summary>
        /// The enterprise that owns this policy.
        /// </summary>
        [JsonPropertyName("owner_enterprise")]
        public MetadataCascadePolicyOwnerEnterpriseField? OwnerEnterprise { get; init; }

        /// <summary>
        /// Represent the folder the policy is applied to.
        /// </summary>
        [JsonPropertyName("parent")]
        public MetadataCascadePolicyParentField? Parent { get; init; }

        /// <summary>
        /// The scope of the metadata cascade policy can either be `global` or
        /// `enterprise_*`. The `global` scope is used for policies that are
        /// available to any Box enterprise. The `enterprise_*` scope represents
        /// policies that have been created within a specific enterprise, where `*`
        /// will be the ID of that enterprise.
        /// </summary>
        [JsonPropertyName("scope")]
        public string? Scope { get; init; }

        /// <summary>
        /// The key of the template that is cascaded down to the folder's
        /// children.
        /// 
        /// In many cases the template key is automatically derived
        /// of its display name, for example `Contract Template` would
        /// become `contractTemplate`. In some cases the creator of the
        /// template will have provided its own template key.
        /// 
        /// Please [list the templates for an enterprise][list], or
        /// get all instances on a [file][file] or [folder][folder]
        /// to inspect a template's key.
        /// 
        /// [list]: e://get-metadata-templates-enterprise
        /// [file]: e://get-files-id-metadata
        /// [folder]: e://get-folders-id-metadata
        /// </summary>
        [JsonPropertyName("templateKey")]
        public string? TemplateKey { get; init; }

        public MetadataCascadePolicy(string id, MetadataCascadePolicyTypeField type = MetadataCascadePolicyTypeField.MetadataCascadePolicy) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal MetadataCascadePolicy(string id, StringEnum<MetadataCascadePolicyTypeField> type) {
            Id = id;
            Type = MetadataCascadePolicyTypeField.MetadataCascadePolicy;
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