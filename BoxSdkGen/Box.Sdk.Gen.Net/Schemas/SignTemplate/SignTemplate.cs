using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SignTemplate : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isnameSet")]
        protected bool _isNameSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isemail_subjectSet")]
        protected bool _isEmailSubjectSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isemail_messageSet")]
        protected bool _isEmailMessageSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isdays_validSet")]
        protected bool _isDaysValidSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isready_sign_linkSet")]
        protected bool _isReadySignLinkSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iscustom_brandingSet")]
        protected bool _isCustomBrandingSet { get; set; }

        protected string? _name { get; set; }

        protected string? _emailSubject { get; set; }

        protected string? _emailMessage { get; set; }

        protected long? _daysValid { get; set; }

        protected SignTemplateReadySignLinkField? _readySignLink { get; set; }

        protected SignTemplateCustomBrandingField? _customBranding { get; set; }

        /// <summary>
        /// The value will always be `sign-template`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<SignTemplateTypeField>))]
        public StringEnum<SignTemplateTypeField>? Type { get; init; }

        /// <summary>
        /// Template identifier.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The name of the template.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get => _name; init { _name = value; _isNameSet = true; } }

        /// <summary>
        /// Subject of signature request email. This is cleaned by sign
        /// request. If this field is not passed, a default subject will be used.
        /// </summary>
        [JsonPropertyName("email_subject")]
        public string? EmailSubject { get => _emailSubject; init { _emailSubject = value; _isEmailSubjectSet = true; } }

        /// <summary>
        /// Message to include in signature request email. The field
        /// is cleaned through sanitization of specific characters. However,
        /// some html tags are allowed. Links included in the
        /// message are also converted to hyperlinks in the email. The
        /// message may contain the following html tags including `a`, `abbr`,
        /// `acronym`, `b`, `blockquote`, `code`, `em`, `i`, `ul`, `li`, `ol`, and
        /// `strong`. Be aware that when the text
        /// to html ratio is too high, the email
        /// may end up in spam filters. Custom styles on
        /// these tags are not allowed.
        /// If this field is not passed, a default message will be used.
        /// </summary>
        [JsonPropertyName("email_message")]
        public string? EmailMessage { get => _emailMessage; init { _emailMessage = value; _isEmailMessageSet = true; } }

        /// <summary>
        /// Set the number of days after which the
        /// created signature request will automatically
        /// expire if not completed. By default, we do
        /// not apply any expiration date on signature
        /// requests, and the signature request does not expire.
        /// </summary>
        [JsonPropertyName("days_valid")]
        public long? DaysValid { get => _daysValid; init { _daysValid = value; _isDaysValidSet = true; } }

        [JsonPropertyName("parent_folder")]
        public FolderMini? ParentFolder { get; init; }

        /// <summary>
        /// List of files to create a signing document from.
        /// Only the ID and type fields are required
        /// for each file.
        /// </summary>
        [JsonPropertyName("source_files")]
        public IReadOnlyList<FileMini>? SourceFiles { get; init; }

        /// <summary>
        /// Indicates if the template input
        /// fields are editable or not.
        /// </summary>
        [JsonPropertyName("are_fields_locked")]
        public bool? AreFieldsLocked { get; init; }

        /// <summary>
        /// Indicates if the template document options
        /// are editable or not,
        /// for example renaming the document.
        /// </summary>
        [JsonPropertyName("are_options_locked")]
        public bool? AreOptionsLocked { get; init; }

        /// <summary>
        /// Indicates if the template signers are editable or not.
        /// </summary>
        [JsonPropertyName("are_recipients_locked")]
        public bool? AreRecipientsLocked { get; init; }

        /// <summary>
        /// Indicates if the template email settings are editable or not.
        /// </summary>
        [JsonPropertyName("are_email_settings_locked")]
        public bool? AreEmailSettingsLocked { get; init; }

        /// <summary>
        /// Indicates if the template files are editable or not.
        /// This includes deleting or renaming template files.
        /// </summary>
        [JsonPropertyName("are_files_locked")]
        public bool? AreFilesLocked { get; init; }

        /// <summary>
        /// Array of signers for the template.
        /// 
        /// **Note**: It may happen that some signers specified in the template belong to conflicting [segments](r://shield-information-barrier-segment-member) (user groups).
        /// This means that due to the security policies, users are assigned to segments to prevent exchanges or communication that could lead to ethical conflicts.
        /// In such a case, an attempt to send a sign request based on a template that lists signers in conflicting segments will result in an error.
        /// 
        /// Read more about [segments and ethical walls](https://support.box.com/hc/en-us/articles/9920431507603-Understanding-Information-Barriers#h_01GFVJEHQA06N7XEZ4GCZ9GFAQ).
        /// </summary>
        [JsonPropertyName("signers")]
        public IReadOnlyList<TemplateSigner>? Signers { get; init; }

        /// <summary>
        /// Additional information on which fields are
        /// required and which fields are not editable.
        /// </summary>
        [JsonPropertyName("additional_info")]
        public SignTemplateAdditionalInfoField? AdditionalInfo { get; init; }

        /// <summary>
        /// Box's ready-sign link feature enables you to create a
        /// link to a signature request that
        /// you've created from a template. Use this link
        /// when you want to post a signature request
        /// on a public form — such as an email, social media post,
        /// or web page — without knowing who the signers will be.
        /// Note: The ready-sign link feature is
        /// limited to Enterprise Plus customers and not
        /// available to Box Verified Enterprises.
        /// </summary>
        [JsonPropertyName("ready_sign_link")]
        public SignTemplateReadySignLinkField? ReadySignLink { get => _readySignLink; init { _readySignLink = value; _isReadySignLinkSet = true; } }

        /// <summary>
        /// Custom branding applied to notifications
        /// and signature requests.
        /// </summary>
        [JsonPropertyName("custom_branding")]
        public SignTemplateCustomBrandingField? CustomBranding { get => _customBranding; init { _customBranding = value; _isCustomBrandingSet = true; } }

        public SignTemplate() {
            
        }
        internal string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}