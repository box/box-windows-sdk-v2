using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateCollaborationRequestBody : ISerializable {
        /// <summary>
        /// The item to attach the comment to.
        /// </summary>
        [JsonPropertyName("item")]
        public CreateCollaborationRequestBodyItemField Item { get; set; }

        /// <summary>
        /// The user or group to give access to the item.
        /// </summary>
        [JsonPropertyName("accessible_by")]
        public CreateCollaborationRequestBodyAccessibleByField AccessibleBy { get; set; }

        /// <summary>
        /// The level of access granted.
        /// </summary>
        [JsonPropertyName("role")]
        [JsonConverter(typeof(StringEnumConverter<CreateCollaborationRequestBodyRoleField>))]
        public StringEnum<CreateCollaborationRequestBodyRoleField> Role { get; set; }

        /// <summary>
        /// If set to `true`, collaborators have access to
        /// shared items, but such items won't be visible in the
        /// All Files list. Additionally, collaborators won't
        /// see the path to the root folder for the
        /// shared item.
        /// </summary>
        [JsonPropertyName("is_access_only")]
        public bool? IsAccessOnly { get; set; }

        /// <summary>
        /// Determines if the invited users can see the entire parent path to
        /// the associated folder. The user will not gain privileges in any
        /// parent folder and therefore can not see content the user is not
        /// collaborated on.
        /// 
        /// Be aware that this meaningfully increases the time required to load the
        /// invitee's **All Files** page. We recommend you limit the number of
        /// collaborations with `can_view_path` enabled to 1,000 per user.
        /// 
        /// Only an owner or co-owners can invite collaborators with a `can_view_path` of
        /// `true`. Only an owner can update `can_view_path` on existing collaborations.
        /// 
        /// `can_view_path` can only be used for folder collaborations.
        /// 
        /// When you delete a folder with `can_view_path=true`, collaborators may still see the parent path. 
        /// For instructions on how to remove this, see 
        /// [Even though a folder invited via can_view_path is deleted, the path remains displayed](https://support.box.com/hc/en-us/articles/37472814319891-Even-though-a-folder-invited-via-can-view-path-is-deleted-the-path-remains-displayed).
        /// </summary>
        [JsonPropertyName("can_view_path")]
        public bool? CanViewPath { get; set; }

        /// <summary>
        /// Set the expiration date for the collaboration. At this date, the
        /// collaboration will be automatically removed from the item.
        /// 
        /// This feature will only work if the **Automatically remove invited
        /// collaborators: Allow folder owners to extend the expiry date**
        /// setting has been enabled in the **Enterprise Settings**
        /// of the **Admin Console**. When the setting is not enabled,
        /// collaborations can not have an expiry date and a value for this
        /// field will be result in an error.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public System.DateTimeOffset? ExpiresAt { get; set; }

        public CreateCollaborationRequestBody(CreateCollaborationRequestBodyItemField item, CreateCollaborationRequestBodyAccessibleByField accessibleBy, CreateCollaborationRequestBodyRoleField role) {
            Item = item;
            AccessibleBy = accessibleBy;
            Role = role;
        }
        
        [JsonConstructorAttribute]
        internal CreateCollaborationRequestBody(CreateCollaborationRequestBodyItemField item, CreateCollaborationRequestBodyAccessibleByField accessibleBy, StringEnum<CreateCollaborationRequestBodyRoleField> role) {
            Item = item;
            AccessibleBy = accessibleBy;
            Role = role;
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}