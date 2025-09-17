using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum GetEventsQueryParamsStreamTypeField {
        [Description("all")]
        All,
        [Description("changes")]
        Changes,
        [Description("sync")]
        Sync,
        [Description("admin_logs")]
        AdminLogs,
        [Description("admin_logs_streaming")]
        AdminLogsStreaming
    }
}