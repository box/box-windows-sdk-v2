using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerAttachment : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isidSet")]
        protected bool _isIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isnameSet")]
        protected bool _isNameSet { get; set; }

        protected string? _id { get; set; }

        protected string? _name { get; set; }

        /// <summary>
        /// Identifier of the attachment file.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get => _id; init { _id = value; _isIdSet = true; } }

        /// <summary>
        /// Display name of the attachment file.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get => _name; init { _name = value; _isNameSet = true; } }

        public SignRequestSignerAttachment() {
            
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