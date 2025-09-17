using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateCollaborationByIdRequestBody : ISerializable {
        /// <summary>
        /// The level of access granted.
        /// </summary>
        [JsonPropertyName("role")]
        [JsonConverter(typeof(StringEnumConverter<UpdateCollaborationByIdRequestBodyRoleField>))]
        public StringEnum<UpdateCollaborationByIdRequestBodyRoleField> Role { get; }

        /// <summary>
        /// Set the status of a `pending` collaboration invitation,
        /// effectively accepting, or rejecting the invite.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<UpdateCollaborationByIdRequestBodyStatusField>))]
        public StringEnum<UpdateCollaborationByIdRequestBodyStatusField>? Status { get; init; }

        /// <summary>
        /// Update the expiration date for the collaboration. At this date,
        /// the collaboration will be automatically removed from the item.
        /// 
        /// This feature will only work if the **Automatically remove invited
        /// collaborators: Allow folder owners to extend the expiry date**
        /// setting has been enabled in the **Enterprise Settings**
        /// of the **Admin Console**. When the setting is not enabled,
        /// collaborations can not have an expiry date and a value for this
        /// field will be result in an error.
        /// 
        /// Additionally, a collaboration can only be given an
        /// expiration if it was created after the **Automatically remove
        /// invited collaborator** setting was enabled.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public System.DateTimeOffset? ExpiresAt { get; init; }

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
        /// Only owner or co-owners can invite collaborators with a `can_view_path` of
        /// `true`.
        /// 
        /// `can_view_path` can only be used for folder collaborations.
        /// </summary>
        [JsonPropertyName("can_view_path")]
        public bool? CanViewPath { get; init; }

        public UpdateCollaborationByIdRequestBody(UpdateCollaborationByIdRequestBodyRoleField role) {
            Role = role;
        }
        
        [JsonConstructorAttribute]
        internal UpdateCollaborationByIdRequestBody(StringEnum<UpdateCollaborationByIdRequestBodyRoleField> role) {
            Role = role;
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