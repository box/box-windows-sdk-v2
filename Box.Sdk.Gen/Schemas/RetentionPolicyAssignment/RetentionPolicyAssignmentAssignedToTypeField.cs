using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum RetentionPolicyAssignmentAssignedToTypeField {
        [Description("folder")]
        Folder,
        [Description("enterprise")]
        Enterprise,
        [Description("metadata_template")]
        MetadataTemplate
    }
}