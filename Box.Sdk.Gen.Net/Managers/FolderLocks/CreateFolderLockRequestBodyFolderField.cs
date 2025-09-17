using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateFolderLockRequestBodyFolderField : ISerializable {
        /// <summary>
        /// The content type the lock is being applied to. Only `folder`
        /// is supported.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; }

        /// <summary>
        /// The ID of the folder.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public CreateFolderLockRequestBodyFolderField(string type, string id) {
            Type = type;
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