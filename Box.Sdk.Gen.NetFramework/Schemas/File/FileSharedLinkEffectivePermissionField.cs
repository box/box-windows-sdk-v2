using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum FileSharedLinkEffectivePermissionField {
        [Description("can_edit")]
        CanEdit,
        [Description("can_download")]
        CanDownload,
        [Description("can_preview")]
        CanPreview,
        [Description("no_access")]
        NoAccess
    }
}