using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Box.V2.Models
{
    public class BoxEnterpriseEvent
    {
        public const string FieldSource = "source";
        public const string FieldCreatedBy = "created_by";
        public const string FieldCreatedAt = "created_at";
        public const string FieldEventId = "event_id";
        public const string FieldEventType = "event_type";
        public const string FieldIPAddress = "ip_address";
        public const string FieldType = "type";
        public const string FieldSessionId = "session_id";
        public const string FieldAdditionalDetails = "additional_details";
        public const string FieldActionBy = "action_by";


        /// <summary>
        /// A mini user object representing the source of the event (file, folder, comment, etc.)
        /// </summary>
        [JsonProperty(PropertyName = FieldSource)]
        public virtual BoxEntity Source { get; set; }

        /// <summary>
        /// A mini user object representing the creator of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public virtual BoxUser CreatedBy { get; set; }

        /// <summary>
        /// When the event was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public virtual DateTimeOffset? CreatedAt { get; private set; }

        /// <summary>
        /// The unique id of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldEventId)]
        public virtual string EventId { get; private set; }

        /// <summary>
        /// The type of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldEventType)]
        public virtual string EventType { get; private set; }

        /// <summary>
        /// The IP address associated with the creation of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldIPAddress)]
        public virtual string IPAddress { get; private set; }

        /// <summary>
        /// The type of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        public virtual string Type { get; private set; }

        /// <summary>
        /// The session Id of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldSessionId)]
        public virtual string SessionId { get; private set; }

        /// <summary>
        /// The additional details of the event
        /// <para>
        /// NOTE: Box returns a variety of differing data for this field with no type indicator so this is being parsed to a Dictionary
        /// </para>
        /// </summary>
        [JsonProperty(PropertyName = FieldAdditionalDetails)]
        public virtual Dictionary<string, object> AdditionalDetails { get; private set; }

        /// <summary>
        /// The action by field on the event.
        /// </summary>
        [JsonProperty(PropertyName = FieldActionBy)]
        public virtual BoxUser ActionBy { get; private set; }

    }

    public class BoxLongPollInfo
    {
        public const string FieldType = "type";
        public const string FieldURL = "url";
        public const string FieldTTL = "ttl";
        public const string FieldMaxRetries = "max_retries";
        public const string FieldRetryTimeout = "retry_timeout";

        [JsonProperty(PropertyName = FieldType)]
        public virtual string Type { get; set; }

        [JsonProperty(PropertyName = FieldURL)]
        public virtual Uri Url { get; set; }

        [JsonProperty(PropertyName = FieldTTL)]
        public virtual string TTL { get; set; }

        [JsonProperty(PropertyName = FieldMaxRetries)]
        public virtual string MaxRetries { get; set; }

        [JsonProperty(PropertyName = FieldRetryTimeout)]
        public virtual int RetryTimeout { get; set; }
    }

    public class BoxLongPollMessage
    {
        public const string FieldMessage = "message";

        [JsonProperty(PropertyName = FieldMessage)]
        public virtual string Message { get; set; }
    }
}
