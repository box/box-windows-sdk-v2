using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum LegalHoldPolicyStatusField {
        [Description("active")]
        Active,
        [Description("applying")]
        Applying,
        [Description("releasing")]
        Releasing,
        [Description("released")]
        Released
    }
}