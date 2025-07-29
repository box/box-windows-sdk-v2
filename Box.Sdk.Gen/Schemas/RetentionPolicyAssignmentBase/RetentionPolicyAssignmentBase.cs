using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class RetentionPolicyAssignmentBase : ISerializable {
        /// <summary>
        /// The unique identifier that represents a file version.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `retention_policy_assignment`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<RetentionPolicyAssignmentBaseTypeField>))]
        public StringEnum<RetentionPolicyAssignmentBaseTypeField> Type { get; }

        public RetentionPolicyAssignmentBase(string id, RetentionPolicyAssignmentBaseTypeField type = RetentionPolicyAssignmentBaseTypeField.RetentionPolicyAssignment) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal RetentionPolicyAssignmentBase(string id, StringEnum<RetentionPolicyAssignmentBaseTypeField> type) {
            Id = id;
            Type = RetentionPolicyAssignmentBaseTypeField.RetentionPolicyAssignment;
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