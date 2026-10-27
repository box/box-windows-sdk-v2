using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier for the Document Split job.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `document_split`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitV2026R0TypeField>))]
        public StringEnum<DocumentSplitV2026R0TypeField> Type { get; set; }

        [JsonPropertyName("source_file")]
        public FileReferenceV2026R0 SourceFile { get; set; }

        /// <summary>
        /// The current state of the Document Split job.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitV2026R0StatusField>))]
        public StringEnum<DocumentSplitV2026R0StatusField> Status { get; set; }

        [JsonPropertyName("split_input")]
        public DocumentSplitCreateRequestTypeV2026R0 SplitInput { get; set; }

        /// <summary>
        /// The result of the Document Split job. `null` until the job has produced
        /// output.
        /// </summary>
        [JsonPropertyName("output")]
        public DocumentSplitOutputV2026R0 Output { get; set; }

        [JsonPropertyName("destination_folder")]
        public FolderReferenceV2026R0 DestinationFolder { get; set; }

        /// <summary>
        /// The system that initiated the request, used for analytics and
        /// traceability.
        /// </summary>
        [JsonPropertyName("input_source")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitV2026R0InputSourceField>))]
        public StringEnum<DocumentSplitV2026R0InputSourceField> InputSource { get; set; }

        /// <summary>
        /// When the Document Split job was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        [JsonPropertyName("created_by")]
        public UserMiniV2026R0 CreatedBy { get; set; }

        [JsonPropertyName("enterprise")]
        public EnterpriseReferenceV2026R0 Enterprise { get; set; }

        /// <summary>
        /// Errors and warnings that occurred during the document split. `null` when
        /// no failures were recorded.
        /// </summary>
        [JsonPropertyName("failures")]
        public DocumentSplitFailuresV2026R0 Failures { get; set; }

        /// <summary>
        /// An optional identifier supplied by the caller when the Document Split
        /// job was created. Returned for correlation only. It is not a Box
        /// resource ID, is not required to be unique, and cannot be used to
        /// look up the job.
        /// </summary>
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        public DocumentSplitV2026R0(string id, FileReferenceV2026R0 sourceFile, DocumentSplitV2026R0StatusField status, DocumentSplitCreateRequestTypeV2026R0 splitInput, FolderReferenceV2026R0 destinationFolder, DocumentSplitV2026R0InputSourceField inputSource, DocumentSplitV2026R0TypeField type = DocumentSplitV2026R0TypeField.DocumentSplit) {
            Id = id;
            Type = type;
            SourceFile = sourceFile;
            Status = status;
            SplitInput = splitInput;
            DestinationFolder = destinationFolder;
            InputSource = inputSource;
        }
        
        [JsonConstructorAttribute]
        internal DocumentSplitV2026R0(string id, FileReferenceV2026R0 sourceFile, StringEnum<DocumentSplitV2026R0StatusField> status, DocumentSplitCreateRequestTypeV2026R0 splitInput, FolderReferenceV2026R0 destinationFolder, StringEnum<DocumentSplitV2026R0InputSourceField> inputSource, StringEnum<DocumentSplitV2026R0TypeField> type) {
            Id = id;
            Type = DocumentSplitV2026R0TypeField.DocumentSplit;
            SourceFile = sourceFile;
            Status = status;
            SplitInput = splitInput;
            DestinationFolder = destinationFolder;
            InputSource = inputSource;
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