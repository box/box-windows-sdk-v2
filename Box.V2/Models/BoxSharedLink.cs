using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Box.V2.Models
{
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

        /// <summary>
        /// The Url of the shared link
        /// </summary>
        [JsonProperty(PropertyName = FieldUrl)]
        public string Url { get; private set; }

        [JsonProperty(PropertyName = FieldDownloadUrl)]
        public string DownloadUrl { get; private set; }

        [JsonProperty(PropertyName = FieldVanityUrl)]
        public string VanityUrl { get; private set; }

        [JsonProperty(PropertyName = FieldIsPasswordEnabled)]
        public bool IsPasswordEnabled { get; private set; }

        [JsonProperty(PropertyName = FieldUnsharedAt)]
        public DateTime? UnsharedAt { get; private set; }

        [JsonProperty(PropertyName = FieldDownloadCount)]
        public int DownloadCount { get; private set; }

        [JsonProperty(PropertyName = FieldPreviewCount)]
        public int PreviewCount { get; private set; }

        [JsonProperty(PropertyName = FieldAccess)]
        [JsonConverter(typeof(StringEnumConverter))]
        public BoxSharedLinkAccessType? Access { get; private set; }

        [JsonProperty(PropertyName = FieldPermissions)]
        public BoxPermission Permissions { get; private set; }
    }
}
