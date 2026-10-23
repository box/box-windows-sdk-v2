using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SharedLinkOtpAllowlistEntryWritable : ISerializable {
        /// <summary>
        /// The kind of allowlist entry.
        /// 
        /// * `email` - The entry matches a single email address.
        /// * `domain` - The entry matches every email address under the
        ///   given domain.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<SharedLinkOtpAllowlistEntryWritableTypeField>))]
        public StringEnum<SharedLinkOtpAllowlistEntryWritableTypeField> Type { get; set; }

        /// <summary>
        /// The value of the allowlist entry.
        /// 
        /// * For `email` entries, the full email address (must contain `@`).
        /// * For `domain` entries, the bare domain (e.g. `example.com`).
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        public SharedLinkOtpAllowlistEntryWritable(SharedLinkOtpAllowlistEntryWritableTypeField type, string value) {
            Type = type;
            Value = value;
        }
        
        [JsonConstructorAttribute]
        internal SharedLinkOtpAllowlistEntryWritable(StringEnum<SharedLinkOtpAllowlistEntryWritableTypeField> type, string value) {
            Type = type;
            Value = value;
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