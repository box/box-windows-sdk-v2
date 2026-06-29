using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSignerInput : SignRequestPrefillTag, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isreasonSet")]
        protected bool _isReasonSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isis_validatedSet")]
        protected bool _isIsValidatedSet { get; set; }

        protected string _reason { get; set; }

        protected bool? _isValidated { get; set; }

        /// <summary>
        /// Type of input.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputTypeField>))]
        public StringEnum<SignRequestSignerInputTypeField> Type { get; set; }

        /// <summary>
        /// Content type of input.
        /// </summary>
        [JsonPropertyName("content_type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestSignerInputContentTypeField>))]
        public StringEnum<SignRequestSignerInputContentTypeField> ContentType { get; set; }

        /// <summary>
        /// Index of page that the input is on.
        /// </summary>
        [JsonPropertyName("page_index")]
        public long PageIndex { get; set; }

        /// <summary>
        /// Indicates whether this input is read-only (cannot be modified by signers).
        /// </summary>
        [JsonPropertyName("read_only")]
        public bool? ReadOnly { get; set; }

        /// <summary>
        /// Specifies the formatting rules that signers must follow for text field inputs.
        /// If set, this validation is mandatory.
        /// </summary>
        [JsonPropertyName("validation")]
        public SignRequestSignerInputValidation Validation { get; set; }

        /// <summary>
        /// The reason for the signer's input, applicable to signature or initial content types
        /// in a `cfr11` request flow. The value is `null` when not applicable.
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get => _reason; set { _reason = value; _isReasonSet = true; } }

        /// <summary>
        /// Indicates whether the signer's input has been validated through re-authentication.
        /// Applicable only for signature or initial content types in a `cfr11` request flow.
        /// The value is `null` for standard request flows or non-applicable input types.
        /// </summary>
        [JsonPropertyName("is_validated")]
        public bool? IsValidated { get => _isValidated; set { _isValidated = value; _isIsValidatedSet = true; } }

        public SignRequestSignerInput(long pageIndex) {
            PageIndex = pageIndex;
        }
        internal new string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}