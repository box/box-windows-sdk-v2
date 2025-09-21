using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TemplateSignerInput : SignRequestPrefillTag, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdocument_idSet")]
        protected bool _isDocumentIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isdropdown_choicesSet")]
        protected bool _isDropdownChoicesSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isgroup_idSet")]
        protected bool _isGroupIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_islabelSet")]
        protected bool _isLabelSet { get; set; }

        protected string? _documentId { get; set; }

        protected IReadOnlyList<string>? _dropdownChoices { get; set; }

        protected string? _groupId { get; set; }

        protected string? _label { get; set; }

        /// <summary>
        /// Type of input.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TemplateSignerInputTypeField>))]
        public StringEnum<TemplateSignerInputTypeField>? Type { get; init; }

        /// <summary>
        /// Content type of input.
        /// </summary>
        [JsonPropertyName("content_type")]
        [JsonConverter(typeof(StringEnumConverter<TemplateSignerInputContentTypeField>))]
        public StringEnum<TemplateSignerInputContentTypeField>? ContentType { get; init; }

        /// <summary>
        /// Whether or not the input is required.
        /// </summary>
        [JsonPropertyName("is_required")]
        public bool? IsRequired { get; init; }

        /// <summary>
        /// Index of page that the input is on.
        /// </summary>
        [JsonPropertyName("page_index")]
        public long PageIndex { get; }

        /// <summary>
        /// Document identifier.
        /// </summary>
        [JsonPropertyName("document_id")]
        public string? DocumentId { get => _documentId; init { _documentId = value; _isDocumentIdSet = true; } }

        /// <summary>
        /// When the input is of the type `dropdown` this
        /// values will be filled with all the
        /// dropdown options.
        /// </summary>
        [JsonPropertyName("dropdown_choices")]
        public IReadOnlyList<string>? DropdownChoices { get => _dropdownChoices; init { _dropdownChoices = value; _isDropdownChoicesSet = true; } }

        /// <summary>
        /// When the input is of type `radio` they can be
        /// grouped to gather with this identifier.
        /// </summary>
        [JsonPropertyName("group_id")]
        public string? GroupId { get => _groupId; init { _groupId = value; _isGroupIdSet = true; } }

        /// <summary>
        /// Where the input is located on a page.
        /// </summary>
        [JsonPropertyName("coordinates")]
        public TemplateSignerInputCoordinatesField? Coordinates { get; init; }

        /// <summary>
        /// The size of the input.
        /// </summary>
        [JsonPropertyName("dimensions")]
        public TemplateSignerInputDimensionsField? Dimensions { get; init; }

        /// <summary>
        /// The label field is used especially for text, attachment, radio, and checkbox type inputs.
        /// </summary>
        [JsonPropertyName("label")]
        public string? Label { get => _label; init { _label = value; _isLabelSet = true; } }

        /// <summary>
        /// Whether this input was defined as read-only(immutable by signers) or not.
        /// </summary>
        [JsonPropertyName("read_only")]
        public bool? ReadOnly { get; init; }

        public TemplateSignerInput(long pageIndex) {
            PageIndex = pageIndex;
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}