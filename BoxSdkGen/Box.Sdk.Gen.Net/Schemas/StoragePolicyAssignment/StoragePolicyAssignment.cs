using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class StoragePolicyAssignment : ISerializable {
        /// <summary>
        /// The unique identifier for a storage policy assignment.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `storage_policy_assignment`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<StoragePolicyAssignmentTypeField>))]
        public StringEnum<StoragePolicyAssignmentTypeField> Type { get; }

        [JsonPropertyName("storage_policy")]
        public StoragePolicyMini? StoragePolicy { get; init; }

        [JsonPropertyName("assigned_to")]
        public StoragePolicyAssignmentAssignedToField? AssignedTo { get; init; }

        public StoragePolicyAssignment(string id, StoragePolicyAssignmentTypeField type = StoragePolicyAssignmentTypeField.StoragePolicyAssignment) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal StoragePolicyAssignment(string id, StringEnum<StoragePolicyAssignmentTypeField> type) {
            Id = id;
            Type = StoragePolicyAssignmentTypeField.StoragePolicyAssignment;
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