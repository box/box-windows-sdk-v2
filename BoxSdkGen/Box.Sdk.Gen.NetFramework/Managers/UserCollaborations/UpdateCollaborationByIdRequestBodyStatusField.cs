using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum UpdateCollaborationByIdRequestBodyStatusField {
        [Description("pending")]
        Pending,
        [Description("accepted")]
        Accepted,
        [Description("rejected")]
        Rejected
    }
}