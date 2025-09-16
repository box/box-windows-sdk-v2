using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum UpdateTaskAssignmentByIdRequestBodyResolutionStateField {
        [Description("completed")]
        Completed,
        [Description("incomplete")]
        Incomplete,
        [Description("approved")]
        Approved,
        [Description("rejected")]
        Rejected
    }
}