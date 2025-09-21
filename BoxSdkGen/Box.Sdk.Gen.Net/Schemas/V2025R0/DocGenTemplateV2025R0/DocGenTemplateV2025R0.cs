using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class DocGenTemplateV2025R0 : DocGenTemplateBaseV2025R0, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isfile_nameSet")]
        protected bool _isFileNameSet { get; set; }

        protected string? _fileName { get; set; }

        /// <summary>
        /// The name of the template.
        /// </summary>
        [JsonPropertyName("file_name")]
        public string? FileName { get => _fileName; init { _fileName = value; _isFileNameSet = true; } }

        public DocGenTemplateV2025R0() {
            
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