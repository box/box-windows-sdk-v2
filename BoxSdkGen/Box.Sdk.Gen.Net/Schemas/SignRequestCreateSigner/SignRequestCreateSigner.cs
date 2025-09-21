using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestCreateSigner : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isemailSet")]
        protected bool _isEmailSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isembed_url_external_user_idSet")]
        protected bool _isEmbedUrlExternalUserIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isredirect_urlSet")]
        protected bool _isRedirectUrlSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isdeclined_redirect_urlSet")]
        protected bool _isDeclinedRedirectUrlSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_islogin_requiredSet")]
        protected bool _isLoginRequiredSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isverification_phone_numberSet")]
        protected bool _isVerificationPhoneNumberSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_ispasswordSet")]
        protected bool _isPasswordSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_issigner_group_idSet")]
        protected bool _isSignerGroupIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_issuppress_notificationsSet")]
        protected bool _isSuppressNotificationsSet { get; set; }

        protected string? _email { get; set; }

        protected string? _embedUrlExternalUserId { get; set; }

        protected string? _redirectUrl { get; set; }

        protected string? _declinedRedirectUrl { get; set; }

        protected bool? _loginRequired { get; set; }

        protected string? _verificationPhoneNumber { get; set; }

        protected string? _password { get; set; }

        protected string? _signerGroupId { get; set; }

        protected bool? _suppressNotifications { get; set; }

        /// <summary>
        /// Email address of the signer.
        /// The email address of the signer is required when making signature requests, except when using templates that are configured to include emails.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get => _email; init { _email = value; _isEmailSet = true; } }

        /// <summary>
        /// Defines the role of the signer in the signature request. A `signer`
        /// must sign the document and an `approver` must approve the document. A
        /// `final_copy_reader` only receives the final signed document and signing
        /// log.
        /// </summary>
        [JsonPropertyName("role")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestCreateSignerRoleField>))]
        public StringEnum<SignRequestCreateSignerRoleField>? Role { get; init; }

        /// <summary>
        /// Used in combination with an embed URL for a sender. After the
        /// sender signs, they are redirected to the next `in_person` signer.
        /// </summary>
        [JsonPropertyName("is_in_person")]
        public bool? IsInPerson { get; init; }

        /// <summary>
        /// Order of the signer.
        /// </summary>
        [JsonPropertyName("order")]
        public long? Order { get; init; }

        /// <summary>
        /// User ID for the signer in an external application responsible
        /// for authentication when accessing the embed URL.
        /// </summary>
        [JsonPropertyName("embed_url_external_user_id")]
        public string? EmbedUrlExternalUserId { get => _embedUrlExternalUserId; init { _embedUrlExternalUserId = value; _isEmbedUrlExternalUserIdSet = true; } }

        /// <summary>
        /// The URL that a signer will be redirected
        /// to after signing a document. Defining this URL
        /// overrides default or global redirect URL
        /// settings for a specific signer.
        /// If no declined redirect URL is specified,
        /// this URL will be used for decline actions as well.
        /// </summary>
        [JsonPropertyName("redirect_url")]
        public string? RedirectUrl { get => _redirectUrl; init { _redirectUrl = value; _isRedirectUrlSet = true; } }

        /// <summary>
        /// The URL that a signer will be redirect
        /// to after declining to sign a document.
        /// Defining this URL overrides default or global
        /// declined redirect URL settings for a specific signer.
        /// </summary>
        [JsonPropertyName("declined_redirect_url")]
        public string? DeclinedRedirectUrl { get => _declinedRedirectUrl; init { _declinedRedirectUrl = value; _isDeclinedRedirectUrlSet = true; } }

        /// <summary>
        /// If set to true, the signer will need to log in to a Box account
        /// before signing the request. If the signer does not have
        /// an existing account, they will have the option to create
        /// a free Box account.
        /// </summary>
        [JsonPropertyName("login_required")]
        public bool? LoginRequired { get => _loginRequired; init { _loginRequired = value; _isLoginRequiredSet = true; } }

        /// <summary>
        /// If set, this phone number will be used to verify the signer
        /// via two-factor authentication before they are able to sign the document.
        /// Cannot be selected in combination with `login_required`.
        /// </summary>
        [JsonPropertyName("verification_phone_number")]
        public string? VerificationPhoneNumber { get => _verificationPhoneNumber; init { _verificationPhoneNumber = value; _isVerificationPhoneNumberSet = true; } }

        /// <summary>
        /// If set, the signer is required to enter the password before they are able
        /// to sign a document. This field is write only.
        /// </summary>
        [JsonPropertyName("password")]
        public string? Password { get => _password; init { _password = value; _isPasswordSet = true; } }

        /// <summary>
        /// If set, signers who have the same value will be assigned to the same input and to the same signer group.
        /// A signer group is not a Box Group. It is an entity that belongs to a Sign Request and can only be
        /// used/accessed within this Sign Request. A signer group is expected to have more than one signer.
        /// If the provided value is only used for one signer, this value will be ignored and request will be handled
        /// as it was intended for an individual signer. The value provided can be any string and only used to
        /// determine which signers belongs to same group. A successful response will provide a generated UUID value
        /// instead for signers in the same signer group.
        /// </summary>
        [JsonPropertyName("signer_group_id")]
        public string? SignerGroupId { get => _signerGroupId; init { _signerGroupId = value; _isSignerGroupIdSet = true; } }

        /// <summary>
        /// If true, no emails about the sign request will be sent.
        /// </summary>
        [JsonPropertyName("suppress_notifications")]
        public bool? SuppressNotifications { get => _suppressNotifications; init { _suppressNotifications = value; _isSuppressNotificationsSet = true; } }

        public SignRequestCreateSigner() {
            
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