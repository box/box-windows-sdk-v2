using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TrashFolderRestored : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isetagSet")]
        protected bool _isEtagSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iscreated_atSet")]
        protected bool _isCreatedAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_ismodified_atSet")]
        protected bool _isModifiedAtSet { get; set; }

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
        [JsonPropertyName("_isshared_linkSet")]
        protected bool _isSharedLinkSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isfolder_upload_emailSet")]
        protected bool _isFolderUploadEmailSet { get; set; }

        protected string? _etag { get; set; }

        protected System.DateTimeOffset? _createdAt { get; set; }

        protected System.DateTimeOffset? _modifiedAt { get; set; }

        protected string? _trashedAt { get; set; }

        protected string? _purgedAt { get; set; }

        protected System.DateTimeOffset? _contentCreatedAt { get; set; }

        protected System.DateTimeOffset? _contentModifiedAt { get; set; }

        protected string? _sharedLink { get; set; }

        protected string? _folderUploadEmail { get; set; }

        /// <summary>
        /// The unique identifier that represent a folder.
        /// 
        /// The ID for any folder can be determined
        /// by visiting a folder in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/folders/123`
        /// the `folder_id` is `123`.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The HTTP `etag` of this folder. This can be used within some API
        /// endpoints in the `If-Match` and `If-None-Match` headers to only
        /// perform changes on the folder if (no) changes have happened.
        /// </summary>
        [JsonPropertyName("etag")]
        public string? Etag { get => _etag; init { _etag = value; _isEtagSet = true; } }

        /// <summary>
        /// The value will always be `folder`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TrashFolderRestoredTypeField>))]
        public StringEnum<TrashFolderRestoredTypeField>? Type { get; init; }

        [JsonPropertyName("sequence_id")]
        public string? SequenceId { get; init; }

        /// <summary>
        /// The name of the folder.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The date and time when the folder was created. This value may
        /// be `null` for some folders such as the root folder or the trash
        /// folder.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get => _createdAt; init { _createdAt = value; _isCreatedAtSet = true; } }

        /// <summary>
        /// The date and time when the folder was last updated. This value may
        /// be `null` for some folders such as the root folder or the trash
        /// folder.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get => _modifiedAt; init { _modifiedAt = value; _isModifiedAtSet = true; } }

        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// The folder size in bytes.
        /// 
        /// Be careful parsing this integer as its
        /// value can get very large.
        /// </summary>
        [JsonPropertyName("size")]
        public long? Size { get; init; }

        [JsonPropertyName("path_collection")]
        public TrashFolderRestoredPathCollectionField? PathCollection { get; init; }

        [JsonPropertyName("created_by")]
        public UserMini? CreatedBy { get; init; }

        [JsonPropertyName("modified_by")]
        public UserMini? ModifiedBy { get; init; }

        /// <summary>
        /// The time at which this folder was put in the
        /// trash - becomes `null` after restore.
        /// </summary>
        [JsonPropertyName("trashed_at")]
        public string? TrashedAt { get => _trashedAt; init { _trashedAt = value; _isTrashedAtSet = true; } }

        /// <summary>
        /// The time at which this folder is expected to be purged
        /// from the trash  - becomes `null` after restore.
        /// </summary>
        [JsonPropertyName("purged_at")]
        public string? PurgedAt { get => _purgedAt; init { _purgedAt = value; _isPurgedAtSet = true; } }

        /// <summary>
        /// The date and time at which this folder was originally
        /// created.
        /// </summary>
        [JsonPropertyName("content_created_at")]
        public System.DateTimeOffset? ContentCreatedAt { get => _contentCreatedAt; init { _contentCreatedAt = value; _isContentCreatedAtSet = true; } }

        /// <summary>
        /// The date and time at which this folder was last updated.
        /// </summary>
        [JsonPropertyName("content_modified_at")]
        public System.DateTimeOffset? ContentModifiedAt { get => _contentModifiedAt; init { _contentModifiedAt = value; _isContentModifiedAtSet = true; } }

        [JsonPropertyName("owned_by")]
        public UserMini? OwnedBy { get; init; }

        /// <summary>
        /// The shared link for this file. This will
        /// be `null` if a folder had been trashed, even though the original shared
        /// link does become active again.
        /// </summary>
        [JsonPropertyName("shared_link")]
        public string? SharedLink { get => _sharedLink; init { _sharedLink = value; _isSharedLinkSet = true; } }

        /// <summary>
        /// The folder upload email for this folder. This will
        /// be `null` if a folder has been trashed, even though the original upload
        /// email does become active again.
        /// </summary>
        [JsonPropertyName("folder_upload_email")]
        public string? FolderUploadEmail { get => _folderUploadEmail; init { _folderUploadEmail = value; _isFolderUploadEmailSet = true; } }

        [JsonPropertyName("parent")]
        public FolderMini? Parent { get; init; }

        /// <summary>
        /// Defines if this item has been deleted or not.
        /// 
        /// * `active` when the item has is not in the trash,
        /// * `trashed` when the item has been moved to the trash but not deleted,
        /// * `deleted` when the item has been permanently deleted.
        /// </summary>
        [JsonPropertyName("item_status")]
        [JsonConverter(typeof(StringEnumConverter<TrashFolderRestoredItemStatusField>))]
        public StringEnum<TrashFolderRestoredItemStatusField>? ItemStatus { get; init; }

        public TrashFolderRestored() {
            
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