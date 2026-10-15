using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenDocumentGenerationDataV2025R0 : ISerializable {
        /// <summary>
        /// File name of the output file.
        /// </summary>
        [JsonPropertyName("generated_file_name")]
        public string GeneratedFileName { get; }

        [JsonPropertyName("user_input")]
        [JsonConverter(typeof(DictionaryObjectValuesConverter))]
        public Dictionary<string, object> UserInput { get; }

        /// <summary>
        /// An optional identifier supplied by the caller for this Box Doc Gen job.
        /// The value is returned on job responses for correlation only. It is not
        /// a Box resource ID, is not required to be unique, and cannot be used to
        /// look up the job.
        /// </summary>
        [JsonPropertyName("external_id")]
        public string? ExternalId { get; init; }

        public DocGenDocumentGenerationDataV2025R0(string generatedFileName, Dictionary<string, object> userInput) {
            GeneratedFileName = generatedFileName;
            UserInput = userInput;
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