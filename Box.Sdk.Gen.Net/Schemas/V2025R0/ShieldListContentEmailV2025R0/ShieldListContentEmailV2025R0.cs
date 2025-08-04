using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShieldListContentEmailV2025R0 : ISerializable {
        /// <summary>
        /// The type of content in the shield list.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShieldListContentEmailV2025R0TypeField>))]
        public StringEnum<ShieldListContentEmailV2025R0TypeField> Type { get; }

        /// <summary>
        /// List of emails.
        /// </summary>
        [JsonPropertyName("email_addresses")]
        public IReadOnlyList<string> EmailAddresses { get; }

        public ShieldListContentEmailV2025R0(IReadOnlyList<string> emailAddresses, ShieldListContentEmailV2025R0TypeField type = ShieldListContentEmailV2025R0TypeField.Email) {
            Type = type;
            EmailAddresses = emailAddresses;
        }
        
        [JsonConstructorAttribute]
        internal ShieldListContentEmailV2025R0(IReadOnlyList<string> emailAddresses, StringEnum<ShieldListContentEmailV2025R0TypeField> type) {
            Type = ShieldListContentEmailV2025R0TypeField.Email;
            EmailAddresses = emailAddresses;
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