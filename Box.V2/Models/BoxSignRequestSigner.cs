using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    /// <summary>
    /// Signers for the sign request.
    /// </summary>
    public class BoxSignRequestSigner
    {
        public const string FieldEmail = "email";
        public const string FieldEmbedUrl = "embed_url";
        public const string FieldEmbedUrlExternalUserId = "embed_url_external_user_id";
        public const string FieldHasViewedDocument = "has_viewed_document";
        public const string FieldInputs = "inputs";
        public const string FieldIsInPerson = "is_in_person";
        public const string FieldOrder = "order";
        public const string FieldRole = "role";
        public const string FieldSignerDecision = "signer_decision";
        public const string FieldDeclinedRedirectUrl = "declined_redirect_url";
        public const string FieldRedirectUrl = "redirect_url";
        public const string FieldIframeableEmbedUrl = "iframeable_embed_url";
        public const string FieldLoginRequired = "login_required";
        public const string FieldPassword = "password";
        public const string FieldSignerGroupId = "signer_group_id";
        public const string FieldVerificationPhoneNumber = "verification_phone_number";


        /// <summary>
        /// Email address of the signer.
        /// </summary>
        [JsonProperty(PropertyName = FieldEmail)]
        public virtual string Email { get; private set; }

        /// <summary>
        /// URL to direct a signer to for signing.
        /// </summary>
        [JsonProperty(PropertyName = FieldEmbedUrl)]
        public virtual string EmbedUrl { get; private set; }

        /// <summary>
        /// User ID for the signer in an external application responsible for authentication when accessing the embed URL.
        /// </summary>
        [JsonProperty(PropertyName = FieldEmbedUrlExternalUserId)]
        public virtual string EmbedUrlExternalUserId { get; private set; }

        /// <summary>
        /// Set to true if the signer views the document
        /// </summary>
        [JsonProperty(PropertyName = FieldHasViewedDocument)]
        public virtual bool HasViewedDocument { get; private set; }

        /// <summary>
        /// Represents a type of inputs.
        /// </summary>
        [JsonProperty(PropertyName = FieldInputs)]
        public virtual List<BoxSignRequestSignerInput> Inputs { get; private set; }

        /// <summary>
        /// Used in combination with an embed URL for a sender. After the sender signs, they will be redirected to the next in_person signer.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsInPerson)]
        public virtual bool IsInPerson { get; private set; }

        /// <summary>
        /// Order of the signer.
        /// </summary>
        [JsonProperty(PropertyName = FieldOrder)]
        public virtual int Order { get; private set; }

        /// <summary>
        /// Defines the role of the signer in the sign request. A signer must sign the document and an approver must approve the document.
        /// A final_copy_reader only receives the final signed document and signing log.
        /// Value is one of signer,approver,final_copy_reader
        /// </summary>
        [JsonProperty(PropertyName = FieldRole)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual BoxSignRequestSignerRole Role { get; private set; }

        /// <summary>
        /// Final decision made by the signer.
        /// </summary>
        [JsonProperty(PropertyName = FieldSignerDecision)]
        public virtual BoxSignRequestSignerDecision SignerDecision { get; private set; }

        /// <summary>
        /// URL to redirect the signer to if they decline to sign the document.
        /// </summary>
        [JsonProperty(PropertyName = FieldDeclinedRedirectUrl)]
        public virtual Uri DeclinedRedirectUrl { get; private set; }

        /// <summary>
        /// URL to redirect the signer to after they sign the document.
        /// </summary>
        [JsonProperty(PropertyName = FieldRedirectUrl)]
        public virtual Uri RedirectUrl { get; private set; }

        /// <summary>
        /// This URL is specifically designed for signing documents within an HTML iframe tag.
        /// </summary>
        [JsonProperty(PropertyName = FieldIframeableEmbedUrl)]
        public virtual string IframeableEmbedUrl { get; private set; }

        /// <summary>
        /// If set to true, signer will need to login to a Box account before signing the request.
        /// If the signer does not have an existing account, they will have an option to create a free Box account.
        /// </summary>
        [JsonProperty(PropertyName = FieldLoginRequired)]
        public virtual bool LoginRequired { get; private set; }

        /// <summary>
        /// If set, the signer is required to enter the password before they are able to sign a document. This field is write only.
        /// </summary>
        [JsonProperty(PropertyName = FieldPassword)]
        public virtual string Password { get; private set; }

        /// <summary>
        /// If set, signers who have the same group ID will be assigned to the same input.
        /// A signer group is expected to have more than one signer.
        /// When a group contains fewer than two signers, it will be converted to a single signer and the group will be removed.
        /// </summary>
        [JsonProperty(PropertyName = FieldSignerGroupId)]
        public virtual string SignerGroupId { get; private set; }

        /// <summary>
        /// If set, this phone number is be used to verify the signer via two factor authentication before they are able to sign the document.
        /// </summary>
        [JsonProperty(PropertyName = FieldVerificationPhoneNumber)]
        public virtual string VerificationPhoneNumber { get; private set; }
    }

    /// <summary>
    /// Defines the role of the signer in the sign request. A signer must sign the document and an approver must approve the document.
    /// A final_copy_reader only receives the final signed document and signing log.
    /// </summary>
    public enum BoxSignRequestSignerRole
    {
        signer,
        approver,
        final_copy_reader
    }

    /// <summary>
    /// Represents a type of input.
    /// </summary>
    public class BoxSignRequestSignerInput
    {
        public const string FieldType = "type";
        public const string FieldCheckboxValue = "checkbox_value";
        public const string FieldContentType = "content_type";
        public const string FieldDateValue = "date_value";
        public const string FieldDocumentTagId = "document_tag_id";
        public const string FieldPageIndex = "page_index";
        public const string FieldTextValue = "text_value";

        /// <summary>
        /// Type of input.
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual BoxSignRequestSingerInputType Type { get; private set; }

        /// <summary>
        /// Checkbox prefill value.
        /// </summary>
        [JsonProperty(PropertyName = FieldCheckboxValue)]
        public virtual bool? CheckboxValue { get; private set; }

        /// <summary>
        /// Content type of input.
        /// </summary>
        [JsonProperty(PropertyName = FieldContentType)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual BoxSignRequestSingerInputContentType ContentType { get; private set; }

        /// <summary>
        /// Date prefill value.
        /// </summary>
        [JsonProperty(PropertyName = FieldDateValue)]
        public virtual DateTimeOffset? DateValue { get; private set; }

        /// <summary>
        /// This references the ID of a specific tag contained in a file of the sign request.
        /// </summary>
        [JsonProperty(PropertyName = FieldDocumentTagId)]
        public virtual string DocumentTagId { get; private set; }

        /// <summary>
        /// Index of page that the input is on.
        /// </summary>
        [JsonProperty(PropertyName = FieldPageIndex)]
        public virtual int PageIndex { get; private set; }

        /// <summary>
        /// Text prefill value.
        /// </summary>
        [JsonProperty(PropertyName = FieldTextValue)]
        public virtual string TextValue { get; private set; }
    }

    /// <summary>
    /// Type of input.
    /// </summary>
    public enum BoxSignRequestSingerInputType
    {
        signature,
        date,
        text,
        checkbox
    }

    /// <summary>
    /// Content type of input.
    /// </summary>
    public enum BoxSignRequestSingerInputContentType
    {
        initial,
        stamp,
        signature,
        company,
        title,
        email,
        full_name,
        first_name,
        last_name,
        text,
        date,
        checkbox,
        attachment
    }

    /// <summary>
    /// Final decision made by the signer.
    /// </summary>
    public class BoxSignRequestSignerDecision
    {
        public const string FieldType = "type";
        public const string FieldFinalizedAt = "finalized_at";

        /// <summary>
        /// Type of decision made by the signer.
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        [JsonConverter(typeof(StringEnumConverter))]
        public virtual BoxSignRequestSingerDecisionType Type { get; private set; }

        /// <summary>
        /// Date and Time that the decision was made.
        /// </summary>
        [JsonProperty(PropertyName = FieldFinalizedAt)]
        public virtual DateTimeOffset? FinalizedAt { get; private set; }
    }

    /// <summary>
    /// Type of decision made by the signer.
    /// </summary>
    public enum BoxSignRequestSingerDecisionType
    {
        signed,
        declined,
    }
}
