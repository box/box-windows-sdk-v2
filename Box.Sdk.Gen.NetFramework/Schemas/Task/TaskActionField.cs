using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum TaskActionField {
        [Description("review")]
        Review,
        [Description("complete")]
        Complete
    }
}