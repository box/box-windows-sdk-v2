using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocumentSplitCreateRequestV2026R0 : ISerializable {
        /// <summary>
        /// The ID of the source file to split.
        /// </summary>
        [JsonPropertyName("source_file_id")]
        public string SourceFileId { get; set; }

        /// <summary>
        /// The ID of the folder where the resulting files will be uploaded.
        /// </summary>
        [JsonPropertyName("destination_folder_id")]
        public string DestinationFolderId { get; set; }

        /// <summary>
        /// The system that initiated the request, used for analytics and
        /// traceability.
        /// </summary>
        [JsonPropertyName("input_source")]
        [JsonConverter(typeof(StringEnumConverter<DocumentSplitCreateRequestV2026R0InputSourceField>))]
        public StringEnum<DocumentSplitCreateRequestV2026R0InputSourceField> InputSource { get; set; }

        [JsonPropertyName("split_input")]
        public DocumentSplitCreateRequestTypeV2026R0 SplitInput { get; set; }

        /// <summary>
        /// An optional identifier supplied by the caller for this Document Split job.
        /// The value is returned on job responses for correlation only. It is not
        /// a Box resource ID, is not required to be unique, and cannot be used to
        /// look up the job.
        /// </summary>
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        public DocumentSplitCreateRequestV2026R0(string sourceFileId, string destinationFolderId, DocumentSplitCreateRequestV2026R0InputSourceField inputSource, DocumentSplitCreateRequestTypeV2026R0 splitInput) {
            SourceFileId = sourceFileId;
            DestinationFolderId = destinationFolderId;
            InputSource = inputSource;
            SplitInput = splitInput;
        }
        
        [JsonConstructorAttribute]
        internal DocumentSplitCreateRequestV2026R0(string sourceFileId, string destinationFolderId, StringEnum<DocumentSplitCreateRequestV2026R0InputSourceField> inputSource, DocumentSplitCreateRequestTypeV2026R0 splitInput) {
            SourceFileId = sourceFileId;
            DestinationFolderId = destinationFolderId;
            InputSource = inputSource;
            SplitInput = splitInput;
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