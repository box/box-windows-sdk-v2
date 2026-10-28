using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitCreateRequestManualV2026R0 : ISerializable {
        /// <summary>
        /// Discriminator. Must be `manual` for this variant.
        /// </summary>
        [JsonPropertyName("split_type")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitCreateRequestManualV2026R0SplitTypeField>))]
        public StringEnum<DocumentSplitCreateRequestManualV2026R0SplitTypeField> SplitType { get; }

        /// <summary>
        /// The version of the manual split input format. Omitted or `v_1` is the
        /// legacy `split_definition` format. Existing clients may omit this field.
        /// </summary>
        [JsonPropertyName("version")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitCreateRequestManualV2026R0VersionField>))]
        public StringEnum<DocumentSplitCreateRequestManualV2026R0VersionField>? Version { get; init; }

        /// <summary>
        /// Page ranges that define the split. Each entry produces one output file
        /// containing the inclusive page range `[start, end]`.
        /// </summary>
        [JsonPropertyName("split_definition")]
        public IReadOnlyList<DocumentSplitPageRangeV2026R0> SplitDefinition { get; }

        public DocumentSplitCreateRequestManualV2026R0(IReadOnlyList<DocumentSplitPageRangeV2026R0> splitDefinition, DocumentSplitCreateRequestManualV2026R0SplitTypeField splitType = DocumentSplitCreateRequestManualV2026R0SplitTypeField.Manual) {
            SplitType = splitType;
            SplitDefinition = splitDefinition;
        }
        
        [JsonConstructorAttribute]
        internal DocumentSplitCreateRequestManualV2026R0(IReadOnlyList<DocumentSplitPageRangeV2026R0> splitDefinition, StringEnum<DocumentSplitCreateRequestManualV2026R0SplitTypeField> splitType) {
            SplitType = DocumentSplitCreateRequestManualV2026R0SplitTypeField.Manual;
            SplitDefinition = splitDefinition;
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