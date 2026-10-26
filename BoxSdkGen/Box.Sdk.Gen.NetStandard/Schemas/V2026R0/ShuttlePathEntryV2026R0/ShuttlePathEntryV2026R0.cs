using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttlePathEntryV2026R0 : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_ispath_idSet")]
        protected bool _isPathIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isparent_pathSet")]
        protected bool _isParentPathSet { get; set; }

        protected string _pathId { get; set; }

        protected string _parentPath { get; set; }

        /// <summary>
        /// The unique identifier of this browse row. This is not the account
        /// identifier; when `scope.type` is `account`, the account identifier is
        /// `scope.id`.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The value will always be `shuttle_path_entry`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttlePathEntryV2026R0TypeField>))]
        public StringEnum<ShuttlePathEntryV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The display name of the path entry.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The human-readable path of the entry.
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }

        /// <summary>
        /// The system-native identifier of the path. `null` when the selected
        /// system does not provide one. Maps to `path_id` on a path mapping
        /// location.
        /// </summary>
        [JsonPropertyName("path_id")]
        public string PathId { get => _pathId; set { _pathId = value; _isPathIdSet = true; } }

        /// <summary>
        /// The human-readable path of the parent entry, or `null` for a root
        /// entry.
        /// </summary>
        [JsonPropertyName("parent_path")]
        public string ParentPath { get => _parentPath; set { _parentPath = value; _isParentPathSet = true; } }

        [JsonPropertyName("scope")]
        public ShuttlePathEntryScopeV2026R0 Scope { get; set; }

        /// <summary>
        /// Whether the entry is a directory (`true`) or a single file
        /// (`false`). Maps to `is_directory` on a path mapping location.
        /// </summary>
        [JsonPropertyName("is_directory")]
        public bool IsDirectory { get; set; }

        /// <summary>
        /// Whether the entry can be selected in a path mapping.
        /// </summary>
        [JsonPropertyName("can_map")]
        public bool CanMap { get; set; }

        public ShuttlePathEntryV2026R0(string id, string name, string path, ShuttlePathEntryScopeV2026R0 scope, bool isDirectory, bool canMap, ShuttlePathEntryV2026R0TypeField type = ShuttlePathEntryV2026R0TypeField.ShuttlePathEntry) {
            Id = id;
            Type = type;
            Name = name;
            Path = path;
            Scope = scope;
            IsDirectory = isDirectory;
            CanMap = canMap;
        }
        
        [JsonConstructorAttribute]
        internal ShuttlePathEntryV2026R0(string id, string name, string path, ShuttlePathEntryScopeV2026R0 scope, bool isDirectory, bool canMap, StringEnum<ShuttlePathEntryV2026R0TypeField> type) {
            Id = id;
            Type = ShuttlePathEntryV2026R0TypeField.ShuttlePathEntry;
            Name = name;
            Path = path;
            Scope = scope;
            IsDirectory = isDirectory;
            CanMap = canMap;
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}