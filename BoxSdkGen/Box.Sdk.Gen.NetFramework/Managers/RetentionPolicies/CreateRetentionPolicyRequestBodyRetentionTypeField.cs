using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum CreateRetentionPolicyRequestBodyRetentionTypeField {
        [Description("modifiable")]
        Modifiable,
        [Description("non_modifiable")]
        NonModifiable
    }
}