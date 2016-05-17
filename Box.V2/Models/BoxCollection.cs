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
    
    /// <summary>
    /// Box representation of a collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BoxCollection<T> : BoxCollection where T : class, new()
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
        public List<BoxSortOrder> Order { get; private set; }

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
        public int ChunkSize { get; private set; }

        [JsonProperty(PropertyName = FieldNextStreamPosition)]
        public string NextStreamPosition { get; private set; }

        [JsonProperty(PropertyName = FieldEntries)]
        public List<T> Entries { get; private set; }
    }
}
