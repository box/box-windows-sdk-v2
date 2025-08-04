using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenJobV2025R0 : DocGenJobBaseV2025R0, ISerializable {
        [JsonPropertyName("batch")]
        public DocGenBatchBaseV2025R0 Batch { get; }

        [JsonPropertyName("template_file")]
        public FileReferenceV2025R0 TemplateFile { get; }

        [JsonPropertyName("template_file_version")]
        public FileVersionBaseV2025R0 TemplateFileVersion { get; }

        [JsonPropertyName("output_file")]
        public FileReferenceV2025R0? OutputFile { get; init; }

        [JsonPropertyName("output_file_version")]
        public FileVersionBaseV2025R0? OutputFileVersion { get; init; }

        /// <summary>
        /// Status of the job.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<DocGenJobV2025R0StatusField>))]
        public StringEnum<DocGenJobV2025R0StatusField> Status { get; }

        /// <summary>
        /// Type of the generated file.
        /// </summary>
        [JsonPropertyName("output_type")]
        public string OutputType { get; }

        public DocGenJobV2025R0(string id, DocGenBatchBaseV2025R0 batch, FileReferenceV2025R0 templateFile, FileVersionBaseV2025R0 templateFileVersion, DocGenJobV2025R0StatusField status, string outputType, DocGenJobBaseV2025R0TypeField type = DocGenJobBaseV2025R0TypeField.DocgenJob) : base(id, type) {
            Batch = batch;
            TemplateFile = templateFile;
            TemplateFileVersion = templateFileVersion;
            Status = status;
            OutputType = outputType;
        }
        
        [JsonConstructorAttribute]
        internal DocGenJobV2025R0(string id, DocGenBatchBaseV2025R0 batch, FileReferenceV2025R0 templateFile, FileVersionBaseV2025R0 templateFileVersion, StringEnum<DocGenJobV2025R0StatusField> status, string outputType, StringEnum<DocGenJobBaseV2025R0TypeField> type) : base(id, type ?? new StringEnum<DocGenJobBaseV2025R0TypeField>(DocGenJobBaseV2025R0TypeField.DocgenJob)) {
            Batch = batch;
            TemplateFile = templateFile;
            TemplateFileVersion = templateFileVersion;
            Status = status;
            OutputType = outputType;
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