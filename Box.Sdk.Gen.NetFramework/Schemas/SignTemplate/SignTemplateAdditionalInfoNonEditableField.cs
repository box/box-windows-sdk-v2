using System.ComponentModel;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum SignTemplateAdditionalInfoNonEditableField {
        [Description("email_subject")]
        EmailSubject,
        [Description("email_message")]
        EmailMessage,
        [Description("name")]
        Name,
        [Description("days_valid")]
        DaysValid,
        [Description("signers")]
        Signers,
        [Description("source_files")]
        SourceFiles
    }
}