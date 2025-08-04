using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Linq;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class SignTemplateReadySignLinkField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isnameSet")]
        protected bool _isNameSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isinstructionsSet")]
        protected bool _isInstructionsSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isfolder_idSet")]
        protected bool _isFolderIdSet { get; set; }

        protected string? _name { get; set; }

        protected string? _instructions { get; set; }

        protected string? _folderId { get; set; }

        /// <summary>
        /// The URL that can be sent to signers.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        /// <summary>
        /// Request name.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get => _name; init { _name = value; _isNameSet = true; } }

        /// <summary>
        /// Extra instructions for all signers.
        /// </summary>
        [JsonPropertyName("instructions")]
        public string? Instructions { get => _instructions; init { _instructions = value; _isInstructionsSet = true; } }

        /// <summary>
        /// The destination folder to place final,
        /// signed document and signing
        /// log. Only `ID` and `type` fields are required.
        /// The root folder,
        /// folder ID `0`, cannot be used.
        /// </summary>
        [JsonPropertyName("folder_id")]
        public string? FolderId { get => _folderId; init { _folderId = value; _isFolderIdSet = true; } }

        /// <summary>
        /// Whether to disable notifications when
        /// a signer has signed.
        /// </summary>
        [JsonPropertyName("is_notification_disabled")]
        public bool? IsNotificationDisabled { get; init; }

        /// <summary>
        /// Whether the ready sign link is enabled or not.
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool? IsActive { get; init; }

        public SignTemplateReadySignLinkField() {
            
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