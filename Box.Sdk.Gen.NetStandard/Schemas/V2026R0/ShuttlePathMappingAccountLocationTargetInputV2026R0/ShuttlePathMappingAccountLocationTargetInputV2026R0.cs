using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathMappingAccountLocationTargetInputV2026R0 : ISerializable {
        /// <summary>
        /// The value will always be `account`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttlePathMappingAccountLocationTargetInputV2026R0TypeField>))]
        public StringEnum<ShuttlePathMappingAccountLocationTargetInputV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The human-readable path relative to the account root.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }

        /// <summary>
        /// An optional stable identifier of the location, when obtained from
        /// browsing.
        /// </summary>
        [JsonPropertyName("path_id")]
        public string PathId { get; set; }

        [JsonPropertyName("account")]
        public ShuttlePathMappingAccountInputV2026R0 Account { get; set; }

        public ShuttlePathMappingAccountLocationTargetInputV2026R0(string path, ShuttlePathMappingAccountInputV2026R0 account, ShuttlePathMappingAccountLocationTargetInputV2026R0TypeField type = ShuttlePathMappingAccountLocationTargetInputV2026R0TypeField.Account) {
            Type = type;
            Path = path;
            Account = account;
        }
        
        [JsonConstructorAttribute]
        internal ShuttlePathMappingAccountLocationTargetInputV2026R0(string path, ShuttlePathMappingAccountInputV2026R0 account, StringEnum<ShuttlePathMappingAccountLocationTargetInputV2026R0TypeField> type) {
            Type = ShuttlePathMappingAccountLocationTargetInputV2026R0TypeField.Account;
            Path = path;
            Account = account;
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}