using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class TermsOfServiceBase : ISerializable {
        /// <summary>
        /// The unique identifier for this terms of service.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The value will always be `terms_of_service`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TermsOfServiceBaseTypeField>))]
        public StringEnum<TermsOfServiceBaseTypeField> Type { get; }

        public TermsOfServiceBase(string id, TermsOfServiceBaseTypeField type = TermsOfServiceBaseTypeField.TermsOfService) {
            Id = id;
            Type = type;
        }
        
        [JsonConstructorAttribute]
        internal TermsOfServiceBase(string id, StringEnum<TermsOfServiceBaseTypeField> type) {
            Id = id;
            Type = TermsOfServiceBaseTypeField.TermsOfService;
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