using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathMappingAccountLocationInputV2026R0 : ISerializable {
        /// <summary>
        /// The value will always be `account`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttlePathMappingAccountLocationInputV2026R0TypeField>))]
        public StringEnum<ShuttlePathMappingAccountLocationInputV2026R0TypeField> Type { get; set; }

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

        /// <summary>
        /// Whether the location is a directory. When set, the destination must be
        /// the same kind of object.
        /// </summary>
        [JsonPropertyName("is_directory")]
        public bool? IsDirectory { get; set; }

        public ShuttlePathMappingAccountLocationInputV2026R0(string path, ShuttlePathMappingAccountInputV2026R0 account, ShuttlePathMappingAccountLocationInputV2026R0TypeField type = ShuttlePathMappingAccountLocationInputV2026R0TypeField.Account) {
            Type = type;
            Path = path;
            Account = account;
        }
        
        [JsonConstructorAttribute]
        internal ShuttlePathMappingAccountLocationInputV2026R0(string path, ShuttlePathMappingAccountInputV2026R0 account, StringEnum<ShuttlePathMappingAccountLocationInputV2026R0TypeField> type) {
            Type = ShuttlePathMappingAccountLocationInputV2026R0TypeField.Account;
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