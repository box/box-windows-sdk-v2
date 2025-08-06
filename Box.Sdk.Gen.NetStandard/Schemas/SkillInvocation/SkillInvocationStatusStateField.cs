using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum SkillInvocationStatusStateField {
        [Description("invoked")]
        Invoked,
        [Description("processing")]
        Processing,
        [Description("success")]
        Success,
        [Description("transient_failure")]
        TransientFailure,
        [Description("permanent_failure")]
        PermanentFailure
    }
}