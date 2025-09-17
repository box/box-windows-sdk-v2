using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Managers {
    public enum SearchForContentQueryParamsScopeField {
        [Description("user_content")]
        UserContent,
        [Description("enterprise_content")]
        EnterpriseContent
    }
}