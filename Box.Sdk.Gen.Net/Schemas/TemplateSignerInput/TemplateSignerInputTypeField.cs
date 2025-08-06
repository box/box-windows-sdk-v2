using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum TemplateSignerInputTypeField {
        [Description("signature")]
        Signature,
        [Description("date")]
        Date,
        [Description("text")]
        Text,
        [Description("checkbox")]
        Checkbox,
        [Description("attachment")]
        Attachment,
        [Description("radio")]
        Radio,
        [Description("dropdown")]
        Dropdown
    }
}