using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ListUserV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isidSet")]
        protected bool _isIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isnameSet")]
        protected bool _isNameSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isemailSet")]
        protected bool _isEmailSet { get; set; }

        protected long? _id { get; set; }

        protected string _name { get; set; }

        protected string _email { get; set; }

        /// <summary>
        /// The ID of the user.
        /// </summary>
        [JsonPropertyName("id")]
        public long? Id { get => _id; set { _id = value; _isIdSet = true; } }

        /// <summary>
        /// The name of the user.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get => _name; set { _name = value; _isNameSet = true; } }

        /// <summary>
        /// The email of the user.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get => _email; set { _email = value; _isEmailSet = true; } }

        public ListUserV2025R0() {
            
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