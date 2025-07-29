using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class StoragePolicy : StoragePolicyMini, ISerializable {
        /// <summary>
        /// A descriptive name of the region.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        public StoragePolicy(string id, StoragePolicyMiniTypeField type = StoragePolicyMiniTypeField.StoragePolicy) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal StoragePolicy(string id, StringEnum<StoragePolicyMiniTypeField> type) : base(id, type ?? new StringEnum<StoragePolicyMiniTypeField>(StoragePolicyMiniTypeField.StoragePolicy)) {
            
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}