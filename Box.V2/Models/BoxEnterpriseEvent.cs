using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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


        /// <summary>
        /// A mini user object representing the source of the event (file, folder, comment, etc.)
        /// </summary>
        [JsonProperty(PropertyName = FieldSource)]
        public BoxEntity Source { get; set; }

        /// <summary>
        /// A mini user object representing the creator of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedBy)]
        public BoxUser CreatedBy { get; set; }

        /// <summary>
        /// When the event was created
        /// </summary>
        [JsonProperty(PropertyName = FieldCreatedAt)]
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// The unique id of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldEventId)]
        public string EventId { get; private set; }

        /// <summary>
        /// The type of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldEventType)]
        public string EventType { get; private set; }

        /// <summary>
        /// The IP address associated with the creation of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldIPAddress)]
        public string IPAddress { get; private set; }

        /// <summary>
        /// The type of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldType)]
        public string Type { get; private set; }

        /// <summary>
        /// The session Id of the event
        /// </summary>
        [JsonProperty(PropertyName = FieldSessionId)]
        public string SessionId { get; private set; }

        /// <summary>
        /// The additional details of the event
        /// <para>
        /// NOTE: Box returns a variety of differing data for this field with no type indicator so this is being parsed to a Dictionary
        /// </para>
        /// </summary>
        [JsonProperty(PropertyName = FieldAdditionalDetails)]
        public Dictionary<string,object> AdditionalDetails { get; private set; }

    }

    public class BoxLongPollInfo
    {
        public const string FieldType = "type";
        public const string FieldURL = "url";
        public const string FieldTTL = "ttl";
        public const string FieldMaxRetries = "max_retries";
        public const string FieldRetryTimeout = "retry_timeout";

        [JsonProperty(PropertyName = FieldType)]
        public string Type { get; set; }

        [JsonProperty(PropertyName = FieldURL)]
        public Uri Url { get; set; }

        [JsonProperty(PropertyName = FieldTTL)]
        public string TTL { get; set; }

        [JsonProperty(PropertyName = FieldMaxRetries)]
        public string MaxRetries { get; set; }

        [JsonProperty(PropertyName = FieldRetryTimeout)]
        public int RetryTimeout { get; set; }
    }

    public class BoxLongPollMessage
    {
        public const string FieldMessage = "message";

        [JsonProperty(PropertyName = FieldMessage)]
        public string Message { get; set; }
    }
}
