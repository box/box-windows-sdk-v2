using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class Event : ISerializable {
        /// <summary>
        /// The value will always be `event`.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// When the event object was created.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// When the event object was recorded in database.
        /// </summary>
        [JsonPropertyName("recorded_at")]
        public System.DateTimeOffset? RecordedAt { get; set; }

        /// <summary>
        /// The ID of the event object. You can use this to detect duplicate events.
        /// </summary>
        [JsonPropertyName("event_id")]
        public string EventId { get; set; }

        [JsonPropertyName("created_by")]
        public UserMini CreatedBy { get; set; }

        [JsonPropertyName("event_type")]
        [JsonConverter(typeof(StringEnumConverter<EventEventTypeField>))]
        public StringEnum<EventEventTypeField> EventType { get; set; }

        /// <summary>
        /// The session of the user that performed the action. Not all events will
        /// populate this attribute.
        /// </summary>
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }

        [JsonPropertyName("source")]
        public EventSourceResource Source { get; set; }

        /// <summary>
        /// This object provides additional information about the event if available.
        /// 
        /// This can include how a user performed an event as well as additional
        /// information to correlate an event to external KeySafe logs. Not all events
        /// have an `additional_details` object.  This object is only available in the
        /// Enterprise Events.
        /// </summary>
        [JsonPropertyName("additional_details")]
        [JsonConverter(typeof(DictionaryObjectValuesConverter))]
        public Dictionary<string, object> AdditionalDetails { get; set; }

        public Event() {
            
        }
        internal string RawJson { get; set; } = default;

        void ISerializable.SetJson(string json) {
            RawJson = json;
        }

        string ISerializable.GetJson() {
            return RawJson;
        }

        /// <summary>
        /// Returns raw json response returned from the API.
        /// </summary>
        public Dictionary<string, object> GetRawData() {
            return SimpleJsonSerializer.GetAllFields(this);
        }

    }
}