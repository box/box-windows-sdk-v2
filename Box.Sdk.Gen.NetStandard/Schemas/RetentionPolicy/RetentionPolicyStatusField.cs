using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum RetentionPolicyStatusField {
        [Description("active")]
        Active,
        [Description("retired")]
        Retired
    }
}