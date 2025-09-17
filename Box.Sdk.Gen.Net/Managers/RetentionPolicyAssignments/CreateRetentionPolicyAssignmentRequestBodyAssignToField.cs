using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateRetentionPolicyAssignmentRequestBodyAssignToField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isidSet")]
        protected bool _isIdSet { get; set; }

        protected string? _id { get; set; }

        /// <summary>
        /// The type of item to assign the policy to.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateRetentionPolicyAssignmentRequestBodyAssignToTypeField>))]
        public StringEnum<CreateRetentionPolicyAssignmentRequestBodyAssignToTypeField> Type { get; }

        /// <summary>
        /// The ID of item to assign the policy to.
        /// Set to `null` or omit when `type` is set to
        /// `enterprise`.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get => _id; init { _id = value; _isIdSet = true; } }

        public CreateRetentionPolicyAssignmentRequestBodyAssignToField(CreateRetentionPolicyAssignmentRequestBodyAssignToTypeField type) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal CreateRetentionPolicyAssignmentRequestBodyAssignToField(StringEnum<CreateRetentionPolicyAssignmentRequestBodyAssignToTypeField> type) {
            Type = type;
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