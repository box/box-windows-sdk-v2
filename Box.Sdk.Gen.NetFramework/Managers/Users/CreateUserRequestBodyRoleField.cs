using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum CreateUserRequestBodyRoleField {
        [Description("coadmin")]
        Coadmin,
        [Description("user")]
        User
    }
}