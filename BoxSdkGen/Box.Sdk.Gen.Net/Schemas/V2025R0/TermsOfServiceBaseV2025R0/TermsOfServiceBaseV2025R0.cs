using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class TermsOfServiceBaseV2025R0 : ISerializable {
        /// <summary>
        /// The unique identifier for this terms of service.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `terms_of_service`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TermsOfServiceBaseV2025R0TypeField>))]
        public StringEnum<TermsOfServiceBaseV2025R0TypeField> Type { get; }

        public TermsOfServiceBaseV2025R0(string id, TermsOfServiceBaseV2025R0TypeField type = TermsOfServiceBaseV2025R0TypeField.TermsOfService) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal TermsOfServiceBaseV2025R0(string id, StringEnum<TermsOfServiceBaseV2025R0TypeField> type) {
            Id = id;
            Type = TermsOfServiceBaseV2025R0TypeField.TermsOfService;
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