using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
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
        public FolderMini Parent { get; set; }

        /// <summary>
        /// The description accompanying the web link. This is
        /// visible within the Box web application.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("path_collection")]
        public WebLinkPathCollectionField PathCollection { get; set; }

        /// <summary>
        /// When this file was created on Box’s servers.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// When this file was last updated on the Box
        /// servers.
        /// </summary>
        [JsonPropertyName("modified_at")]
        public System.DateTimeOffset? ModifiedAt { get; set; }

        /// <summary>
        /// When this file was moved to the trash.
        /// </summary>
        [JsonPropertyName("trashed_at")]
        public System.DateTimeOffset? TrashedAt { get => _trashedAt; set { _trashedAt = value; _isTrashedAtSet = true; } }

        /// <summary>
        /// When this file will be permanently deleted.
        /// </summary>
        [JsonPropertyName("purged_at")]
        public System.DateTimeOffset? PurgedAt { get => _purgedAt; set { _purgedAt = value; _isPurgedAtSet = true; } }

        [JsonPropertyName("created_by")]
        public UserMini CreatedBy { get; set; }

        [JsonPropertyName("modified_by")]
        public UserMini ModifiedBy { get; set; }

        [JsonPropertyName("owned_by")]
        public UserMini OwnedBy { get; set; }

        [JsonPropertyName("shared_link")]
        public WebLinkSharedLinkField SharedLink { get; set; }

        /// <summary>
        /// Whether this item is deleted or not. Values include `active`,
        /// `trashed` if the file has been moved to the trash, and `deleted` if
        /// the file has been permanently deleted.
        /// </summary>
        [JsonPropertyName("item_status")]
        [JsonConverter(typeof(StringEnumConverter<WebLinkItemStatusField>))]
        public StringEnum<WebLinkItemStatusField> ItemStatus { get; set; }

        public WebLink(string id, WebLinkBaseTypeField type = WebLinkBaseTypeField.WebLink) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal WebLink(string id, StringEnum<WebLinkBaseTypeField> type) : base(id, type ?? new StringEnum<WebLinkBaseTypeField>(WebLinkBaseTypeField.WebLink)) {
            
        }
        internal new string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}