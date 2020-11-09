using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a shared link
    /// </summary>
    public class BoxSharedLink
    {
        public const string FieldUrl = "url";
        public const string FieldDownloadUrl = "download_url";
        public const string FieldVanityUrl = "vanity_url";
        public const string FieldIsPasswordEnabled = "is_password_enabled";
        public const string FieldUnsharedAt = "unshared_at";
        public const string FieldDownloadCount = "download_count";
        public const string FieldPreviewCount = "preview_count";
        public const string FieldAccess = "access";
        public const string FieldPermissions = "permissions";
        public const string FieldEffectiveAccess = "effective_access";

        /// <summary>
        /// The Url of the shared link
        /// </summary>
        [JsonProperty(PropertyName = FieldUrl)]
        public string Url { get; private set; }

        /// <summary>
        /// The Url of the download
        /// </summary>
        [JsonProperty(PropertyName = FieldDownloadUrl)]
        public string DownloadUrl { get; private set; }

        /// <summary>
        /// An easily readible url
        /// </summary>
        [JsonProperty(PropertyName = FieldVanityUrl)]
        public string VanityUrl { get; private set; }

        /// <summary>
        /// Whether or not a password is enabled
        /// </summary>
        [JsonProperty(PropertyName = FieldIsPasswordEnabled)]
        public bool IsPasswordEnabled { get; private set; }

        /// <summary>
        /// When the item's share link will expire
        /// </summary>
        [JsonProperty(PropertyName = FieldUnsharedAt)]
        public DateTime? UnsharedAt { get; private set; }

        /// <summary>
        /// Number of downloads
        /// </summary>
        [JsonProperty(PropertyName = FieldDownloadCount)]
        public int DownloadCount { get; private set; }

        /// <summary>
        /// Number of previews 
        /// </summary>
        [JsonProperty(PropertyName = FieldPreviewCount)]
        public int PreviewCount { get; private set; }

        /// <summary>
        /// Type of access
        /// </summary>
        [JsonProperty(PropertyName = FieldAccess)]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxSharedLinkAccessType? Access { get; private set; }

        /// <summary>
        /// Type of permissions
        /// </summary>
        [JsonProperty(PropertyName = FieldPermissions)]
        public BoxPermission Permissions { get; private set; }

        /// <summary>
        /// Type of effective access
        /// </summary>
        [JsonProperty(PropertyName = FieldEffectiveAccess)]
        public BoxSharedLinkAccessType? EffectiveAccess { get; private set; }
    }
}
