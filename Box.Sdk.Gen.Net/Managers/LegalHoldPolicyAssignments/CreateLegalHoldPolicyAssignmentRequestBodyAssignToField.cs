using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateLegalHoldPolicyAssignmentRequestBodyAssignToField : ISerializable {
        /// <summary>
        /// The type of item to assign the policy to.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateLegalHoldPolicyAssignmentRequestBodyAssignToTypeField>))]
        public StringEnum<CreateLegalHoldPolicyAssignmentRequestBodyAssignToTypeField> Type { get; }

        /// <summary>
        /// The ID of item to assign the policy to.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public CreateLegalHoldPolicyAssignmentRequestBodyAssignToField(CreateLegalHoldPolicyAssignmentRequestBodyAssignToTypeField type, string id) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal CreateLegalHoldPolicyAssignmentRequestBodyAssignToField(StringEnum<CreateLegalHoldPolicyAssignmentRequestBodyAssignToTypeField> type, string id) {
            Type = type;
            Id = id;
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