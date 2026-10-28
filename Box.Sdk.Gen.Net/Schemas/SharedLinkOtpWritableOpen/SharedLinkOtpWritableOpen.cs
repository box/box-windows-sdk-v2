using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SharedLinkOtpWritableOpen : ISerializable {
        /// <summary>
        /// Set to `open` to allow any anonymous recipient to request a
        /// one-time-passcode to any email address.
        /// </summary>
        [JsonPropertyName("mode")]
        [JsonConverter(typeof(StringEnumConverter<SharedLinkOtpWritableOpenModeField>))]
        public StringEnum<SharedLinkOtpWritableOpenModeField> Mode { get; }

        public SharedLinkOtpWritableOpen(SharedLinkOtpWritableOpenModeField mode = SharedLinkOtpWritableOpenModeField.Open) {
            Mode = mode;
        }
        
        [JsonConstructorAttribute]
        internal SharedLinkOtpWritableOpen(StringEnum<SharedLinkOtpWritableOpenModeField> mode) {
            Mode = SharedLinkOtpWritableOpenModeField.Open;
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