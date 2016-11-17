using Newtonsoft.Json;
using System.Collections.Generic;

namespace Box.V2.Models
{
    public abstract class BoxCollection
    {
        public const string FieldTotalCount = "total_count";
        public const string FieldEntries = "entries";
        public const string FieldOffset = "offset";
        public const string FieldLimit = "limit";
        public const string FieldOrder = "order";
        public const string FieldNextMarker = "next_marker";
    }

    public abstract class BoxCollectionMarkerBased
    {
        public const string FieldEntries = "entries";
        public const string FieldMarker = "next_marker";
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
    /// Box representation of a collection that uses offset and limit fields for paging through results.
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

    /// <summary>
    /// Box representation of a collection that uses the next_marker and limit fields for paging through results.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoxCollectionMarkerBased<T> : BoxCollectionMarkerBased where T : class, new()
    {
        /// <summary>
        /// Number of items to return per request.
        /// </summary>
        [JsonProperty(PropertyName = FieldLimit)]
        public int Limit { get; set; }

        /// <summary>
        /// Should be empty for first invocation of the API. Use the one returned in response for each subsequent call.
        /// </summary>
        [JsonProperty(PropertyName = FieldMarker)]
        public string NextMarker { get; set; }

        /// <summary>
        /// List of items returned.
        /// </summary>
        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; set; }

        /// <summary>
        /// Default is "asc". Valid values are asc, desc. Case in-sensitive, ASC/DESC works just fine.
        /// </summary>
        [JsonProperty(PropertyName = FieldOrder)]

        public List<BoxSortOrder> Order { get; set; }
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
