using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    public class BoxLegalHoldPolicyRequest
    {
        public const string FieldPolicyName = "policy_name";
        public const string FieldDescription = "description";
        public const string FieldFilterStartedAt = "filter_started_at";
        public const string FieldFilterEndedAt = "filter_ended_at";
        public const string FieldReleaseNotes = "release_notes";

        /// <summary>
        /// Name of Legal Hold Policy. Max characters 254.
        /// </summary>
        [JsonProperty(PropertyName = FieldPolicyName)]
        public string PolicyName { get; set; }

        /// <summary>
        /// Description of Legal Hold Policy. Max characters 500.
        /// </summary>
        [JsonProperty(PropertyName = FieldDescription)]
        public string Description { get; set; }

        /// <summary>
        /// Date filter applies to Custodian assignments only.
        /// </summary>
        [JsonProperty(PropertyName = FieldFilterStartedAt)]
        public DateTime? FilterStartedAt { get; set; }

        /// <summary>
        /// Date filter applies to Custodian assignments only.
        /// </summary>
        [JsonProperty(PropertyName = FieldFilterEndedAt)]
        public DateTime? FilterEndedAt { get; set; }

        /// <summary>
        /// Notes around why the policy was released. Optional property with a 500 character limit.
        /// </summary>
        [JsonProperty(PropertyName = FieldReleaseNotes)]
        public DateTime? ReleaseNotes { get; set; }

    }
}
