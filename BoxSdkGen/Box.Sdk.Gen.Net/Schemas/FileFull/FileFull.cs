using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileFull : File, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_islockSet")]
        protected bool _isLockSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isexpires_atSet")]
        protected bool _isExpiresAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isdisposition_atSet")]
        protected bool _isDispositionAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isshared_link_permission_optionsSet")]
        protected bool _isSharedLinkPermissionOptionsSet { get; set; }

        protected FileFullLockField? _lock { get; set; }

        protected System.DateTimeOffset? _expiresAt { get; set; }

        protected System.DateTimeOffset? _dispositionAt { get; set; }

        protected IReadOnlyList<StringEnum<FileFullSharedLinkPermissionOptionsField>>? _sharedLinkPermissionOptions { get; set; }

        /// <summary>
        /// The version number of this file.
        /// </summary>
        [JsonPropertyName("version_number")]
        public string? VersionNumber { get; init; }

        /// <summary>
        /// The number of comments on this file.
        /// </summary>
        [JsonPropertyName("comment_count")]
        public long? CommentCount { get; init; }

        [JsonPropertyName("permissions")]
        public FileFullPermissionsField? Permissions { get; init; }

        [JsonPropertyName("tags")]
        public IReadOnlyList<string>? Tags { get; init; }

        [JsonPropertyName("lock")]
        public FileFullLockField? Lock { get => _lock; init { _lock = value; _isLockSet = true; } }

        /// <summary>
        /// Indicates the (optional) file extension for this file. By default,
        /// this is set to an empty string.
        /// </summary>
        [JsonPropertyName("extension")]
        public string? Extension { get; init; }

        /// <summary>
        /// Indicates if the file is a package. Packages are commonly used
        /// by Mac Applications and can include iWork files.
        /// </summary>
        [JsonPropertyName("is_package")]
        public bool? IsPackage { get; init; }

        [JsonPropertyName("expiring_embed_link")]
        public FileFullExpiringEmbedLinkField? ExpiringEmbedLink { get; init; }

        [JsonPropertyName("watermark_info")]
        public FileFullWatermarkInfoField? WatermarkInfo { get; init; }

        /// <summary>
        /// Specifies if the file can be accessed
        /// via the direct shared link or a shared link
        /// to a parent folder.
        /// </summary>
        [JsonPropertyName("is_accessible_via_shared_link")]
        public bool? IsAccessibleViaSharedLink { get; init; }

        /// <summary>
        /// A list of the types of roles that user can be invited at
        /// when sharing this file.
        /// </summary>
        [JsonPropertyName("allowed_invitee_roles")]
        [JsonConverter(typeof(StringEnumListConverter<FileFullAllowedInviteeRolesField>))]
        public IReadOnlyList<StringEnum<FileFullAllowedInviteeRolesField>>? AllowedInviteeRoles { get; init; }

        /// <summary>
        /// Specifies if this file is owned by a user outside of the
        /// authenticated enterprise.
        /// </summary>
        [JsonPropertyName("is_externally_owned")]
        public bool? IsExternallyOwned { get; init; }

        /// <summary>
        /// Specifies if this file has any other collaborators.
        /// </summary>
        [JsonPropertyName("has_collaborations")]
        public bool? HasCollaborations { get; init; }

        [JsonPropertyName("metadata")]
        public FileFullMetadataField? Metadata { get; init; }

        /// <summary>
        /// When the file will automatically be deleted.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public System.DateTimeOffset? ExpiresAt { get => _expiresAt; init { _expiresAt = value; _isExpiresAtSet = true; } }

        [JsonPropertyName("representations")]
        public FileFullRepresentationsField? Representations { get; init; }

        [JsonPropertyName("classification")]
        public FileFullClassificationField? Classification { get; init; }

        [JsonPropertyName("uploader_display_name")]
        public string? UploaderDisplayName { get; init; }

        /// <summary>
        /// The retention expiration timestamp for the given file.
        /// </summary>
        [JsonPropertyName("disposition_at")]
        public System.DateTimeOffset? DispositionAt { get => _dispositionAt; init { _dispositionAt = value; _isDispositionAtSet = true; } }

        /// <summary>
        /// A list of the types of roles that user can be invited at
        /// when sharing this file.
        /// </summary>
        [JsonPropertyName("shared_link_permission_options")]
        [JsonConverter(typeof(StringEnumListConverter<FileFullSharedLinkPermissionOptionsField>))]
        public IReadOnlyList<StringEnum<FileFullSharedLinkPermissionOptionsField>>? SharedLinkPermissionOptions { get => _sharedLinkPermissionOptions; init { _sharedLinkPermissionOptions = value; _isSharedLinkPermissionOptionsSet = true; } }

        /// <summary>
        /// This field will return true if the file or any ancestor of the file
        /// is associated with at least one app item. Note that this will return
        /// true even if the context user does not have access to the app item(s)
        /// associated with the file.
        /// </summary>
        [JsonPropertyName("is_associated_with_app_item")]
        public bool? IsAssociatedWithAppItem { get; init; }

        public FileFull(string id, FileBaseTypeField type = FileBaseTypeField.File) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal FileFull(string id, StringEnum<FileBaseTypeField> type) : base(id, type ?? new StringEnum<FileBaseTypeField>(FileBaseTypeField.File)) {
            
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