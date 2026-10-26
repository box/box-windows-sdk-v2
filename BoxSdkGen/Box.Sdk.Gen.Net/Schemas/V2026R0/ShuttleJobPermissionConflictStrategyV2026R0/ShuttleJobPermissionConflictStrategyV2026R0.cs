using System.ComponentModel;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum ShuttleJobPermissionConflictStrategyV2026R0 {
        [Description("preserve_structure_restrict_sharing")]
        PreserveStructureRestrictSharing,
        [Description("preserve_structure_expand_sharing")]
        PreserveStructureExpandSharing,
        [Description("preserve_sharing_modify_structure")]
        PreserveSharingModifyStructure,
        [Description("warn_permissions_conflicts")]
        WarnPermissionsConflicts
    }
}