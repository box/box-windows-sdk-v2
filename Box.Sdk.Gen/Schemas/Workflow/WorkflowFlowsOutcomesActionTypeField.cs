using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum WorkflowFlowsOutcomesActionTypeField {
        [Description("add_metadata")]
        AddMetadata,
        [Description("assign_task")]
        AssignTask,
        [Description("copy_file")]
        CopyFile,
        [Description("copy_folder")]
        CopyFolder,
        [Description("create_folder")]
        CreateFolder,
        [Description("delete_file")]
        DeleteFile,
        [Description("delete_folder")]
        DeleteFolder,
        [Description("lock_file")]
        LockFile,
        [Description("move_file")]
        MoveFile,
        [Description("move_folder")]
        MoveFolder,
        [Description("remove_watermark_file")]
        RemoveWatermarkFile,
        [Description("rename_folder")]
        RenameFolder,
        [Description("restore_folder")]
        RestoreFolder,
        [Description("share_file")]
        ShareFile,
        [Description("share_folder")]
        ShareFolder,
        [Description("unlock_file")]
        UnlockFile,
        [Description("upload_file")]
        UploadFile,
        [Description("wait_for_task")]
        WaitForTask,
        [Description("watermark_file")]
        WatermarkFile,
        [Description("go_back_to_step")]
        GoBackToStep,
        [Description("apply_file_classification")]
        ApplyFileClassification,
        [Description("apply_folder_classification")]
        ApplyFolderClassification,
        [Description("send_notification")]
        SendNotification
    }
}