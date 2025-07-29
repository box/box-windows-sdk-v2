using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Dictionary<string, object> UserInput { get; }

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