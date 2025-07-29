using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequest : SignRequestBase, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_issignature_colorSet")]
        protected bool _isSignatureColorSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isprepare_urlSet")]
        protected bool _isPrepareUrlSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isauto_expire_atSet")]
        protected bool _isAutoExpireAtSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_iscollaborator_levelSet")]
        protected bool _isCollaboratorLevelSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_issender_emailSet")]
        protected bool _isSenderEmailSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_issender_idSet")]
        protected bool _isSenderIdSet { get; set; }

        protected string? _signatureColor { get; set; }

        protected string? _prepareUrl { get; set; }

        protected System.DateTimeOffset? _autoExpireAt { get; set; }

        protected string? _collaboratorLevel { get; set; }

        protected string? _senderEmail { get; set; }

        protected long? _senderId { get; set; }

        /// <summary>
        /// The value will always be `sign-request`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestTypeField>))]
        public StringEnum<SignRequestTypeField>? Type { get; init; }

        /// <summary>
        /// List of files to create a signing document from. This is currently limited to ten files. Only the ID and type fields are required for each file.
        /// </summary>
        [JsonPropertyName("source_files")]
        public IReadOnlyList<FileBase>? SourceFiles { get; init; }

        /// <summary>
        /// Array of signers for the signature request.
        /// </summary>
        [JsonPropertyName("signers")]
        public IReadOnlyList<SignRequestSigner>? Signers { get; init; }

        /// <summary>
        /// Force a specific color for the signature (blue, black, or red).
        /// </summary>
        [JsonPropertyName("signature_color")]
        public string? SignatureColor { get => _signatureColor; init { _signatureColor = value; _isSignatureColorSet = true; } }

        /// <summary>
        /// Box Sign request ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// This URL is returned if `is_document_preparation_needed` is
        /// set to `true` in the request. The parameter is used to prepare
        /// the signature request
        /// using the UI. The signature request is not
        /// sent until the preparation
        /// phase is complete.
        /// </summary>
        [JsonPropertyName("prepare_url")]
        public string? PrepareUrl { get => _prepareUrl; init { _prepareUrl = value; _isPrepareUrlSet = true; } }

        [JsonPropertyName("signing_log")]
        public FileMini? SigningLog { get; init; }

        /// <summary>
        /// Describes the status of the signature request.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestStatusField>))]
        public StringEnum<SignRequestStatusField>? Status { get; init; }

        /// <summary>
        /// List of files that will be signed, which are copies of the original
        /// source files. A new version of these files are created as signers sign
        /// and can be downloaded at any point in the signing process.
        /// </summary>
        [JsonPropertyName("sign_files")]
        public SignRequestSignFilesField? SignFiles { get; init; }

        /// <summary>
        /// Uses `days_valid` to calculate the date and time, in GMT, the sign request will expire if unsigned.
        /// </summary>
        [JsonPropertyName("auto_expire_at")]
        public System.DateTimeOffset? AutoExpireAt { get => _autoExpireAt; init { _autoExpireAt = value; _isAutoExpireAtSet = true; } }

        [JsonPropertyName("parent_folder")]
        public FolderMini? ParentFolder { get; init; }

        /// <summary>
        /// The collaborator level of the user to the sign request. Values can include "owner", "editor", and "viewer".
        /// </summary>
        [JsonPropertyName("collaborator_level")]
        public string? CollaboratorLevel { get => _collaboratorLevel; init { _collaboratorLevel = value; _isCollaboratorLevelSet = true; } }

        /// <summary>
        /// The email address of the sender of the sign request.
        /// </summary>
        [JsonPropertyName("sender_email")]
        public string? SenderEmail { get => _senderEmail; init { _senderEmail = value; _isSenderEmailSet = true; } }

        /// <summary>
        /// The user ID of the sender of the sign request.
        /// </summary>
        [JsonPropertyName("sender_id")]
        public long? SenderId { get => _senderId; init { _senderId = value; _isSenderIdSet = true; } }

        public SignRequest() {
            
        }
        internal new string? RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string? ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object?>? GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}