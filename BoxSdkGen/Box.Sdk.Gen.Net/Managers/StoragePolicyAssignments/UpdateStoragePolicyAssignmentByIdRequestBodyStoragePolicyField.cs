using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateStoragePolicyAssignmentByIdRequestBodyStoragePolicyField : ISerializable {
        /// <summary>
        /// The type to assign.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<UpdateStoragePolicyAssignmentByIdRequestBodyStoragePolicyTypeField>))]
        public StringEnum<UpdateStoragePolicyAssignmentByIdRequestBodyStoragePolicyTypeField> Type { get; }

        /// <summary>
        /// The ID of the storage policy to assign.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public UpdateStoragePolicyAssignmentByIdRequestBodyStoragePolicyField(string id, UpdateStoragePolicyAssignmentByIdRequestBodyStoragePolicyTypeField type = UpdateStoragePolicyAssignmentByIdRequestBodyStoragePolicyTypeField.StoragePolicy) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal UpdateStoragePolicyAssignmentByIdRequestBodyStoragePolicyField(string id, StringEnum<UpdateStoragePolicyAssignmentByIdRequestBodyStoragePolicyTypeField> type) {
            Type = UpdateStoragePolicyAssignmentByIdRequestBodyStoragePolicyTypeField.StoragePolicy;
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