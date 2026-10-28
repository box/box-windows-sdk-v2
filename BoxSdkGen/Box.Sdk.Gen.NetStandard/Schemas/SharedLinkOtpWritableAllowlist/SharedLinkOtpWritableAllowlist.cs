using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SharedLinkOtpWritableAllowlist : ISerializable {
        /// <summary>
        /// Set to `allowlist` to restrict OTP requests to emails matching an
        /// entry on the `allowlist`.
        /// </summary>
        [JsonPropertyName("mode")]
        [JsonConverter(typeof(StringEnumConverter<SharedLinkOtpWritableAllowlistModeField>))]
        public StringEnum<SharedLinkOtpWritableAllowlistModeField> Mode { get; set; }

        /// <summary>
        /// The list of email and domain entries that are permitted to access the
        /// shared link when `mode` is `allowlist`. Anonymous users whose
        /// email matches an entry can request and verify a one-time-passcode.
        /// 
        /// Providing this field replaces the entire existing allowlist; passing
        /// an empty array clears all entries. Required when `mode` is `allowlist`.
        /// </summary>
        [JsonPropertyName("allowlist")]
        public IReadOnlyList<SharedLinkOtpAllowlistEntryWritable> Allowlist { get; set; }

        public SharedLinkOtpWritableAllowlist(IReadOnlyList<SharedLinkOtpAllowlistEntryWritable> allowlist, SharedLinkOtpWritableAllowlistModeField mode = SharedLinkOtpWritableAllowlistModeField.Allowlist) {
            Mode = mode;
            Allowlist = allowlist;
        }
        
        [JsonConstructorAttribute]
        internal SharedLinkOtpWritableAllowlist(IReadOnlyList<SharedLinkOtpAllowlistEntryWritable> allowlist, StringEnum<SharedLinkOtpWritableAllowlistModeField> mode) {
            Mode = SharedLinkOtpWritableAllowlistModeField.Allowlist;
            Allowlist = allowlist;
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