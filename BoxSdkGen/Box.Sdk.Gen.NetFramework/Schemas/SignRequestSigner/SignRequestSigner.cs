using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestSigner : SignRequestCreateSigner, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_issigner_decisionSet")]
        protected bool _isSignerDecisionSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isembed_urlSet")]
        protected bool _isEmbedUrlSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isiframeable_embed_urlSet")]
        protected bool _isIframeableEmbedUrlSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isattachmentsSet")]
        protected bool _isAttachmentsSet { get; set; }

        protected SignRequestSignerSignerDecisionField _signerDecision { get; set; }

        protected string _embedUrl { get; set; }

        protected string _iframeableEmbedUrl { get; set; }

        protected IReadOnlyList<SignRequestSignerAttachment> _attachments { get; set; }

        /// <summary>
        /// Set to `true` if the signer views the document.
        /// </summary>
        [JsonPropertyName("has_viewed_document")]
        public bool? HasViewedDocument { get; set; }

        /// <summary>
        /// Final decision made by the signer.
        /// </summary>
        [JsonPropertyName("signer_decision")]
        public SignRequestSignerSignerDecisionField SignerDecision { get => _signerDecision; set { _signerDecision = value; _isSignerDecisionSet = true; } }

        [JsonPropertyName("inputs")]
        public IReadOnlyList<SignRequestSignerInput> Inputs { get; set; }

        /// <summary>
        /// URL to direct a signer to for signing.
        /// </summary>
        [JsonPropertyName("embed_url")]
        public string EmbedUrl { get => _embedUrl; set { _embedUrl = value; _isEmbedUrlSet = true; } }

        /// <summary>
        /// This URL is specifically designed for
        /// signing documents within an HTML `iframe` tag.
        /// It will be returned in the response
        /// only if the `embed_url_external_user_id`
        /// parameter was passed in the
        /// `create Box Sign request` call.
        /// </summary>
        [JsonPropertyName("iframeable_embed_url")]
        public string IframeableEmbedUrl { get => _iframeableEmbedUrl; set { _iframeableEmbedUrl = value; _isIframeableEmbedUrlSet = true; } }

        /// <summary>
        /// Attachments that the signer uploaded.
        /// </summary>
        [JsonPropertyName("attachments")]
        public IReadOnlyList<SignRequestSignerAttachment> Attachments { get => _attachments; set { _attachments = value; _isAttachmentsSet = true; } }

        public SignRequestSigner() {
            
        }
        internal new string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public new Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}