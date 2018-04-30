﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models.Request
{
    public class BoxRetentionPolicyAssignmentRequest
    {
        /// <summary>
        /// The id of the retention policy to assign this content to.
        /// </summary>
        [JsonProperty(PropertyName = "policy_id")]
        public string PolicyId { get; set; }

        /// <summary>
        /// The type and id of the content to assign the retention policy to. If assigning to an enterprise, no id should be provided.
        /// </summary>
        [JsonProperty(PropertyName = "assign_to")]
        public BoxRequestEntity AssignTo { get; set; }

        /// <summary>
        /// An optional list of metadata field filters to use when applying the retention policy to a metadata template, e.g. {"field": "foo", "value": "bar"}
        /// </summary>
        [JsonProperty(PropertyName = "filter_fields")]
        public List<object> FilterFields { get; set; }
    }
}
