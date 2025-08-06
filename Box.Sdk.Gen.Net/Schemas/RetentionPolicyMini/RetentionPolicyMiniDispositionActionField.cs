using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum RetentionPolicyMiniDispositionActionField {
        [Description("permanently_delete")]
        PermanentlyDelete,
        [Description("remove_retention")]
        RemoveRetention
    }
}