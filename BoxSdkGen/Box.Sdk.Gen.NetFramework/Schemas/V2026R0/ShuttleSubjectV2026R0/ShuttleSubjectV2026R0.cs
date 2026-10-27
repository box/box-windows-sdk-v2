using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleSubjectV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isidSet")]
        protected bool _isIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isemailSet")]
        protected bool _isEmailSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isnameSet")]
        protected bool _isNameSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isrolesSet")]
        protected bool _isRolesSet { get; set; }

        protected string _id { get; set; }

        protected string _email { get; set; }

        protected string _name { get; set; }

        protected IReadOnlyList<StringEnum<ShuttleSubjectRoleV2026R0>> _roles { get; set; }

        /// <summary>
        /// Whether this subject is a user or a group.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleSubjectV2026R0TypeField>))]
        public StringEnum<ShuttleSubjectV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The platform-native identifier of the subject. `null` when the system
        /// does not provide one.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get => _id; set { _id = value; _isIdSet = true; } }

        /// <summary>
        /// The email address of the subject. `null` when unknown.
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get => _email; set { _email = value; _isEmailSet = true; } }

        /// <summary>
        /// The display name of the subject. `null` when unknown.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get => _name; set { _name = value; _isNameSet = true; } }

        /// <summary>
        /// Roles this subject holds on the source. `null` when no roles are known.
        /// </summary>
        [JsonPropertyName("roles")]
        [JsonConverter(typeof(StringEnumListConverter<ShuttleSubjectRoleV2026R0>))]
        public IReadOnlyList<StringEnum<ShuttleSubjectRoleV2026R0>> Roles { get => _roles; set { _roles = value; _isRolesSet = true; } }

        public ShuttleSubjectV2026R0(ShuttleSubjectV2026R0TypeField type) {
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleSubjectV2026R0(StringEnum<ShuttleSubjectV2026R0TypeField> type) {
            Type = type;
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