using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Managers {
    public enum GetAiAgentDefaultConfigQueryParamsModeField {
        [Description("ask")]
        Ask,
        [Description("text_gen")]
        TextGen,
        [Description("extract")]
        Extract,
        [Description("extract_structured")]
        ExtractStructured
    }
}