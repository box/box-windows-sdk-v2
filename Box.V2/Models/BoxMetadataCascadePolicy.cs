using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxMetadataCascadePolicy : BoxEntity
    {
        public const string FieldOwnerEnterprise = "owner_enterprise";
        public const string FieldParent = "parent";
        public const string FieldScope = "scope";
        public const string FieldTemplateKey = "templateKey";

        /// <summary>
        /// Gets the owner enterprise of the cascade policy.
        /// </summary>
        [JsonProperty(PropertyName = FieldOwnerEnterprise)]
        public virtual BoxEntity OwnerEnterprise { get; private set; }

        /// <summary>
        /// Gets the parent of the cascade policy.
        /// </summary>
        [JsonProperty(PropertyName = FieldParent)]
        public virtual BoxEntity Parent { get; private set; }

        /// <summary>
        /// Gets the scope of the cascade policy.
        /// </summary>
        [JsonProperty(PropertyName = FieldScope)]
        public virtual string Scope { get; private set; }

        /// <summary>
        /// Gets the template key of the cascade policy.
        /// </summary>
        [JsonProperty(PropertyName = FieldTemplateKey)]
        public virtual string TemplateKey { get; private set; }
    }
}
