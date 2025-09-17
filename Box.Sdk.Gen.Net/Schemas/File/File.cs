using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class File : FileMini, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_istrashed_atSet")]
        protected bool _isTrashedAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_ispurged_atSet")]
        protected bool _isPurgedAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iscontent_created_atSet")]
        protected bool _isContentCreatedAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iscontent_modified_atSet")]
        protected bool _isContentModifiedAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isparentSet")]
        protected bool _isParentSet { get; set; }

        protected System.DateTimeOffset? _trashedAt { get; set; }

        protected System.DateTimeOffset? _purgedAt { get; set; }

        protected System.DateTimeOffset? _contentCreatedAt { get; set; }

        protected System.DateTimeOffset? _contentModifiedAt { get; set; }

        protected FolderMini? _parent { get; set; }

        /// <summary>
        /// The optional description of this file.
        /// If the description exceeds 255 characters, the first 255 characters
        /// are set as a file description and the rest of it is ignored.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// The file size in bytes. Be careful parsing this integer as it can
        /// get very large and cause an integer overflow.
        /// </summary>
        [JsonPropertyName("size")]
        public long? Size { get; init; }

        [JsonPropertyName("path_collection")]
        public FilePathCollectionField? PathCollection { get; init; }

        /// <summary>
        /// The date and time when the file was created on Box.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// The date and time when the file was last updated on Box.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; init; }

        /// <summary>
        /// The time at which this file was put in the trash.
        /// </summary>
        [JsonPropertyName("trashed_at")]
        public System.DateTimeOffset? TrashedAt { get => _trashedAt; init { _trashedAt = value; _isTrashedAtSet = true; } }

        /// <summary>
        /// The time at which this file is expected to be purged
        /// from the trash.
        /// </summary>
        [JsonPropertyName("purged_at")]
        public System.DateTimeOffset? PurgedAt { get => _purgedAt; init { _purgedAt = value; _isPurgedAtSet = true; } }

        /// <summary>
        /// The date and time at which this file was originally
        /// created, which might be before it was uploaded to Box.
        /// </summary>
        [JsonPropertyName("content_created_at")]
        public System.DateTimeOffset? ContentCreatedAt { get => _contentCreatedAt; init { _contentCreatedAt = value; _isContentCreatedAtSet = true; } }

        /// <summary>
        /// The date and time at which this file was last updated,
        /// which might be before it was uploaded to Box.
        /// </summary>
        [JsonPropertyName("content_modified_at")]
        public System.DateTimeOffset? ContentModifiedAt { get => _contentModifiedAt; init { _contentModifiedAt = value; _isContentModifiedAtSet = true; } }

        [JsonPropertyName("created_by")]
        public UserMini? CreatedBy { get; init; }

        [JsonPropertyName("modified_by")]
        public UserMini? ModifiedBy { get; init; }

        [JsonPropertyName("owned_by")]
        public UserMini? OwnedBy { get; init; }

        [JsonPropertyName("shared_link")]
        public FileSharedLinkField? SharedLink { get; init; }

        [JsonPropertyName("parent")]
        public FolderMini? Parent { get => _parent; init { _parent = value; _isParentSet = true; } }

        /// <summary>
        /// Defines if this item has been deleted or not.
        /// 
        /// * `active` when the item has is not in the trash
        /// * `trashed` when the item has been moved to the trash but not deleted
        /// * `deleted` when the item has been permanently deleted.
        /// </summary>
        [JsonPropertyName("item_status")]
        [JsonConverter(typeof(StringEnumConverter<FileItemStatusField>))]
        public StringEnum<FileItemStatusField>? ItemStatus { get; init; }

        public File(string id, FileBaseTypeField type = FileBaseTypeField.File) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal File(string id, StringEnum<FileBaseTypeField> type) : base(id, type ?? new StringEnum<FileBaseTypeField>(FileBaseTypeField.File)) {
            
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}