using Newtonsoft.Json;
using System;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a user
    /// </summary>
    public class BoxUser : BoxEntity
    {
        public const string FieldName = "name";
        public const string FieldLogin = "login";
        public const string FieldCreatedAt = "created_at";
        public const string FieldModifiedAt = "modified_at";
        public const string FieldRole = "role";
        public const string FieldLanguage = "language";
        public const string FieldSpaceAmount = "space_amount";
        public const string FieldSpaceUsed = "space_used";
        public const string FieldMaxUploadSize = "max_upload_size";
        public const string FieldTrackingCodes = "tracking_codes";
        public const string FieldCanSeeManagedUsers = "can_see_managed_users";
        public const string FieldIsSyncEnabled = "is_sync_enabled";
        public const string FieldStatus = "status";
        public const string FieldJobTitle = "job_title";
        public const string FieldPhone = "phone";
        public const string FieldAddress = "address";
        public const string FieldAvatarUrl = "avatar_url";
        public const string FieldIsExemptFromDeviceLimits = "is_exempt_from_device_limits";
        public const string FieldIsExemptFromLoginVerification = "is_exempt_from_login_verification";
        public const string FieldEnterprise = "enterprise";
        public const string FieldIsPlatformAccessOnly = "is_platform_access_only";

        /// <summary>
        /// The name of this user
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public string Name { get; private set; }

        /// <summary>
        /// The email address this user uses to login
        /// </summary>
        [JsonProperty(PropertyName = FieldLogin)]
        public string Login { get; private set; }

        /// <summary>
        /// The time this user was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The time this user was last modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public DateTime? ModifiedAt { get; private set; }

        /// <summary>
        /// This user’s enterprise role. Can be admin, coadmin, or user
        /// </summary>
        [JsonProperty(PropertyName = FieldRole)]
        public string Role { get; private set; }

        /// <summary>
        /// The language of this user
        /// </summary>
        [JsonProperty(PropertyName = FieldLanguage)]
        public string Language { get; private set; }

        /// <summary>
        /// The user’s total available space amount in bytes
        /// </summary>
        [JsonProperty(PropertyName = FieldSpaceAmount)]
        public long? SpaceAmount { get; private set; }

        /// <summary>
        /// The amount of space in use by the user
        /// </summary>
        [JsonProperty(PropertyName = FieldSpaceUsed)]
        public long? SpaceUsed { get; private set; }

        /// <summary>
        /// The maximum individual file size in bytes this user can have
        /// </summary>
        [JsonProperty(PropertyName = FieldMaxUploadSize)]
        public long? MaxUploadSize { get; private set; }

        /// <summary>
        /// An array of key/value pairs set by the user’s admin
        /// </summary>
        [JsonProperty(PropertyName = FieldTrackingCodes)]
        public string[] TrackingCodes { get; private set; }

        /// <summary>
        /// Whether this user can see other enterprise users in its contact list
        /// </summary>
        [JsonProperty(PropertyName = FieldCanSeeManagedUsers)]
        public bool? CanSeeManagedUsers { get; private set; }

        /// <summary>
        /// Whether or not this user can use Box Sync
        /// </summary>
        [JsonProperty(PropertyName = FieldIsSyncEnabled)]
        public bool? IsSyncEnabled { get; private set; }

        /// <summary>
        /// Can be active or inactive
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public string Status { get; private set; }

        /// <summary>
        /// The user’s job title
        /// </summary>
        [JsonProperty(PropertyName = FieldJobTitle)]
        public string JobTitle { get; private set; }

        /// <summary>
        /// The user’s phone number
        /// </summary>
        [JsonProperty(PropertyName = FieldPhone)]
        public string Phone { get; private set; }

        /// <summary>
        /// The user’s address
        /// </summary>
        [JsonProperty(PropertyName = FieldAddress)]
        public string Address { get; private set; }

        /// <summary>
        /// URL of this user’s avatar image
        /// </summary>
        [JsonProperty(PropertyName = FieldAvatarUrl)]
        public string AvatarUrl { get; private set; }

        /// <summary>
        /// Whether to exempt this user from Enterprise device limits
        /// </summary>
        [JsonProperty(PropertyName = FieldIsExemptFromDeviceLimits)]
        public bool IsExemptFromDeviceLimits { get; private set; }

        /// <summary>
        /// Whether or not this user must use two-factor authentication
        /// </summary>
        [JsonProperty(PropertyName = FieldIsExemptFromLoginVerification)]
        public bool IsExemptFromLoginVerification { get; private set; }

        /// <summary>
        /// Mini representation of this user’s enterprise, including the ID of its enterprise
        /// </summary>
        [JsonProperty(PropertyName = FieldEnterprise)]
        public BoxEnterprise Enterprise { get; private set; }

        /// <summary>
        /// Whether or not the user is an App User (platform)
        /// </summary>
        [JsonProperty(PropertyName = FieldIsPlatformAccessOnly)]
        public bool? IsPlatformAccessOnly { get; private set; }

    }
}
