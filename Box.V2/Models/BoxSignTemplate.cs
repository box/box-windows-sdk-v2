using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// A standard representation of a sign template, as returned from any Box Sign Templates API endpoints by default.
    /// </summary>
    public class BoxSignTemplate : BoxEntity
    {
        public const string FieldAdditionalInfo = "additional_info";
        public const string FieldAreEmailSettingsLocked = "are_email_settings_locked";
        public const string FieldAreFieldsLocked = "are_fields_locked";
        public const string FieldAreFilesLocked = "are_files_locked";
        public const string FieldAreOptionsLocked = "are_options_locked";
        public const string FieldAreRecipientsLocked = "are_recipients_locked";
        public const string FieldCustomBranding = "custom_branding";
        public const string FieldDaysValid = "days_valid";
        public const string FieldEmailMessage = "email_message";
        public const string FieldEmailSubject = "email_subject";
        public const string FieldName = "name";
        public const string FieldParentFolder = "parent_folder";
        public const string FieldReadySignLink = "ready_sign_link";
        public const string FieldSigners = "signers";
        public const string FieldSourceFiles = "source_files";

        /// <summary>
        /// Additional information on which fields are required and which fields are not editable.
        /// </summary>
        [JsonProperty(PropertyName = FieldAdditionalInfo)]
        public virtual BoxSignTemplateAdditionalInfo AdditionalInfo { get; private set; }

        /// <summary>
        /// Indicates if the template email settings are editable or not.
        /// </summary>
        [JsonProperty(PropertyName = FieldAreEmailSettingsLocked)]
        public virtual bool AreEmailSettingsLocked { get; private set; }

        /// <summary>
        /// Indicates if the template input fields are editable or not.
        /// </summary>
        [JsonProperty(PropertyName = FieldAreFieldsLocked)]
        public virtual bool AreFieldsLocked { get; private set; }

        /// <summary>
        /// Indicates if the template files are editable or not. This includes deleting or renaming template files.
        /// </summary>
        [JsonProperty(PropertyName = FieldAreFilesLocked)]
        public virtual bool AreFilesLocked { get; private set; }

        /// <summary>
        /// Indicates if the template document options are editable or not, for example renaming the document.
        /// </summary>
        [JsonProperty(PropertyName = FieldAreOptionsLocked)]
        public virtual bool AreOptionsLocked { get; private set; }

        /// <summary>
        /// Indicates if the template signers are editable or not.
        /// </summary>
        [JsonProperty(PropertyName = FieldAreRecipientsLocked)]
        public virtual bool AreRecipientsLocked { get; private set; }

        /// <summary>
        /// Custom branding applied to notifications and signature requests.
        /// </summary>
        [JsonProperty(PropertyName = FieldCustomBranding)]
        public virtual BoxSignTemplateCustomBranding CustomBranding { get; private set; }

        /// <summary>
        /// Set the number of days after which the created signature request will automatically expire if not completed.
        /// By default, we do not apply any expiration date on signature requests, and the signature request does not expire.
        /// </summary>
        [JsonProperty(PropertyName = FieldDaysValid)]
        public virtual int? DaysValid { get; private set; }

        /// <summary>
        /// Message to include in signature request email. The field is cleaned through sanitization of specific characters.
        /// However, some html tags are allowed. Links included in the message are also converted to hyperlinks in the email.
        /// The message may contain the following html tags including a, abbr, acronym, b, blockquote, code, em, i, ul, li, ol, and strong.
        /// Be aware that when the text to html ratio is too high, the email may end up in spam filters. Custom styles on these tags are not allowed.
        /// If this field is not passed, a default message will be used.
        /// </summary>
        [JsonProperty(PropertyName = FieldEmailMessage)]
        public virtual string EmailMessage { get; private set; }

        /// <summary>
        /// Subject of signature request email. This is cleaned by signature request. If this field is not passed, a default subject will be used.
        /// </summary>
        [JsonProperty(PropertyName = FieldEmailSubject)]
        public virtual string EmailSubject { get; private set; }

        /// <summary>
        /// Name of the template.
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual string Name { get; private set; }

        /// <summary>
        /// The destination folder to place final, signed document and signing log.
        /// </summary>
        [JsonProperty(PropertyName = FieldParentFolder)]
        public virtual BoxFolder ParentFolder { get; private set; }

        /// <summary>
        /// Box's ready-sign link feature enables you to create a link to a signature request that you've created from a template.
        /// Use this link when you want to post a signature request on a public form — such as an email, social media post,
        /// or web page — without knowing who the signers will be.
        /// Note: The ready-sign link feature is limited to Enterprise Plus customers and not available to Box Verified Enterprises.
        /// </summary>
        [JsonProperty(PropertyName = FieldReadySignLink)]
        public virtual BoxSignTemplateReadySignLink ReadySignLink { get; private set; }

        /// <summary>
        /// List of signers for the template.
        /// </summary>
        [JsonProperty(PropertyName = FieldSigners)]
        public virtual List<BoxSignTemplateSigner> Signers { get; private set; }

        /// <summary>
        /// List of files to be used in the template.
        /// </summary>
        [JsonProperty(PropertyName = FieldSourceFiles)]
        public virtual List<BoxFile> SourceFiles { get; private set; }
    }
}
