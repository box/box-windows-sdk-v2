using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class UserFull : User, ISerializable {
        /// <summary>
        /// The user’s enterprise role.
        /// </summary>
        [JsonPropertyName("role")]
        [JsonConverter(typeof(StringEnumConverter<UserFullRoleField>))]
        public StringEnum<UserFullRoleField>? Role { get; init; }

        /// <summary>
        /// Tracking codes allow an admin to generate reports from the
        /// admin console and assign an attribute to a specific group
        /// of users. This setting must be enabled for an enterprise
        /// before it can be used.
        /// </summary>
        [JsonPropertyName("tracking_codes")]
        public IReadOnlyList<TrackingCode>? TrackingCodes { get; init; }

        /// <summary>
        /// Whether the user can see other enterprise users in their contact list.
        /// </summary>
        [JsonPropertyName("can_see_managed_users")]
        public bool? CanSeeManagedUsers { get; init; }

        /// <summary>
        /// Whether the user can use Box Sync.
        /// </summary>
        [JsonPropertyName("is_sync_enabled")]
        public bool? IsSyncEnabled { get; init; }

        /// <summary>
        /// Whether the user is allowed to collaborate with users outside their
        /// enterprise.
        /// </summary>
        [JsonPropertyName("is_external_collab_restricted")]
        public bool? IsExternalCollabRestricted { get; init; }

        /// <summary>
        /// Whether to exempt the user from Enterprise device limits.
        /// </summary>
        [JsonPropertyName("is_exempt_from_device_limits")]
        public bool? IsExemptFromDeviceLimits { get; init; }

        /// <summary>
        /// Whether the user must use two-factor authentication.
        /// </summary>
        [JsonPropertyName("is_exempt_from_login_verification")]
        public bool? IsExemptFromLoginVerification { get; init; }

        [JsonPropertyName("enterprise")]
        public UserFullEnterpriseField? Enterprise { get; init; }

        /// <summary>
        /// Tags for all files and folders owned by the user. Values returned
        /// will only contain tags that were set by the requester.
        /// </summary>
        [JsonPropertyName("my_tags")]
        public IReadOnlyList<string>? MyTags { get; init; }

        /// <summary>
        /// The root (protocol, subdomain, domain) of any links that need to be
        /// generated for the user.
        /// </summary>
        [JsonPropertyName("hostname")]
        public string? Hostname { get; init; }

        /// <summary>
        /// Whether the user is an App User.
        /// </summary>
        [JsonPropertyName("is_platform_access_only")]
        public bool? IsPlatformAccessOnly { get; init; }

        /// <summary>
        /// An external identifier for an app user, which can be used to look up
        /// the user. This can be used to tie user IDs from external identity
        /// providers to Box users.
        /// </summary>
        [JsonPropertyName("external_app_user_id")]
        public string? ExternalAppUserId { get; init; }

        public UserFull(string id, UserBaseTypeField type = UserBaseTypeField.User) : base(id, type) {
            
        }
        
        [JsonConstructorAttribute]
        internal UserFull(string id, StringEnum<UserBaseTypeField> type) : base(id, type ?? new StringEnum<UserBaseTypeField>(UserBaseTypeField.User)) {
            
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