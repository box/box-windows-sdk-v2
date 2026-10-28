using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleTargetSubjectV2026R0 : ISerializable {
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
        /// Whether the Box target is a user or a group.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleTargetSubjectV2026R0TypeField>))]
        public StringEnum<ShuttleTargetSubjectV2026R0TypeField> Type { get; }

        /// <summary>
        /// The Box user or group ID. `null` when the mapping was not created
        /// through this API and is identified by email (user) or name (group)
        /// instead. Skip is represented by `target: null`, not a missing `id`.
        /// Create and update always send `id`.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get => _id; init { _id = value; _isIdSet = true; } }

        /// <summary>
        /// The email address of the target. `null` when unknown.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get => _email; init { _email = value; _isEmailSet = true; } }

        /// <summary>
        /// The display name of the target. `null` when unknown.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get => _name; init { _name = value; _isNameSet = true; } }

        public ShuttleTargetSubjectV2026R0(ShuttleTargetSubjectV2026R0TypeField type) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleTargetSubjectV2026R0(StringEnum<ShuttleTargetSubjectV2026R0TypeField> type) {
            Type = type;
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