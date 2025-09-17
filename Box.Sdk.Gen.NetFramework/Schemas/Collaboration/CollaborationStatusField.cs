using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum CollaborationStatusField {
        [Description("accepted")]
        Accepted,
        [Description("pending")]
        Pending,
        [Description("rejected")]
        Rejected
    }
}