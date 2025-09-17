using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateRetentionPolicyAssignmentRequestBody : ISerializable {
        /// <summary>
        /// The ID of the retention policy to assign.
        /// </summary>
        [JsonPropertyName("policy_id")]
        public string PolicyId { get; }

        /// <summary>
        /// The item to assign the policy to.
        /// </summary>
        [JsonPropertyName("assign_to")]
        public CreateRetentionPolicyAssignmentRequestBodyAssignToField AssignTo { get; }

        /// <summary>
        /// If the `assign_to` type is `metadata_template`,
        /// then optionally add the `filter_fields` parameter which will
        /// require an array of objects with a field entry and a value entry.
        /// Currently only one object of `field` and `value` is supported.
        /// </summary>
        [JsonPropertyName("filter_fields")]
        public IReadOnlyList<CreateRetentionPolicyAssignmentRequestBodyFilterFieldsField>? FilterFields { get; init; }

        /// <summary>
        /// The date the retention policy assignment begins.
        /// 
        /// If the `assigned_to` type is `metadata_template`,
        /// this field can be a date field's metadata attribute key id.
        /// </summary>
        [JsonPropertyName("start_date_field")]
        public string? StartDateField { get; init; }

        public CreateRetentionPolicyAssignmentRequestBody(string policyId, CreateRetentionPolicyAssignmentRequestBodyAssignToField assignTo) {
            PolicyId = policyId;
            AssignTo = assignTo;
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