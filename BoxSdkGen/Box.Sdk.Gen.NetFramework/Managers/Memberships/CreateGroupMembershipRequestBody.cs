using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class CreateGroupMembershipRequestBody : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isconfigurable_permissionsSet")]
        protected bool _isConfigurablePermissionsSet { get; set; }

        protected Dictionary<string, bool> _configurablePermissions { get; set; }

        /// <summary>
        /// The user to add to the group.
        /// </summary>
        [JsonPropertyName("user")]
        public CreateGroupMembershipRequestBodyUserField User { get; set; }

        /// <summary>
        /// The group to add the user to.
        /// </summary>
        [JsonPropertyName("group")]
        public CreateGroupMembershipRequestBodyGroupField Group { get; set; }

        /// <summary>
        /// The role of the user in the group.
        /// </summary>
        [JsonPropertyName("role")]
        [JsonConverter(typeof(StringEnumConverter<CreateGroupMembershipRequestBodyRoleField>))]
        public StringEnum<CreateGroupMembershipRequestBodyRoleField> Role { get; set; }

        /// <summary>
        /// Custom configuration for the permissions an admin
        /// if a group will receive. This option has no effect
        /// on members with a role of `member`.
        /// 
        /// Setting these permissions overwrites the default
        /// access levels of an admin.
        /// 
        /// Specifying a value of `null` for this object will disable
        /// all configurable permissions. Specifying permissions will set
        /// them accordingly, omitted permissions will be enabled by default.
        /// </summary>
        [JsonPropertyName("configurable_permissions")]
        public Dictionary<string, bool> ConfigurablePermissions { get => _configurablePermissions; set { _configurablePermissions = value; _isConfigurablePermissionsSet = true; } }

        public CreateGroupMembershipRequestBody(CreateGroupMembershipRequestBodyUserField user, CreateGroupMembershipRequestBodyGroupField group) {
            User = user;
            Group = group;
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