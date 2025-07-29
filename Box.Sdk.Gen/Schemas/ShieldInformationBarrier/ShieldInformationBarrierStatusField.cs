using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum ShieldInformationBarrierStatusField {
        [Description("draft")]
        Draft,
        [Description("pending")]
        Pending,
        [Description("disabled")]
        Disabled,
        [Description("enabled")]
        Enabled,
        [Description("invalid")]
        Invalid
    }
}