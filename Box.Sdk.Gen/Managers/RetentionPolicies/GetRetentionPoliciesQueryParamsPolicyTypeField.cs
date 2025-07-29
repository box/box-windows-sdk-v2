using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum GetRetentionPoliciesQueryParamsPolicyTypeField {
        [Description("finite")]
        Finite,
        [Description("indefinite")]
        Indefinite
    }
}