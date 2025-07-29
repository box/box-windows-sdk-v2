using System.ComponentModel;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum CollaborationAllowlistEntryDirectionField {
        [Description("inbound")]
        Inbound,
        [Description("outbound")]
        Outbound,
        [Description("both")]
        Both
    }
}