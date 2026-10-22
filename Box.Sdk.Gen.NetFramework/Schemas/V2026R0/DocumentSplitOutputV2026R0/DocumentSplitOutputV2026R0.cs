using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitOutputV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_issplit_definitionSet")]
        protected bool _isSplitDefinitionSet { get; set; }

        protected IReadOnlyList<DocumentSplitPageRangeV2026R0> _splitDefinition { get; set; }

        /// <summary>
        /// References to the files that were created by the split.
        /// </summary>
        [JsonPropertyName("generated_files")]
        public IReadOnlyList<FileReferenceV2026R0> GeneratedFiles { get; set; }

        /// <summary>
        /// The page ranges that Box produced for a `smart` split. Each entry
        /// corresponds to one generated file with the inclusive page range
        /// `[start, end]`. Omitted or `null` for `manual` splits, where the
        /// caller-supplied ranges are available in `split_input.split_definition`.
        /// Omitted for `page_selection` splits, where the caller-supplied page
        /// layout is available in `split_input.page_selection`.
        /// </summary>
        [JsonPropertyName("split_definition")]
        public IReadOnlyList<DocumentSplitPageRangeV2026R0> SplitDefinition { get => _splitDefinition; set { _splitDefinition = value; _isSplitDefinitionSet = true; } }

        public DocumentSplitOutputV2026R0() {
            
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}