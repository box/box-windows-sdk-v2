using System.ComponentModel;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum LegalHoldPolicyAssignedItemTypeField {
        [Description("file")]
        File,
        [Description("file_version")]
        FileVersion,
        [Description("folder")]
        Folder,
        [Description("user")]
        User,
        [Description("ownership")]
        Ownership,
        [Description("interactions")]
        Interactions
    }
}