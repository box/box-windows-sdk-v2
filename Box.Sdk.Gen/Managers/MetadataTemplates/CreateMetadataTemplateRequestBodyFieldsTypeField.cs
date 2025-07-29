using System.ComponentModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public enum CreateMetadataTemplateRequestBodyFieldsTypeField {
        [Description("string")]
        String,
        [Description("float")]
        Float,
        [Description("date")]
        Date,
        [Description("enum")]
        Enum,
        [Description("multiSelect")]
        MultiSelect
    }
}