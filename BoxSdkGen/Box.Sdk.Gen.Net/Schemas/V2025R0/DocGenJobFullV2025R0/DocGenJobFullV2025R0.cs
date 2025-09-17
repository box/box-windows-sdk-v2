using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenJobFullV2025R0 : DocGenJobV2025R0, ISerializable {
        /// <summary>
        /// Time of job creation.
        /// </summary>
        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; init; }

        [JsonPropertyName("created_by")]
        public UserBaseV2025R0 CreatedBy { get; }

        [JsonPropertyName("enterprise")]
        public EnterpriseReferenceV2025R0 Enterprise { get; }

        /// <summary>
        /// Source of the request.
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; }

        public DocGenJobFullV2025R0(string id, DocGenBatchBaseV2025R0 batch, FileReferenceV2025R0 templateFile, FileVersionBaseV2025R0 templateFileVersion, DocGenJobV2025R0StatusField status, string outputType, UserBaseV2025R0 createdBy, EnterpriseReferenceV2025R0 enterprise, string source, DocGenJobBaseV2025R0TypeField type = DocGenJobBaseV2025R0TypeField.DocgenJob) : base(id, batch, templateFile, templateFileVersion, status, outputType, type) {
            CreatedBy = createdBy;
            Enterprise = enterprise;
            Source = source;
        }
        
        [JsonConstructorAttribute]
        internal DocGenJobFullV2025R0(string id, DocGenBatchBaseV2025R0 batch, FileReferenceV2025R0 templateFile, FileVersionBaseV2025R0 templateFileVersion, StringEnum<DocGenJobV2025R0StatusField> status, string outputType, UserBaseV2025R0 createdBy, EnterpriseReferenceV2025R0 enterprise, string source, StringEnum<DocGenJobBaseV2025R0TypeField> type) : base(id, batch, templateFile, templateFileVersion, status, outputType, type ?? new StringEnum<DocGenJobBaseV2025R0TypeField>(DocGenJobBaseV2025R0TypeField.DocgenJob)) {
            CreatedBy = createdBy;
            Enterprise = enterprise;
            Source = source;
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