using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TrashFile : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isetagSet")]
        protected bool _isEtagSet { get; set; }

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

        protected string? _etag { get; set; }

        protected System.DateTimeOffset? _trashedAt { get; set; }

        protected System.DateTimeOffset? _purgedAt { get; set; }

        protected System.DateTimeOffset? _contentCreatedAt { get; set; }

        protected System.DateTimeOffset? _contentModifiedAt { get; set; }

        protected string? _sharedLink { get; set; }

        /// <summary>
        /// The unique identifier that represent a file.
        /// 
        /// The ID for any file can be determined
        /// by visiting a file in the web application
        /// and copying the ID from the URL. For example,
        /// for the URL `https://*.app.box.com/files/123`
        /// the `file_id` is `123`.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        /// <summary>
        /// The HTTP `etag` of this file. This can be used within some API
        /// endpoints in the `If-Match` and `If-None-Match` headers to only
        /// perform changes on the file if (no) changes have happened.
        /// </summary>
        [JsonPropertyName("etag")]
        public string? Etag { get => _etag; init { _etag = value; _isEtagSet = true; } }

        /// <summary>
        /// The value will always be `file`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TrashFileTypeField>))]
        public StringEnum<TrashFileTypeField> Type { get; }

        [JsonPropertyName("sequence_id")]
        public string SequenceId { get; }

        /// <summary>
        /// The name of the file.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The SHA1 hash of the file. This can be used to compare the contents
        /// of a file on Box with a local file.
        /// </summary>
        [JsonPropertyName("sha1")]
        public string Sha1 { get; }

        [JsonPropertyName("file_version")]
        public FileVersionMini? FileVersion { get; init; }

        /// <summary>
        /// The optional description of this file.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; }

        /// <summary>
        /// The file size in bytes. Be careful parsing this integer as it can
        /// get very large and cause an integer overflow.
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; }

        [JsonPropertyName("path_collection")]
        public TrashFilePathCollectionField PathCollection { get; }

        /// <summary>
        /// The date and time when the file was created on Box.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The date and time when the file was last updated on Box.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset ModifiedAt { get; }

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
        public UserMini ModifiedBy { get; }

        [JsonPropertyName("owned_by")]
        public UserMini OwnedBy { get; }

        /// <summary>
        /// The shared link for this file. This will
        /// be `null` if a file has been trashed, since the link will no longer
        /// be active.
        /// </summary>
        [JsonPropertyName("shared_link")]
        public string? SharedLink { get => _sharedLink; init { _sharedLink = value; _isSharedLinkSet = true; } }

        [JsonPropertyName("parent")]
        public FolderMini? Parent { get; init; }

        /// <summary>
        /// Defines if this item has been deleted or not.
        /// 
        /// * `active` when the item has is not in the trash
        /// * `trashed` when the item has been moved to the trash but not deleted
        /// * `deleted` when the item has been permanently deleted.
        /// </summary>
        [JsonPropertyName("item_status")]
        [JsonConverter(typeof(StringEnumConverter<TrashFileItemStatusField>))]
        public StringEnum<TrashFileItemStatusField> ItemStatus { get; }

        public TrashFile(string id, string sequenceId, string sha1, string description, long size, TrashFilePathCollectionField pathCollection, System.DateTimeOffset createdAt, System.DateTimeOffset modifiedAt, UserMini modifiedBy, UserMini ownedBy, TrashFileItemStatusField itemStatus, TrashFileTypeField type = TrashFileTypeField.File) {
            Id = id;
            Type = type;
            SequenceId = sequenceId;
            Sha1 = sha1;
            Description = description;
            Size = size;
            PathCollection = pathCollection;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            ModifiedBy = modifiedBy;
            OwnedBy = ownedBy;
            ItemStatus = itemStatus;
        }
        
        [JsonConstructorAttribute]
        internal TrashFile(string id, string sequenceId, string sha1, string description, long size, TrashFilePathCollectionField pathCollection, System.DateTimeOffset createdAt, System.DateTimeOffset modifiedAt, UserMini modifiedBy, UserMini ownedBy, StringEnum<TrashFileItemStatusField> itemStatus, StringEnum<TrashFileTypeField> type) {
            Id = id;
            Type = TrashFileTypeField.File;
            SequenceId = sequenceId;
            Sha1 = sha1;
            Description = description;
            Size = size;
            PathCollection = pathCollection;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
            ModifiedBy = modifiedBy;
            OwnedBy = ownedBy;
            ItemStatus = itemStatus;
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