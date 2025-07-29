using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum GroupMiniGroupTypeField {
        [Description("managed_group")]
        ManagedGroup,
        [Description("all_users_group")]
        AllUsersGroup
    }
}