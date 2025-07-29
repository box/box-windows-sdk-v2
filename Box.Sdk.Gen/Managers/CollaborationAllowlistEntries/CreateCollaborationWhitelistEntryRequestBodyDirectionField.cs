using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum CreateCollaborationWhitelistEntryRequestBodyDirectionField {
        [Description("inbound")]
        Inbound,
        [Description("outbound")]
        Outbound,
        [Description("both")]
        Both
    }
}