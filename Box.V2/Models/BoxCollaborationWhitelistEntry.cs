using System;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box whitelist of a domain.
    /// </summary>
    public class BoxCollaborationWhitelistEntry : BoxEntity
    {
        public const string FieldDomain = "domain";
        public const string FieldDirection = "direction";
        public const string FieldEnterprise = "enterprise";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";

        /// <summary>
        /// The domain to be whitelisted for collaboration.
        /// </summary>
        [JsonProperty(PropertyName = FieldDomain)]
        public virtual string Domain { get; set; }

        /// <summary>
        /// The direction of the whitelist for collaboration.
        /// </summary>
        [JsonProperty(PropertyName = FieldDirection)]
        public virtual string Direction { get; set; }

        /// <summary>
        /// The enterprise the collaboration whitelist belongs to.
        /// </summary>
        [JsonProperty(PropertyName = FieldEnterprise)]
        public virtual BoxEnterprise Enterprise { get; set; }

        /// <summary>
        /// The time this collaboration whitelist was created.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The time this collaboration whitelist was modified.
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; set; }
    }
}
