using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class UploadSessionSessionEndpointsField : ISerializable {
        /// <summary>
        /// The URL to upload parts to.
        /// </summary>
        [JsonPropertyName("upload_part")]
        public string? UploadPart { get; init; }

        /// <summary>
        /// The URL used to commit the file.
        /// </summary>
        [JsonPropertyName("commit")]
        public string? Commit { get; init; }

        /// <summary>
        /// The URL for used to abort the session.
        /// </summary>
        [JsonPropertyName("abort")]
        public string? Abort { get; init; }

        /// <summary>
        /// The URL users to list all parts.
        /// </summary>
        [JsonPropertyName("list_parts")]
        public string? ListParts { get; init; }

        /// <summary>
        /// The URL used to get the status of the upload.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; init; }

        /// <summary>
        /// The URL used to get the upload log from.
        /// </summary>
        [JsonPropertyName("log_event")]
        public string? LogEvent { get; init; }

        public UploadSessionSessionEndpointsField() {
            
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