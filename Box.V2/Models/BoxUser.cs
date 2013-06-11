using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public class BoxUser : BoxEntity
    {
        /// <summary>
        /// The name of this user
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// The email address this user uses to login
        /// </summary>
        [JsonProperty(PropertyName = "login")]
        public string Login { get; private set; }

        /// <summary>
        /// The time this user was created
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The time this user was last modified
        /// </summary>
        [JsonProperty(PropertyName = "modified_at")]
        public DateTime? ModifiedAt { get; private set; }

        /// <summary>
        /// This user’s enterprise role. Can be admin, coadmin, or user
        /// </summary>
        [JsonProperty(PropertyName = "role")]
        public string Role { get; private set; }

        /// <summary>
        /// The language of this user
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; private set; }

        /// <summary>
        /// The user’s total available space amount in bytes
        /// </summary>
        [JsonProperty(PropertyName = "space_amount")]
        public long? SpaceAmount { get; private set; }

        /// <summary>
        /// The amount of space in use by the user
        /// </summary>
        [JsonProperty(PropertyName = "space_used")]
        public long? SpaceUsed { get; private set; }

        /// <summary>
        /// The maximum individual file size in bytes this user can have
        /// </summary>
        [JsonProperty(PropertyName = "max_upload_size")]
        public long? MaxUploadSize { get; private set; }

        /// <summary>
        /// An array of key/value pairs set by the user’s admin
        /// </summary>
        [JsonProperty(PropertyName = "tracking_codes")]
        public string[] TrackingCodes { get; private set; }

        /// <summary>
        /// Whether this user can see other enterprise users in its contact list
        /// </summary>
        [JsonProperty(PropertyName = "can_see_managed_users")]
        public bool? CanSeeManagedUsers { get; private set; }

        /// <summary>
        /// Whether or not this user can use Box Sync
        /// </summary>
        [JsonProperty(PropertyName = "is_sync_enabled")]
        public bool? IsSyncEnabled { get; private set; }

        /// <summary>
        /// Can be active or inactive
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; private set; }

        /// <summary>
        /// The user’s job title
        /// </summary>
        [JsonProperty(PropertyName = "job_title")]
        public string JobTitle { get; private set; }

        /// <summary>
        /// The user’s phone number
        /// </summary>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; private set; }

        /// <summary>
        /// The user’s address
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; private set; }

        /// <summary>
        /// URL of this user’s avatar image
        /// </summary>
        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; private set; }

        /// <summary>
        /// Whether to exempt this user from Enterprise device limits
        /// </summary>
        [JsonProperty(PropertyName = "is_exempt_from_device_limits")]
        public bool IsExemptFromDeviceLimits { get; private set; }

        /// <summary>
        /// Whether or not this user must use two-factor authentication
        /// </summary>
        [JsonProperty(PropertyName = "is_exempt_from_login_verification")]
        public bool IsExemptFromLoginVerification { get; private set; }

        /// <summary>
        /// Mini representation of this user’s enterprise, including the ID of its enterprise
        /// </summary>
        [JsonProperty(PropertyName = "enterprise")]
        public BoxEntity Enterprise { get; private set; }

    }
}
