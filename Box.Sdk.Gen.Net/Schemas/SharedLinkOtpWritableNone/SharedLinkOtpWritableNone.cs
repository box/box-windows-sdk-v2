using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SharedLinkOtpWritableNone : ISerializable {
        /// <summary>
        /// Set to `none` to disable OTP enforcement for the shared link.
        /// </summary>
        [JsonPropertyName("mode")]
        [JsonConverter(typeof(StringEnumConverter<SharedLinkOtpWritableNoneModeField>))]
        public StringEnum<SharedLinkOtpWritableNoneModeField> Mode { get; }

        public SharedLinkOtpWritableNone(SharedLinkOtpWritableNoneModeField mode = SharedLinkOtpWritableNoneModeField.None) {
            Mode = mode;
        }
        
        [JsonConstructorAttribute]
        internal SharedLinkOtpWritableNone(StringEnum<SharedLinkOtpWritableNoneModeField> mode) {
            Mode = SharedLinkOtpWritableNoneModeField.None;
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