using System.ComponentModel;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum ShuttleJobScheduleTriggerByTimeV2026R0RepeatField {
        [Description("none")]
        None,
        [Description("daily")]
        Daily,
        [Description("weekly")]
        Weekly,
        [Description("weekdays")]
        Weekdays,
        [Description("weekends")]
        Weekends
    }
}