using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a group membership
    /// </summary>
    public class BoxGroupMembership : BoxEntity
    {
        public const string FieldRole = "role";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";
        public const string FieldUser = "user";
        public const string FieldGroup = "group";

        /// <summary>
        /// The role of the user in this group
        /// </summary>
        [JsonProperty (PropertyName = FieldRole)]
        public string Role { get; set; }

        /// <summary>
        /// Date and time this membership was created
        /// </summary>
        [JsonProperty (PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Date and time this membership was modified
        /// </summary>
        [JsonProperty (PropertyName = FieldModifiedAt)]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// The user in this membership
        /// </summary>
        [JsonProperty (PropertyName = FieldUser)]
        public BoxUser User { get; set; }

        /// <summary>
        /// The group in this membership
        /// </summary>
        [JsonProperty (PropertyName = FieldGroup)]
        public BoxGroup Group { get; set; }
    }
}
