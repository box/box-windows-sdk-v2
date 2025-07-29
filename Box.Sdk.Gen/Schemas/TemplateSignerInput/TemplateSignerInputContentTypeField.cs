using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum TemplateSignerInputContentTypeField {
        [Description("signature")]
        Signature,
        [Description("initial")]
        Initial,
        [Description("stamp")]
        Stamp,
        [Description("date")]
        Date,
        [Description("checkbox")]
        Checkbox,
        [Description("text")]
        Text,
        [Description("full_name")]
        FullName,
        [Description("first_name")]
        FirstName,
        [Description("last_name")]
        LastName,
        [Description("company")]
        Company,
        [Description("title")]
        Title,
        [Description("email")]
        Email,
        [Description("attachment")]
        Attachment,
        [Description("radio")]
        Radio,
        [Description("dropdown")]
        Dropdown
    }
}