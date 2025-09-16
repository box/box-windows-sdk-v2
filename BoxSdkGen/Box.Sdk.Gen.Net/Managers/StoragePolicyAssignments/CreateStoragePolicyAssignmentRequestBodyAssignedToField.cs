using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateStoragePolicyAssignmentRequestBodyAssignedToField : ISerializable {
        /// <summary>
        /// The type to assign the policy to.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CreateStoragePolicyAssignmentRequestBodyAssignedToTypeField>))]
        public StringEnum<CreateStoragePolicyAssignmentRequestBodyAssignedToTypeField> Type { get; }

        /// <summary>
        /// The ID of the user or enterprise.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public CreateStoragePolicyAssignmentRequestBodyAssignedToField(CreateStoragePolicyAssignmentRequestBodyAssignedToTypeField type, string id) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal CreateStoragePolicyAssignmentRequestBodyAssignedToField(StringEnum<CreateStoragePolicyAssignmentRequestBodyAssignedToTypeField> type, string id) {
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