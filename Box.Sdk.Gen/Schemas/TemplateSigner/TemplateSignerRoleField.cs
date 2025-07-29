using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum TemplateSignerRoleField {
        [Description("signer")]
        Signer,
        [Description("approver")]
        Approver,
        [Description("final_copy_reader")]
        FinalCopyReader
    }
}