using Newtonsoft.Json;
using System.Collections.Generic;

namespace Box.V2.Models
{
    /// <summary>
    /// Box representation of a collection
    /// </summary>
    public abstract class BoxCollection
    {
        public const string FieldTotalCount = "total_count";
        public const string FieldEntries = "entries";
        public const string FieldOffset = "offset";
        public const string FieldLimit = "limit";
        public const string FieldOrder = "order";
    }

    public abstract class BoxEventCollection
    {
        public const string FieldChunkSize = "chunk_size";
        public const string FieldNextStreamPosition = "next_stream_position";
        public const string FieldEntries = "entries";
    }

    public abstract class BoxLongPollInfoCollection
    {
        public const string FieldChunkSize = "chunk_size";
        public const string FieldEntries = "entries";
    }

    public abstract class BoxWebhookCollection
    {
        public const string FieldLimit = "limit";
        public const string FieldNextMarker = "next_marker";
        public const string FieldEntries = "entries";
    }

    public abstract class BoxDevicePinCollection
    {
        public const string FieldEntries = "entries";
        public const string FieldMarker = "next_marker";
        public const string FieldLimit = "limit";
        public const string FieldOrder = "order";
    }

    public abstract class BoxMetadataTemplateCollection
    {
        public const string FieldEntries = "entries";
    }

    public abstract class BoxEnterpriseMetadataTemplateCollection
    {
        public const string FieldEntries = "entries";
        public const string FieldTotalCount = "total_count";
    }
    
    /// <summary>
    /// Box representation of a collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoxCollection<T> : BoxCollection where T : class, new()
    {
        [JsonProperty(PropertyName = FieldTotalCount)]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; set; }

        [JsonProperty(PropertyName = FieldOffset)]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = FieldLimit)]
        public int Limit { get; set; }

        [JsonProperty(PropertyName = FieldOrder)]
        public List<BoxSortOrder> Order { get; set; }

    }

    public class BoxCollectionSingleSortOrder<T> : BoxCollection where T : class, new()
    {
        [JsonProperty(PropertyName = FieldTotalCount)]
        public int TotalCount { get; private set; }

        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; private set; }

        [JsonProperty(PropertyName = FieldOffset)]
        public int Offset { get; private set; }

        [JsonProperty(PropertyName = FieldLimit)]
        public int Limit { get; private set; }

        [JsonProperty(PropertyName = FieldOrder)]
        public BoxSortOrder Order { get; private set; }

    }

    public class BoxEventCollection<T> : BoxEventCollection where T: BoxEnterpriseEvent
    {
        [JsonProperty(PropertyName = FieldChunkSize)]
        public int ChunkSize { get; set; }

        [JsonProperty(PropertyName = FieldNextStreamPosition)]
        public string NextStreamPosition { get; set; }

        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; set; }
    }

    public class BoxLongPollInfoCollection<T> : BoxLongPollInfoCollection where T : BoxLongPollInfo
    {
        [JsonProperty(PropertyName = FieldChunkSize)]
        public int ChunkSize { get; private set; }

        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; private set; }
    }

    public class BoxWebhookCollection<T> : BoxWebhookCollection where T : BoxWebhook
    {
        /// <summary>
        /// Max number of webhooks returned
        /// </summary>
        [JsonProperty(PropertyName = FieldLimit)]
        public int Limit { get; private set; }

        /// <summary>
        /// Marker to use for next request for webhooks. Will be null if no more webhooks.
        /// </summary>
        [JsonProperty(PropertyName = FieldNextMarker)]
        public string NextMarker { get; private set; }

        /// <summary>
        /// List of webhooks returned
        /// </summary>
        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; private set; }
    }

    public class BoxDevicePinCollection<T> : BoxDevicePinCollection where T: BoxDevicePin
    {
        /// <summary>
        /// Default value is 100. Max value is 10000
        /// </summary>
        [JsonProperty(PropertyName = FieldLimit)]
        public int Limit { get; private set; }

        /// <summary>
        /// Needs not be passed or can be empty for first invocation of the API. Use the one returned in response for each subsequent call.
        /// </summary>
        [JsonProperty(PropertyName = FieldMarker)]
        public string NextMarker { get; private set; }

        /// <summary>
        /// List of device pins returned
        /// </summary>
        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; private set; }

        /// <summary>
        /// Default is "asc". Valid values are asc, desc. Case in-sensitive, ASC/DESC works just fine.
        /// </summary>
        [JsonProperty(PropertyName = FieldOrder)]
        public List<BoxSortOrder> Order { get; private set; }
    }

    public class BoxMetadataTemplateCollection<T> : BoxMetadataTemplateCollection where T: Dictionary<string,object>
    {
        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; private set; }
    }

    public class BoxEnterpriseMetadataTemplateCollection<T> : BoxEnterpriseMetadataTemplateCollection where T : BoxMetadataTemplate
    {
        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; private set; }

        [JsonProperty(PropertyName = FieldTotalCount)]
        public int TotalCount { get; private set; }
    }
}
