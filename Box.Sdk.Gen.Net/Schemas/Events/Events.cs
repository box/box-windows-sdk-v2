using Box.Sdk.Gen;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class Events : ISerializable {
        /// <summary>
        /// The number of events returned in this response.
        /// </summary>
        [JsonPropertyName("chunk_size")]
        public long? ChunkSize { get; init; }

        /// <summary>
        /// The stream position of the start of the next page (chunk)
        /// of events.
        /// </summary>
        [JsonPropertyName("next_stream_position")]
        public EventsNextStreamPositionField? NextStreamPosition { get; init; }

        /// <summary>
        /// A list of events.
        /// </summary>
        [JsonPropertyName("entries")]
        public IReadOnlyList<Event>? Entries { get; init; }

        public Events() {
            
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