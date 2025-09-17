using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum EventSourceItemTypeField {
        [Description("file")]
        File,
        [Description("folder")]
        Folder
    }
}