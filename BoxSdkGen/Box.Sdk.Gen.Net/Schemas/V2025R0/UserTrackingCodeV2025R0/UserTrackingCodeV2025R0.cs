using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class UserTrackingCodeV2025R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isidSet")]
        protected bool _isIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isnameSet")]
        protected bool _isNameSet { get; set; }

        protected long? _id { get; set; }

        protected string? _name { get; set; }

        /// <summary>
        /// The ID of the user tracking code.
        /// </summary>
        [JsonPropertyName("id")]
        public long? Id { get => _id; init { _id = value; _isIdSet = true; } }

        /// <summary>
        /// The name of the user tracking code.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get => _name; init { _name = value; _isNameSet = true; } }

        public UserTrackingCodeV2025R0() {
            
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