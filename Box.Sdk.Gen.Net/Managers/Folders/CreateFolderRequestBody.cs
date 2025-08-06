using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateFolderRequestBody : ISerializable {
        /// <summary>
        /// The name for the new folder.
        /// 
        /// The following restrictions to folder names apply: names containing
        /// non-printable ASCII characters, forward and backward slashes
        /// (`/`, `\`), names with trailing spaces, and names `.` and `..` are
        /// not allowed.
        /// 
        /// Folder names must be unique within their parent folder. The name check is case-insensitive, 
        /// so a folder named `New Folder` cannot be created in a parent folder that already contains 
        /// a folder named `new folder`.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; }

        /// <summary>
        /// The parent folder to create the new folder within.
        /// </summary>
        [JsonPropertyName("parent")]
        public CreateFolderRequestBodyParentField Parent { get; }

        [JsonPropertyName("folder_upload_email")]
        public CreateFolderRequestBodyFolderUploadEmailField? FolderUploadEmail { get; init; }

        /// <summary>
        /// Specifies whether a folder should be synced to a
        /// user's device or not. This is used by Box Sync
        /// (discontinued) and is not used by Box Drive.
        /// </summary>
        [JsonPropertyName("sync_state")]
        [JsonConverter(typeof(StringEnumConverter<CreateFolderRequestBodySyncStateField>))]
        public StringEnum<CreateFolderRequestBodySyncStateField>? SyncState { get; init; }

        public CreateFolderRequestBody(string name, CreateFolderRequestBodyParentField parent) {
            Name = name;
            Parent = parent;
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