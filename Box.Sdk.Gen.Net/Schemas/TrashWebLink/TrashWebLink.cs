using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TrashWebLink : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_istrashed_atSet")]
        protected bool _isTrashedAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_ispurged_atSet")]
        protected bool _isPurgedAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isshared_linkSet")]
        protected bool _isSharedLinkSet { get; set; }

        protected System.DateTimeOffset? _trashedAt { get; set; }

        protected System.DateTimeOffset? _purgedAt { get; set; }

        protected string? _sharedLink { get; set; }

        /// <summary>
        /// The value will always be `web_link`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TrashWebLinkTypeField>))]
        public StringEnum<TrashWebLinkTypeField>? Type { get; init; }

        /// <summary>
        /// The unique identifier for this web link.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        [JsonPropertyName("sequence_id")]
        public string? SequenceId { get; init; }

        /// <summary>
        /// The entity tag of this web link. Used with `If-Match`
        /// headers.
        /// </summary>
        [JsonPropertyName("etag")]
        public string? Etag { get; init; }

        /// <summary>
        /// The name of the web link.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The URL this web link points to.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        [JsonPropertyName("parent")]
        public FolderMini? Parent { get; init; }

        /// <summary>
        /// The description accompanying the web link. This is
        /// visible within the Box web application.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        [JsonPropertyName("path_collection")]
        public TrashWebLinkPathCollectionField? PathCollection { get; init; }

        /// <summary>
        /// When this file was created on Box’s servers.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// When this file was last updated on the Box
        /// servers.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; init; }

        /// <summary>
        /// When this file was last moved to the trash.
        /// </summary>
        [JsonPropertyName("trashed_at")]
        public System.DateTimeOffset? TrashedAt { get => _trashedAt; init { _trashedAt = value; _isTrashedAtSet = true; } }

        /// <summary>
        /// When this file will be permanently deleted.
        /// </summary>
        [JsonPropertyName("purged_at")]
        public System.DateTimeOffset? PurgedAt { get => _purgedAt; init { _purgedAt = value; _isPurgedAtSet = true; } }

        [JsonPropertyName("created_by")]
        public UserMini? CreatedBy { get; init; }

        [JsonPropertyName("modified_by")]
        public UserMini? ModifiedBy { get; init; }

        [JsonPropertyName("owned_by")]
        public UserMini? OwnedBy { get; init; }

        /// <summary>
        /// The shared link for this bookmark. This will
        /// be `null` if a bookmark has been trashed, since the link will no longer
        /// be active.
        /// </summary>
        [JsonPropertyName("shared_link")]
        public string? SharedLink { get => _sharedLink; init { _sharedLink = value; _isSharedLinkSet = true; } }

        /// <summary>
        /// Whether this item is deleted or not. Values include `active`,
        /// `trashed` if the file has been moved to the trash, and `deleted` if
        /// the file has been permanently deleted.
        /// </summary>
        [JsonPropertyName("item_status")]
        [JsonConverter(typeof(StringEnumConverter<TrashWebLinkItemStatusField>))]
        public StringEnum<TrashWebLinkItemStatusField>? ItemStatus { get; init; }

        public TrashWebLink() {
            
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