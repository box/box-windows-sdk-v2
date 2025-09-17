using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenTemplateCreateRequestV2025R0 : ISerializable {
        [JsonPropertyName("file")]
        public FileReferenceV2025R0 File { get; set; }

        public DocGenTemplateCreateRequestV2025R0(FileReferenceV2025R0 file) {
            File = file;
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