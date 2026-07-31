using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class WebLink : WebLinkMini, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_istrashed_atSet")]
        protected bool _isTrashedAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_ispurged_atSet")]
        protected bool _isPurgedAtSet { get; set; }

        protected System.DateTimeOffset? _trashedAt { get; set; }

        protected System.DateTimeOffset? _purgedAt { get; set; }

        [JsonPropertyName("parent")]
        public FolderMini? Parent { get; init; }

        /// <summary>
        /// The description accompanying the web link. This is
        /// visible within the Box web application.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        [JsonPropertyName("path_collection")]
        public WebLinkPathCollectionField? PathCollection { get; init; }

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
        /// When this file was moved to the trash.
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

        [JsonPropertyName("shared_link")]
        public WebLinkSharedLinkField? SharedLink { get; init; }

        /// <summary>
        /// Whether this item is deleted or not. Values include `active`,
        /// `trashed` if the file has been moved to the trash, and `deleted` if
        /// the file has been permanently deleted.
        /// </summary>
        [JsonPropertyName("item_status")]
        [JsonConverter(typeof(StringEnumConverter<WebLinkItemStatusField>))]
        public StringEnum<WebLinkItemStatusField>? ItemStatus { get; init; }

        /// <summary>
        /// The collections that this web link belongs to.
        /// 
        /// For more information, see the
        /// [collections guide](https://developer.box.com/guides/collections).
        /// </summary>
        [JsonPropertyName("collections")]
        public IReadOnlyList<Collection>? Collections { get; init; }

        /// <summary>
        /// The shared link access levels the authenticated user is allowed to
        /// use when creating or updating a shared link for this web link.
        /// 
        /// The list depends on item policy and user authorization, so it may be
        /// narrower than the levels available to the owner. An empty array means
        /// no access level is available to this user.
        /// </summary>
        [JsonPropertyName("allowed_shared_link_access_levels")]
        [JsonConverter(typeof(StringEnumListConverter<WebLinkAllowedSharedLinkAccessLevelsField>))]
        public IReadOnlyList<StringEnum<WebLinkAllowedSharedLinkAccessLevelsField>>? AllowedSharedLinkAccessLevels { get; init; }

        public WebLink(string id, WebLinkBaseTypeField type = WebLinkBaseTypeField.WebLink) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal WebLink(string id, StringEnum<WebLinkBaseTypeField> type) : base(id, type ?? new StringEnum<WebLinkBaseTypeField>(WebLinkBaseTypeField.WebLink)) {
            
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