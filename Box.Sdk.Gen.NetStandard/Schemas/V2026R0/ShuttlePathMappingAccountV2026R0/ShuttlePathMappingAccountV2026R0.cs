using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathMappingAccountV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isidSet")]
        protected bool _isIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isemailSet")]
        protected bool _isEmailSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isnameSet")]
        protected bool _isNameSet { get; set; }

        protected string _id { get; set; }

        protected string _email { get; set; }

        protected string _name { get; set; }

        /// <summary>
        /// The platform-native identifier of the account. `null` when the system
        /// does not provide one.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get => _id; set { _id = value; _isIdSet = true; } }

        /// <summary>
        /// The email address of the account. `null` when unknown.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get => _email; set { _email = value; _isEmailSet = true; } }

        /// <summary>
        /// The display name of the account. `null` when unknown.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get => _name; set { _name = value; _isNameSet = true; } }

        public ShuttlePathMappingAccountV2026R0() {
            
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