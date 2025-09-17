using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateFileVersionByIdRequestBody : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_istrashed_atSet")]
        protected bool _isTrashedAtSet { get; set; }

        protected string? _trashedAt { get; set; }

        /// <summary>
        /// Set this to `null` to clear
        /// the date and restore the file.
        /// </summary>
        [JsonPropertyName("trashed_at")]
        public string? TrashedAt { get => _trashedAt; init { _trashedAt = value; _isTrashedAtSet = true; } }

        public UpdateFileVersionByIdRequestBody() {
            
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