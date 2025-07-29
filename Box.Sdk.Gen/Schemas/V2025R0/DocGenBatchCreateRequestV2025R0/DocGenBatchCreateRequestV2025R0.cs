using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenBatchCreateRequestV2025R0 : ISerializable {
        [JsonPropertyName("file")]
        public FileReferenceV2025R0 File { get; }

        [JsonPropertyName("file_version")]
        public FileVersionBaseV2025R0? FileVersion { get; init; }

        /// <summary>
        /// Source of input. The value has to be `api` for all the API-based document generation requests.
        /// </summary>
        [JsonPropertyName("input_source")]
        public string InputSource { get; }

        [JsonPropertyName("destination_folder")]
        public DocGenBatchCreateRequestV2025R0DestinationFolderField DestinationFolder { get; }

        /// <summary>
        /// Type of the output file.
        /// </summary>
        [JsonPropertyName("output_type")]
        public string OutputType { get; }

        [JsonPropertyName("document_generation_data")]
        public IReadOnlyList<DocGenDocumentGenerationDataV2025R0> DocumentGenerationData { get; }

        public DocGenBatchCreateRequestV2025R0(FileReferenceV2025R0 file, string inputSource, DocGenBatchCreateRequestV2025R0DestinationFolderField destinationFolder, string outputType, IReadOnlyList<DocGenDocumentGenerationDataV2025R0> documentGenerationData) {
            File = file;
            InputSource = inputSource;
            DestinationFolder = destinationFolder;
            OutputType = outputType;
            DocumentGenerationData = documentGenerationData;
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