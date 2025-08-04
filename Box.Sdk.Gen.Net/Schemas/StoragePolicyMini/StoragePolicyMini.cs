using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class StoragePolicyMini : ISerializable {
        /// <summary>
        /// The unique identifier for this storage policy.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `storage_policy`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<StoragePolicyMiniTypeField>))]
        public StringEnum<StoragePolicyMiniTypeField> Type { get; }

        public StoragePolicyMini(string id, StoragePolicyMiniTypeField type = StoragePolicyMiniTypeField.StoragePolicy) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal StoragePolicyMini(string id, StringEnum<StoragePolicyMiniTypeField> type) {
            Id = id;
            Type = StoragePolicyMiniTypeField.StoragePolicy;
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