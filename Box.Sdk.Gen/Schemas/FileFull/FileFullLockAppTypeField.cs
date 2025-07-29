using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum FileFullLockAppTypeField {
        [Description("gsuite")]
        Gsuite,
        [Description("office_wopi")]
        OfficeWopi,
        [Description("office_wopiplus")]
        OfficeWopiplus,
        [Description("other")]
        Other
    }
}