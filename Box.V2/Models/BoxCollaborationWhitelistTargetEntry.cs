﻿using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box whitelist of a target.
    /// </summary>
    public class BoxCollaborationWhitelistTargetEntry : BoxEntity
    {
        public const string FieldUser = "user";
        public const string FieldEnterprise = "enterprise";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";

        /// <summary>
        /// The user that is exempt from whitelist.
        /// </summary>
        [JsonProperty(PropertyName = FieldUser)]
        public BoxUser User { get; set; }

        /// <summary>
        /// The domain the whitelist is active in.
        /// </summary>
        [JsonProperty(PropertyName = FieldEnterprise)]
        public BoxEnterprise Enterprise { get; set; }

        /// <summary>
        /// The time this whitelist was created at.
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// The time this whitelist was modified at.
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public DateTime? ModifiedAt { get; set; }
    }
}
