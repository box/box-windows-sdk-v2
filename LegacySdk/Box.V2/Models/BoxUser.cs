using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        public const string FieldTimezone = "timezone";
        public const string FieldIsExternalCollabRestricted = "is_external_collab_restricted";
        public const string FieldMyTags = "my_tags";
        public const string FieldHostname = "hostname";
        public const string FieldExternalAppUserId = "external_app_user_id";
        public const string FieldNotificationEmail = "notification_email";

        /// <summary>
        /// The name of this user
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual string Name { get; private set; }

        /// <summary>
        /// The email address this user uses to login
        /// </summary>
        [JsonProperty(PropertyName = FieldLogin)]
        public virtual string Login { get; private set; }

        /// <summary>
        /// The time this user was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The time this user was last modified
        /// </summary>
        [JsonProperty(PropertyName = FieldModifiedAt)]
        public virtual DateTimeOffset? ModifiedAt { get; private set; }

        /// <summary>
        /// This user’s enterprise role. Can be admin, coadmin, or user
        /// </summary>
        [JsonProperty(PropertyName = FieldRole)]
        public virtual string Role { get; private set; }

        /// <summary>
        /// The language of this user
        /// </summary>
        [JsonProperty(PropertyName = FieldLanguage)]
        public virtual string Language { get; private set; }

        /// <summary>
        /// The user’s total available space amount in bytes
        /// </summary>
        [JsonProperty(PropertyName = FieldSpaceAmount)]
        public virtual long? SpaceAmount { get; private set; }

        /// <summary>
        /// The amount of space in use by the user
        /// </summary>
        [JsonProperty(PropertyName = FieldSpaceUsed)]
        public virtual long? SpaceUsed { get; private set; }

        /// <summary>
        /// The maximum individual file size in bytes this user can have
        /// </summary>
        [JsonProperty(PropertyName = FieldMaxUploadSize)]
        public virtual long? MaxUploadSize { get; private set; }

        /// <summary>
        /// An array of key/value pairs set by the user’s admin
        /// </summary>
        [JsonProperty(PropertyName = FieldTrackingCodes)]
        public virtual IList<BoxTrackingCode> TrackingCodes { get; private set; }

        /// <summary>
        /// Whether this user can see other enterprise users in its contact list
        /// </summary>
        [JsonProperty(PropertyName = FieldCanSeeManagedUsers)]
        public virtual bool? CanSeeManagedUsers { get; private set; }

        /// <summary>
        /// Whether or not this user can use Box Sync
        /// </summary>
        [JsonProperty(PropertyName = FieldIsSyncEnabled)]
        public virtual bool? IsSyncEnabled { get; private set; }

        /// <summary>
        /// Can be active or inactive
        /// </summary>
        [JsonProperty(PropertyName = FieldStatus)]
        public virtual string Status { get; private set; }

        /// <summary>
        /// The user’s job title
        /// </summary>
        [JsonProperty(PropertyName = FieldJobTitle)]
        public virtual string JobTitle { get; private set; }

        /// <summary>
        /// The user’s phone number
        /// </summary>
        [JsonProperty(PropertyName = FieldPhone)]
        public virtual string Phone { get; private set; }

        /// <summary>
        /// The user’s address
        /// </summary>
        [JsonProperty(PropertyName = FieldAddress)]
        public virtual string Address { get; private set; }

        /// <summary>
        /// URL of this user’s avatar image
        /// </summary>
        [JsonProperty(PropertyName = FieldAvatarUrl)]
        public virtual string AvatarUrl { get; private set; }

        /// <summary>
        /// Whether to exempt this user from Enterprise device limits
        /// </summary>
        [JsonProperty(PropertyName = FieldIsExemptFromDeviceLimits)]
        public virtual bool IsExemptFromDeviceLimits { get; private set; }

        /// <summary>
        /// Whether or not this user must use two-factor authentication
        /// </summary>
        [JsonProperty(PropertyName = FieldIsExemptFromLoginVerification)]
        public virtual bool IsExemptFromLoginVerification { get; private set; }

        /// <summary>
        /// Mini representation of this user’s enterprise, including the ID of its enterprise
        /// </summary>
        [JsonProperty(PropertyName = FieldEnterprise)]
        public virtual BoxEnterprise Enterprise { get; private set; }

        /// <summary>
        /// Whether or not the user is an App User (platform)
        /// </summary>
        [JsonProperty(PropertyName = FieldIsPlatformAccessOnly)]
        public virtual bool? IsPlatformAccessOnly { get; private set; }

        /// <summary>
        /// The user's timezone
        /// </summary>
        [JsonProperty(PropertyName = FieldTimezone)]
        public virtual string Timezone { get; private set; }

        /// <summary>
        /// Whether the user has been restricted from collaborating with parties outside their enterprise
        /// </summary>
        [JsonProperty(PropertyName = FieldIsExternalCollabRestricted)]
        public virtual bool? IsExternalCollabRestricted { get; private set; }

        /// <summary>
        /// Tags for all files and folders owned by the user
        /// </summary>
        [JsonProperty(PropertyName = FieldMyTags)]
        public virtual string[] Tags { get; private set; }

        /// <summary>
        /// The root (protocol, subdomain, domain) of any Box URLs that need to be generated for the user
        /// </summary>
        [JsonProperty(PropertyName = FieldHostname)]
        public virtual string Hostname { get; private set; }

        /// <summary>
        /// The external app user id that has been set for the app user.  An arbitrary identifier that can be used by external user sync tools to link this Box User to an external user.
        /// Example values of this field could be an Active Directory Object ID or primary key from a user-tracking database. We recommend use of this field in order to avoid issues when email addresses and names are updated in either Box or external systems.
        /// </summary>
        [JsonProperty(PropertyName = FieldExternalAppUserId)]
        public virtual string ExternalAppUserId { get; private set; }

        /// <summary>
        /// An alternate notification email address to which email notifications are sent. When it's confirmed, this will be the email address to which notifications are sent instead
        /// of to the primary email address.
        /// </summary>
        [JsonProperty(PropertyName = FieldNotificationEmail)]
        public virtual BoxNotificationEmail NotificationEmail { get; set; }
    }
}
