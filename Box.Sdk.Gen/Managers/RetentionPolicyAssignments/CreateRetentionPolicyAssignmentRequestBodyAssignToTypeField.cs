using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum CreateRetentionPolicyAssignmentRequestBodyAssignToTypeField {
        [Description("enterprise")]
        Enterprise,
        [Description("folder")]
        Folder,
        [Description("metadata_template")]
        MetadataTemplate
    }
}