using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateLegalHoldPolicyAssignmentRequestBody : ISerializable {
        /// <summary>
        /// The ID of the policy to assign.
        /// </summary>
        [JsonPropertyName("policy_id")]
        public string PolicyId { get; }

        /// <summary>
        /// The item to assign the policy to.
        /// </summary>
        [JsonPropertyName("assign_to")]
        public CreateLegalHoldPolicyAssignmentRequestBodyAssignToField AssignTo { get; }

        public CreateLegalHoldPolicyAssignmentRequestBody(string policyId, CreateLegalHoldPolicyAssignmentRequestBodyAssignToField assignTo) {
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