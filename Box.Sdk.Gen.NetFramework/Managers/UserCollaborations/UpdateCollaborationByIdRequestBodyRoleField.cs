using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum UpdateCollaborationByIdRequestBodyRoleField {
        [Description("editor")]
        Editor,
        [Description("viewer")]
        Viewer,
        [Description("previewer")]
        Previewer,
        [Description("uploader")]
        Uploader,
        [Description("previewer uploader")]
        PreviewerUploader,
        [Description("viewer uploader")]
        ViewerUploader,
        [Description("co-owner")]
        CoOwner,
        [Description("owner")]
        Owner
    }
}