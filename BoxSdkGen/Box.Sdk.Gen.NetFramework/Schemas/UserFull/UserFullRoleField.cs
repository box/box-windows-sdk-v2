using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum UserFullRoleField {
        [Description("admin")]
        Admin,
        [Description("coadmin")]
        Coadmin,
        [Description("user")]
        User
    }
}