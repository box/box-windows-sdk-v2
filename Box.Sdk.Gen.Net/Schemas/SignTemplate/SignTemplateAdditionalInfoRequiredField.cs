using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SignTemplateAdditionalInfoRequiredField : ISerializable {
        /// <summary>
        /// Required signer fields.
        /// </summary>
        [JsonPropertyName("signers")]
        [JsonConverter(typeof(StringEnumNestedListConverter<SignTemplateAdditionalInfoRequiredSignersField>))]
        public IReadOnlyList<IReadOnlyList<StringEnum<SignTemplateAdditionalInfoRequiredSignersField>>>? Signers { get; init; }

        public SignTemplateAdditionalInfoRequiredField() {
            
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