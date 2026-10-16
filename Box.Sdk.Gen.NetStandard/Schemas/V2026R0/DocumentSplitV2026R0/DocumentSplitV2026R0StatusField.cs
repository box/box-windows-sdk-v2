using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum DocumentSplitV2026R0StatusField {
        [Description("submitted")]
        Submitted,
        [Description("completed")]
        Completed,
        [Description("failed")]
        Failed,
        [Description("completed_with_error")]
        CompletedWithError
    }
}