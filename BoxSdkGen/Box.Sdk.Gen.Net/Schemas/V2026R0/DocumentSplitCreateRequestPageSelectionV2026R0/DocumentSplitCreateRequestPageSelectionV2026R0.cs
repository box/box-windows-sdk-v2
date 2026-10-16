using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitCreateRequestPageSelectionV2026R0 : ISerializable {
        /// <summary>
        /// Discriminator. Must be `page_selection` for this variant.
        /// </summary>
        [JsonPropertyName("split_type")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitCreateRequestPageSelectionV2026R0SplitTypeField>))]
        public StringEnum<DocumentSplitCreateRequestPageSelectionV2026R0SplitTypeField> SplitType { get; }

        /// <summary>
        /// The version of the page selection input format. Optional. When provided
        /// must be `v_2`.
        /// </summary>
        [JsonPropertyName("version")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitCreateRequestPageSelectionV2026R0VersionField>))]
        public StringEnum<DocumentSplitCreateRequestPageSelectionV2026R0VersionField>? Version { get; init; }

        [JsonPropertyName("page_selection")]
        public IReadOnlyList<DocumentSplitPageSelectionItemV2026R0> PageSelection { get; }

        public DocumentSplitCreateRequestPageSelectionV2026R0(IReadOnlyList<DocumentSplitPageSelectionItemV2026R0> pageSelection, DocumentSplitCreateRequestPageSelectionV2026R0SplitTypeField splitType = DocumentSplitCreateRequestPageSelectionV2026R0SplitTypeField.PageSelection) {
            SplitType = splitType;
            PageSelection = pageSelection;
        }
        
        [JsonConstructorAttribute]
        internal DocumentSplitCreateRequestPageSelectionV2026R0(IReadOnlyList<DocumentSplitPageSelectionItemV2026R0> pageSelection, StringEnum<DocumentSplitCreateRequestPageSelectionV2026R0SplitTypeField> splitType) {
            SplitType = DocumentSplitCreateRequestPageSelectionV2026R0SplitTypeField.PageSelection;
            PageSelection = pageSelection;
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