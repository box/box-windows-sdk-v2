using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class LegalHoldPolicyAssignedItem : ISerializable {
        /// <summary>
        /// The type of item the policy is assigned to.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<LegalHoldPolicyAssignedItemTypeField>))]
        public StringEnum<LegalHoldPolicyAssignedItemTypeField> Type { get; }

        /// <summary>
        /// The ID of the item the policy is assigned to.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public LegalHoldPolicyAssignedItem(LegalHoldPolicyAssignedItemTypeField type, string id) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal LegalHoldPolicyAssignedItem(StringEnum<LegalHoldPolicyAssignedItemTypeField> type, string id) {
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