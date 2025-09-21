using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class PreflightFileUploadCheckRequestBody : ISerializable {
        /// <summary>
        /// The name for the file.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The size of the file in bytes.
        /// </summary>
        [JsonPropertyName("size")]
        public int? Size { get; init; }

        [JsonPropertyName("parent")]
        public PreflightFileUploadCheckRequestBodyParentField? Parent { get; init; }

        public PreflightFileUploadCheckRequestBody() {
            
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