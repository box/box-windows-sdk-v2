using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SignTemplateAdditionalInfoField : ISerializable {
        /// <summary>
        /// Non editable fields.
        /// </summary>
        [JsonPropertyName("non_editable")]
        [JsonConverter(typeof(StringEnumListConverter<SignTemplateAdditionalInfoNonEditableField>))]
        public IReadOnlyList<StringEnum<SignTemplateAdditionalInfoNonEditableField>>? NonEditable { get; init; }

        /// <summary>
        /// Required fields.
        /// </summary>
        [JsonPropertyName("required")]
        public SignTemplateAdditionalInfoRequiredField? Required { get; init; }

        public SignTemplateAdditionalInfoField() {
            
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