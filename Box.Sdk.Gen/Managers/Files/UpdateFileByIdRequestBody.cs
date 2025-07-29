using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateFileByIdRequestBody : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isshared_linkSet")]
        protected bool _isSharedLinkSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_islockSet")]
        protected bool _isLockSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iscollectionsSet")]
        protected bool _isCollectionsSet { get; set; }

        protected UpdateFileByIdRequestBodySharedLinkField? _sharedLink { get; set; }

        protected UpdateFileByIdRequestBodyLockField? _lock { get; set; }

        protected IReadOnlyList<UpdateFileByIdRequestBodyCollectionsField>? _collections { get; set; }

        /// <summary>
        /// An optional different name for the file. This can be used to
        /// rename the file.
        /// 
        /// File names must be unique within their parent folder. The name check is case-insensitive, so a file 
        /// named `New File` cannot be created in a parent folder that already contains a folder named `new file`.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// The description for a file. This can be seen in the right-hand sidebar panel
        /// when viewing a file in the Box web app. Additionally, this index is used in
        /// the search index of the file, allowing users to find the file by the content
        /// in the description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        [JsonPropertyName("parent")]
        public UpdateFileByIdRequestBodyParentField? Parent { get; init; }

        [JsonPropertyName("shared_link")]
        public UpdateFileByIdRequestBodySharedLinkField? SharedLink { get => _sharedLink; init { _sharedLink = value; _isSharedLinkSet = true; } }

        /// <summary>
        /// Defines a lock on an item. This prevents the item from being
        /// moved, renamed, or otherwise changed by anyone other than the user
        /// who created the lock.
        /// 
        /// Set this to `null` to remove the lock.
        /// </summary>
        [JsonPropertyName("lock")]
        public UpdateFileByIdRequestBodyLockField? Lock { get => _lock; init { _lock = value; _isLockSet = true; } }

        /// <summary>
        /// The retention expiration timestamp for the given file. This
        /// date cannot be shortened once set on a file.
        /// </summary>
        [JsonPropertyName("disposition_at")]
        public System.DateTimeOffset? DispositionAt { get; init; }

        /// <summary>
        /// Defines who can download a file.
        /// </summary>
        [JsonPropertyName("permissions")]
        public UpdateFileByIdRequestBodyPermissionsField? Permissions { get; init; }

        /// <summary>
        /// An array of collections to make this file
        /// a member of. Currently
        /// we only support the `favorites` collection.
        /// 
        /// To get the ID for a collection, use the
        /// [List all collections][1] endpoint.
        /// 
        /// Passing an empty array `[]` or `null` will remove
        /// the file from all collections.
        /// 
        /// [1]: e://get-collections
        /// </summary>
        [JsonPropertyName("collections")]
        public IReadOnlyList<UpdateFileByIdRequestBodyCollectionsField>? Collections { get => _collections; init { _collections = value; _isCollectionsSet = true; } }

        /// <summary>
        /// The tags for this item. These tags are shown in
        /// the Box web app and mobile apps next to an item.
        /// 
        /// To add or remove a tag, retrieve the item's current tags,
        /// modify them, and then update this field.
        /// 
        /// There is a limit of 100 tags per item, and 10,000
        /// unique tags per enterprise.
        /// </summary>
        [JsonPropertyName("tags")]
        public IReadOnlyList<string>? Tags { get; init; }

        public UpdateFileByIdRequestBody() {
            
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