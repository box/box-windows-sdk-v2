using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FolderLockLockedOperationsField : ISerializable {
        /// <summary>
        /// Whether moving the folder is restricted.
        /// </summary>
        [JsonPropertyName("move")]
        public bool Move { get; }

        /// <summary>
        /// Whether deleting the folder is restricted.
        /// </summary>
        [JsonPropertyName("delete")]
        public bool Delete { get; }

        public FolderLockLockedOperationsField(bool move, bool delete) {
            Move = move;
            Delete = delete;
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