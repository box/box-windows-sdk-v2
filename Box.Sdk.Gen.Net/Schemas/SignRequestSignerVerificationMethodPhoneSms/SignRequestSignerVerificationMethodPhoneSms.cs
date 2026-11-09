using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerVerificationMethodPhoneSms : ISerializable {
        /// <summary>
        /// The type of verification method.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerVerificationMethodPhoneSmsTypeField>))]
        public StringEnum<SignRequestSignerVerificationMethodPhoneSmsTypeField> Type { get; }

        /// <summary>
        /// The phone number to where the message will be sent.
        /// </summary>
        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; }

        public SignRequestSignerVerificationMethodPhoneSms(string phoneNumber, SignRequestSignerVerificationMethodPhoneSmsTypeField type = SignRequestSignerVerificationMethodPhoneSmsTypeField.Phone) {
            Type = type;
            PhoneNumber = phoneNumber;
        }
        
        [JsonConstructorAttribute]
        internal SignRequestSignerVerificationMethodPhoneSms(string phoneNumber, StringEnum<SignRequestSignerVerificationMethodPhoneSmsTypeField> type) {
            Type = SignRequestSignerVerificationMethodPhoneSmsTypeField.Phone;
            PhoneNumber = phoneNumber;
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