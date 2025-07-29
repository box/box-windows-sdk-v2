using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class RemoveSharedLinkFromFileRequestBody : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isshared_linkSet")]
        protected bool _isSharedLinkSet { get; set; }

        protected RemoveSharedLinkFromFileRequestBodySharedLinkField? _sharedLink { get; set; }

        /// <summary>
        /// By setting this value to `null`, the shared link
        /// is removed from the file.
        /// </summary>
        [JsonPropertyName("shared_link")]
        public RemoveSharedLinkFromFileRequestBodySharedLinkField? SharedLink { get => _sharedLink; init { _sharedLink = value; _isSharedLinkSet = true; } }

        public RemoveSharedLinkFromFileRequestBody() {
            
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