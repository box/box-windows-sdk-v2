using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum FileFullSharedLinkPermissionOptionsField {
        [Description("can_preview")]
        CanPreview,
        [Description("can_download")]
        CanDownload,
        [Description("can_edit")]
        CanEdit
    }
}