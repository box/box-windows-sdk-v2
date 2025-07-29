using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum FileFullRepresentationsEntriesStatusStateField {
        [Description("success")]
        Success,
        [Description("viewable")]
        Viewable,
        [Description("pending")]
        Pending,
        [Description("none")]
        None
    }
}