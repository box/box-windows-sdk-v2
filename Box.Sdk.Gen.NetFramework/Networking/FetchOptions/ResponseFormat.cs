using System.ComponentModel;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen {
    public enum ResponseFormat {
        [Description("json")]
        Json,
        [Description("binary")]
        Binary,
        [Description("no_content")]
        NoContent
    }
}