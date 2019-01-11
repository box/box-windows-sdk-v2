using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a group
    /// </summary>
    public class BoxGroup : BoxEntity
    {
        public const string FieldName = "name";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";
        public const string FieldDescription = "description";
        public const string FieldProvenance = "provenance";
        public const string FieldExternalSyncIdentifier = "external_sync_identifier";
        public const string FieldInvitabilityLevel = "invitability_level";
        public const string FieldMemberViewabilityLevel = "member_viewability_level";

        /// <summary>
        /// The name of the group
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public string Name { get; private set; }

        /// <summary>
        /// When this group was created on Box's servers
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// When this group was last updated on Box's servers
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public DateTime? ModifiedAt { get; private set; }

        /// <summary>
        /// A description of the group.
        /// </summary>
        [JsonProperty(PropertyName = FieldDescription)]
        public string Description { get; private set; }

        /// <summary>
        /// Used to track the external source where the group is coming from.
        /// </summary>
        [JsonProperty(PropertyName = FieldProvenance)]
        public string Provenance { get; private set; }

        /// <summary>
        /// Used as a group identifier for groups coming from an external source.
        /// </summary>
        [JsonProperty(PropertyName = FieldExternalSyncIdentifier)]
        public string ExternalSyncIdentifier { get; private set; }

        /// <summary>
        /// Specifies who can invite this group to folders. Retrieved through the fields parameter.
        /// </summary>
        [JsonProperty(PropertyName = FieldInvitabilityLevel)]
        public string InvitabilityLevel { get; private set; }

        /// <summary>
        /// Specifies who can view the members of this group. Retrieved through the fields parameter.
        /// </summary>
        [JsonProperty(PropertyName = FieldMemberViewabilityLevel)]
        public string MemberViewabilityLevel { get; private set; }
    }
}
