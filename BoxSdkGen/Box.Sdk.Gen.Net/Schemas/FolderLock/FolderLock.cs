using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FolderLock : ISerializable {
        [JsonPropertyName("folder")]
        public FolderMini? Folder { get; init; }

        /// <summary>
        /// The unique identifier for this folder lock.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The object type, always `folder_lock`.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("created_by")]
        public UserBase? CreatedBy { get; init; }

        /// <summary>
        /// When the folder lock object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// The operations that have been locked. Currently the `move`
        /// and `delete` operations cannot be locked separately, and both need to be
        /// set to `true`.
        /// </summary>
        [JsonPropertyName("locked_operations")]
        public FolderLockLockedOperationsField? LockedOperations { get; init; }

        /// <summary>
        /// The lock type, always `freeze`.
        /// </summary>
        [JsonPropertyName("lock_type")]
        public string? LockType { get; init; }

        public FolderLock() {
            
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