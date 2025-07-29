using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestPrefillTag : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdocument_tag_idSet")]
        protected bool _isDocumentTagIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_istext_valueSet")]
        protected bool _isTextValueSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_ischeckbox_valueSet")]
        protected bool _isCheckboxValueSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isdate_valueSet")]
        protected bool _isDateValueSet { get; set; }

        protected string? _documentTagId { get; set; }

        protected string? _textValue { get; set; }

        protected bool? _checkboxValue { get; set; }

        protected System.DateOnly? _dateValue { get; set; }

        /// <summary>
        /// This references the ID of a specific tag contained in a file of the signature request.
        /// </summary>
        [JsonPropertyName("document_tag_id")]
        public string? DocumentTagId { get => _documentTagId; init { _documentTagId = value; _isDocumentTagIdSet = true; } }

        /// <summary>
        /// Text prefill value.
        /// </summary>
        [JsonPropertyName("text_value")]
        public string? TextValue { get => _textValue; init { _textValue = value; _isTextValueSet = true; } }

        /// <summary>
        /// Checkbox prefill value.
        /// </summary>
        [JsonPropertyName("checkbox_value")]
        public bool? CheckboxValue { get => _checkboxValue; init { _checkboxValue = value; _isCheckboxValueSet = true; } }

        /// <summary>
        /// Date prefill value.
        /// </summary>
        [JsonPropertyName("date_value")]
        public System.DateOnly? DateValue { get => _dateValue; init { _dateValue = value; _isDateValueSet = true; } }

        public SignRequestPrefillTag() {
            
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