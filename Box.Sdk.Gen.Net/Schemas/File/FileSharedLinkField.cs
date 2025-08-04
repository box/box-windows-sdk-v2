using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileSharedLinkField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isdownload_urlSet")]
        protected bool _isDownloadUrlSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isvanity_urlSet")]
        protected bool _isVanityUrlSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isvanity_nameSet")]
        protected bool _isVanityNameSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isunshared_atSet")]
        protected bool _isUnsharedAtSet { get; set; }

        protected string? _downloadUrl { get; set; }

        protected string? _vanityUrl { get; set; }

        protected string? _vanityName { get; set; }

        protected System.DateTimeOffset? _unsharedAt { get; set; }

        /// <summary>
        /// The URL that can be used to access the item on Box.
        /// 
        /// This URL will display the item in Box's preview UI where the file
        /// can be downloaded if allowed.
        /// 
        /// This URL will continue to work even when a custom `vanity_url`
        /// has been set for this shared link.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; }

        /// <summary>
        /// A URL that can be used to download the file. This URL can be used in
        /// a browser to download the file. This URL includes the file
        /// extension so that the file will be saved with the right file type.
        /// 
        /// This property will be `null` for folders.
        /// </summary>
        [JsonPropertyName("download_url")]
        public string? DownloadUrl { get => _downloadUrl; init { _downloadUrl = value; _isDownloadUrlSet = true; } }

        /// <summary>
        /// The "Custom URL" that can also be used to preview the item on Box.  Custom
        /// URLs can only be created or modified in the Box Web application.
        /// </summary>
        [JsonPropertyName("vanity_url")]
        public string? VanityUrl { get => _vanityUrl; init { _vanityUrl = value; _isVanityUrlSet = true; } }

        /// <summary>
        /// The custom name of a shared link, as used in the `vanity_url` field.
        /// </summary>
        [JsonPropertyName("vanity_name")]
        public string? VanityName { get => _vanityName; init { _vanityName = value; _isVanityNameSet = true; } }

        /// <summary>
        /// The access level for this shared link.
        /// 
        /// * `open` - provides access to this item to anyone with this link
        /// * `company` - only provides access to this item to people the same company
        /// * `collaborators` - only provides access to this item to people who are
        ///    collaborators on this item
        /// 
        /// If this field is omitted when creating the shared link, the access level
        /// will be set to the default access level specified by the enterprise admin.
        /// </summary>
        [JsonPropertyName("access")]
        [JsonConverter(typeof(StringEnumConverter<FileSharedLinkAccessField>))]
        public StringEnum<FileSharedLinkAccessField>? Access { get; init; }

        /// <summary>
        /// The effective access level for the shared link. This can be a more
        /// restrictive access level than the value in the `access` field when the
        /// enterprise settings restrict the allowed access levels.
        /// </summary>
        [JsonPropertyName("effective_access")]
        [JsonConverter(typeof(StringEnumConverter<FileSharedLinkEffectiveAccessField>))]
        public StringEnum<FileSharedLinkEffectiveAccessField> EffectiveAccess { get; }

        /// <summary>
        /// The effective permissions for this shared link.
        /// These result in the more restrictive combination of
        /// the share link permissions and the item permissions set
        /// by the administrator, the owner, and any ancestor item
        /// such as a folder.
        /// </summary>
        [JsonPropertyName("effective_permission")]
        [JsonConverter(typeof(StringEnumConverter<FileSharedLinkEffectivePermissionField>))]
        public StringEnum<FileSharedLinkEffectivePermissionField> EffectivePermission { get; }

        /// <summary>
        /// The date and time when this link will be unshared. This field can only be
        /// set by users with paid accounts.
        /// </summary>
        [JsonPropertyName("unshared_at")]
        public System.DateTimeOffset? UnsharedAt { get => _unsharedAt; init { _unsharedAt = value; _isUnsharedAtSet = true; } }

        /// <summary>
        /// Defines if the shared link requires a password to access the item.
        /// </summary>
        [JsonPropertyName("is_password_enabled")]
        public bool IsPasswordEnabled { get; }

        /// <summary>
        /// Defines if this link allows a user to preview, edit, and download an item.
        /// These permissions refer to the shared link only and
        /// do not supersede permissions applied to the item itself.
        /// </summary>
        [JsonPropertyName("permissions")]
        public FileSharedLinkPermissionsField? Permissions { get; init; }

        /// <summary>
        /// The number of times this item has been downloaded.
        /// </summary>
        [JsonPropertyName("download_count")]
        public long DownloadCount { get; }

        /// <summary>
        /// The number of times this item has been previewed.
        /// </summary>
        [JsonPropertyName("preview_count")]
        public long PreviewCount { get; }

        public FileSharedLinkField(string url, FileSharedLinkEffectiveAccessField effectiveAccess, FileSharedLinkEffectivePermissionField effectivePermission, bool isPasswordEnabled, long downloadCount, long previewCount) {
            Url = url;
            EffectiveAccess = effectiveAccess;
            EffectivePermission = effectivePermission;
            IsPasswordEnabled = isPasswordEnabled;
            DownloadCount = downloadCount;
            PreviewCount = previewCount;
        }
        
        [JsonConstructorAttribute]
        internal FileSharedLinkField(string url, StringEnum<FileSharedLinkEffectiveAccessField> effectiveAccess, StringEnum<FileSharedLinkEffectivePermissionField> effectivePermission, bool isPasswordEnabled, long downloadCount, long previewCount) {
            Url = url;
            EffectiveAccess = effectiveAccess;
            EffectivePermission = effectivePermission;
            IsPasswordEnabled = isPasswordEnabled;
            DownloadCount = downloadCount;
            PreviewCount = previewCount;
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