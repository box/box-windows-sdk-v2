using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SharedLinkOtpAllowlistEntry : ISerializable {
        /// <summary>
        /// The kind of allowlist entry.
        /// 
        /// * `email` - The entry matches a single email address.
        /// * `domain` - The entry matches every email address under the
        ///   given domain.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<SharedLinkOtpAllowlistEntryTypeField>))]
        public StringEnum<SharedLinkOtpAllowlistEntryTypeField> Type { get; }

        /// <summary>
        /// The value of the allowlist entry.
        /// 
        /// * For `email` entries, the full email address (must contain `@`).
        /// * For `domain` entries, the bare domain (e.g. `example.com`).
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; }

        /// <summary>
        /// The date and time when the allowlist entry was added.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset CreatedAt { get; }

        [JsonPropertyName("created_by")]
        public UserBase CreatedBy { get; }

        public SharedLinkOtpAllowlistEntry(SharedLinkOtpAllowlistEntryTypeField type, string value, System.DateTimeOffset createdAt, UserBase createdBy) {
            Type = type;
            Value = value;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }
        
        [JsonConstructorAttribute]
        internal SharedLinkOtpAllowlistEntry(StringEnum<SharedLinkOtpAllowlistEntryTypeField> type, string value, System.DateTimeOffset createdAt, UserBase createdBy) {
            Type = type;
            Value = value;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
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