using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum GetLegalHoldPolicyAssignmentsQueryParamsAssignToTypeField {
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