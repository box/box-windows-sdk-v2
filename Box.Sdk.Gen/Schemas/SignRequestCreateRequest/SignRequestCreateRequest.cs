using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SignRequestCreateRequest : SignRequestBase, ISerializable {
        [JsonInclude]
        [JsonPropertyName("_issource_filesSet")]
        protected bool _isSourceFilesSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_issignature_colorSet")]
        protected bool _isSignatureColorSet { get; set; }

        protected IReadOnlyList<FileBase>? _sourceFiles { get; set; }

        protected StringEnum<SignRequestCreateRequestSignatureColorField>? _signatureColor { get; set; }

        /// <summary>
        /// List of files to create a signing document from. This is currently limited to ten files. Only the ID and type fields are required for each file.
        /// </summary>
        [JsonPropertyName("source_files")]
        public IReadOnlyList<FileBase>? SourceFiles { get => _sourceFiles; init { _sourceFiles = value; _isSourceFilesSet = true; } }

        /// <summary>
        /// Force a specific color for the signature (blue, black, or red).
        /// </summary>
        [JsonPropertyName("signature_color")]
        [JsonConverter(typeof(StringEnumConverter<SignRequestCreateRequestSignatureColorField>))]
        public StringEnum<SignRequestCreateRequestSignatureColorField>? SignatureColor { get => _signatureColor; init { _signatureColor = value; _isSignatureColorSet = true; } }

        /// <summary>
        /// Array of signers for the signature request. 35 is the
        /// max number of signers permitted.
        /// 
        /// **Note**: It may happen that some signers belong to conflicting [segments](r://shield-information-barrier-segment-member) (user groups).
        /// This means that due to the security policies, users are assigned to segments to prevent exchanges or communication that could lead to ethical conflicts.
        /// In such a case, an attempt to send the sign request will result in an error.
        /// 
        /// Read more about [segments and ethical walls](https://support.box.com/hc/en-us/articles/9920431507603-Understanding-Information-Barriers#h_01GFVJEHQA06N7XEZ4GCZ9GFAQ).
        /// </summary>
        [JsonPropertyName("signers")]
        public IReadOnlyList<SignRequestCreateSigner> Signers { get; }

        [JsonPropertyName("parent_folder")]
        public FolderMini? ParentFolder { get; init; }

        public SignRequestCreateRequest(IReadOnlyList<SignRequestCreateSigner> signers) {
            Signers = signers;
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