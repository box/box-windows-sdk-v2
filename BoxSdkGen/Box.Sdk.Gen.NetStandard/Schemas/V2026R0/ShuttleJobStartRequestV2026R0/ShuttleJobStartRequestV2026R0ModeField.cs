using System.ComponentModel;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum ShuttleJobStartRequestV2026R0ModeField {
        [Description("transfer")]
        Transfer,
        [Description("simulation")]
        Simulation,
        [Description("scan")]
        Scan,
        [Description("analysis")]
        Analysis
    }
}