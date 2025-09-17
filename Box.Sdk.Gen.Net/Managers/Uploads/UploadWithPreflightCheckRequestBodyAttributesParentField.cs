using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UploadWithPreflightCheckRequestBodyAttributesParentField : ISerializable {
        /// <summary>
        /// The id of the parent folder. Use
        /// `0` for the user's root folder.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public UploadWithPreflightCheckRequestBodyAttributesParentField(string id) {
            Id = id;
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