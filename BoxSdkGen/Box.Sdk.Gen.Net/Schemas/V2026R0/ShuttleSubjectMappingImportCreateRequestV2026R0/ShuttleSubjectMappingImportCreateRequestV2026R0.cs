using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleSubjectMappingImportCreateRequestV2026R0 : ISerializable {
        /// <summary>
        /// The unique identifier of the shuttle job whose subject mappings are
        /// replaced.
        /// </summary>
        [JsonPropertyName("job_id")]
        public string JobId { get; }

        /// <summary>
        /// The unique identifier of the Box file holding the mapping workbook. The
        /// file is read as the calling user, so it must live somewhere that user can
        /// download from.
        /// </summary>
        [JsonPropertyName("file_id")]
        public string FileId { get; }

        public ShuttleSubjectMappingImportCreateRequestV2026R0(string jobId, string fileId) {
            JobId = jobId;
            FileId = fileId;
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