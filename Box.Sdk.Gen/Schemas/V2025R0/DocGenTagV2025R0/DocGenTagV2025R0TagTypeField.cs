using System.ComponentModel;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum DocGenTagV2025R0TagTypeField {
        [Description("text")]
        Text,
        [Description("arithmetic")]
        Arithmetic,
        [Description("conditional")]
        Conditional,
        [Description("for-loop")]
        ForLoop,
        [Description("table-loop")]
        TableLoop,
        [Description("image")]
        Image
    }
}