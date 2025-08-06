using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum GroupMembershipRoleField {
        [Description("member")]
        Member,
        [Description("admin")]
        Admin
    }
}