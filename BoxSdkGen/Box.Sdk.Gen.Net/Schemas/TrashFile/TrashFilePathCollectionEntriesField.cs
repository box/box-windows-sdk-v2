using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class TrashFilePathCollectionEntriesField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_issequence_idSet")]
        protected bool _isSequenceIdSet { get; set; }

        [JsonInclude]
        [JsonPropertyName("_isetagSet")]
        protected bool _isEtagSet { get; set; }

        protected string? _sequenceId { get; set; }

        protected string? _etag { get; set; }

        /// <summary>
        /// The value will always be `folder`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<TrashFilePathCollectionEntriesTypeField>))]
        public StringEnum<TrashFilePathCollectionEntriesTypeField>? Type { get; init; }

        /// <summary>
        /// The unique identifier that represent a folder.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// This field is null for the Trash folder.
        /// </summary>
        [JsonPropertyName("sequence_id")]
        public string? SequenceId { get => _sequenceId; init { _sequenceId = value; _isSequenceIdSet = true; } }

        /// <summary>
        /// This field is null for the Trash folder.
        /// </summary>
        [JsonPropertyName("etag")]
        public string? Etag { get => _etag; init { _etag = value; _isEtagSet = true; } }

        /// <summary>
        /// The name of the Trash folder.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        public TrashFilePathCollectionEntriesField() {
            
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