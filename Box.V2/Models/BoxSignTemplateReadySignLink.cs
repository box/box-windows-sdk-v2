using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Box.V2.Models
{
    // <summary>
    // Box's ready-sign link feature enables you to create a link to a signature request that you've created from a template.
    // </summary>
    public class BoxSignTemplateReadySignLink
    {
        public const string FieldFolderId = "folder_id";
        public const string FieldInstructions = "instructions";
        public const string FieldIsActive = "is_active";
        public const string FieldIsNotificationDisabled = "is_notification_disabled";
        public const string FieldName = "name";
        public const string FieldUrl = "url";

        /// <summary>
        /// The destination folder to place final, signed document and signing log.
        /// </summary>
        [JsonProperty(PropertyName = FieldFolderId)]
        public virtual string FolderId { get; private set; }

        /// <summary>
        /// Extra instructions for all signers.
        /// </summary>
        [JsonProperty(PropertyName = FieldInstructions)]
        public virtual string Instructions { get; private set; }

        /// <summary>
        /// Whether the ready sign link is enabled or not.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsActive)]
        public virtual bool IsActive { get; private set; }

        /// <summary>
        /// Whether to disable notifications when a signer has signed.
        /// </summary>
        [JsonProperty(PropertyName = FieldIsNotificationDisabled)]
        public virtual bool IsNotificationDisabled { get; private set; }

        /// <summary>
        /// The name of the ready sign link.
        /// </summary>
        [JsonProperty(PropertyName = FieldName)]
        public virtual string Name { get; private set; }

        /// <summary>
        /// The URL that can be sent to signers.
        /// </summary>
        [JsonProperty(PropertyName = FieldUrl)]
        public virtual string Url { get; private set; }
    }
}
