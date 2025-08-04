using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum UpdateGroupMembershipByIdRequestBodyRoleField {
        [Description("member")]
        Member,
        [Description("admin")]
        Admin
    }
}