using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathEntryAccountScopeV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isidSet")]
        protected bool _isIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isemailSet")]
        protected bool _isEmailSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isnameSet")]
        protected bool _isNameSet { get; set; }

        protected string? _id { get; set; }

        protected string? _email { get; set; }

        protected string? _name { get; set; }

        /// <summary>
        /// The value will always be `account`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttlePathEntryAccountScopeV2026R0TypeField>))]
        public StringEnum<ShuttlePathEntryAccountScopeV2026R0TypeField> Type { get; }

        /// <summary>
        /// The platform-native identifier of the account that owns this entry.
        /// `null` when the system does not provide one. This is not the path
        /// entry `id`. Maps to `account.id` on an account-scoped path mapping
        /// location.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get => _id; init { _id = value; _isIdSet = true; } }

        /// <summary>
        /// The email of the account that owns this entry. This is a
        /// system-native identifier and is not always an RFC 5322 address.
        /// `null` when unknown. Maps to `account.email` on an account-scoped
        /// path mapping location.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get => _email; init { _email = value; _isEmailSet = true; } }

        /// <summary>
        /// The display name of the account that owns this entry. `null` when
        /// unknown. Maps to `account.name` on an account-scoped path mapping
        /// location.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get => _name; init { _name = value; _isNameSet = true; } }

        public ShuttlePathEntryAccountScopeV2026R0(ShuttlePathEntryAccountScopeV2026R0TypeField type = ShuttlePathEntryAccountScopeV2026R0TypeField.Account) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal ShuttlePathEntryAccountScopeV2026R0(StringEnum<ShuttlePathEntryAccountScopeV2026R0TypeField> type) {
            Type = ShuttlePathEntryAccountScopeV2026R0TypeField.Account;
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