using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum UpdateFileMetadataByIdRequestBodyOpField {
        [Description("add")]
        Add,
        [Description("replace")]
        Replace,
        [Description("remove")]
        Remove,
        [Description("test")]
        Test,
        [Description("move")]
        Move,
        [Description("copy")]
        Copy
    }
}