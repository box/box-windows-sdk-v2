using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateFolderLockRequestBody : ISerializable {
        /// <summary>
        /// The operations to lock for the folder. If `locked_operations` is
        /// included in the request, both `move` and `delete` must also be
        /// included and both set to `true`.
        /// </summary>
        [JsonPropertyName("locked_operations")]
        public CreateFolderLockRequestBodyLockedOperationsField? LockedOperations { get; init; }

        /// <summary>
        /// The folder to apply the lock to.
        /// </summary>
        [JsonPropertyName("folder")]
        public CreateFolderLockRequestBodyFolderField Folder { get; }

        public CreateFolderLockRequestBody(CreateFolderLockRequestBodyFolderField folder) {
            Folder = folder;
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