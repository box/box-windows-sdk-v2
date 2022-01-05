using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxWebhook : BoxEntity
    {
        public const string FieldTarget = "target";
        public const string FieldCreatedBy = "created_by";
        public const string FieldCreatedAt = "created_at";
        public const string FieldAddress = "address";
        public const string FieldTriggers = "triggers";

        /// <summary>
        /// The target item for this webhook
        /// </summary>
        [JsonProperty(PropertyName = FieldTarget)]
        public virtual BoxEntity Target { get; private set; }

        /// <summary>
        /// The user who created this webhook
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public virtual BoxUser CreatedBy { get; private set; }

        /// <summary>
        /// The time the webhook was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The time the webhook was created
        /// </summary>
        [JsonProperty(PropertyName = FieldAddress)]
        public virtual string Address { get; private set; }

        /// <summary>
        /// The available roles that can be used to invite people to the folder
        /// WARNING: This property is still in development and may change!
        /// </summary>
        [JsonProperty(PropertyName = FieldTriggers)]
        public virtual IList<string> Triggers { get; protected set; }
    }
}
