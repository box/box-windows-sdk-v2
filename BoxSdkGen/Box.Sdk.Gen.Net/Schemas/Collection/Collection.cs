using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class Collection : ISerializable {
        /// <summary>
        /// The unique identifier for this collection.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The value will always be `collection`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<CollectionTypeField>))]
        public StringEnum<CollectionTypeField>? Type { get; init; }

        /// <summary>
        /// The name of the collection.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonConverter(typeof(StringEnumConverter<CollectionNameField>))]
        public StringEnum<CollectionNameField>? Name { get; init; }

        /// <summary>
        /// The type of the collection. This is used to
        /// determine the proper visual treatment for
        /// collections.
        /// </summary>
        [JsonPropertyName("collection_type")]
        [JsonConverter(typeof(StringEnumConverter<CollectionCollectionTypeField>))]
        public StringEnum<CollectionCollectionTypeField>? CollectionType { get; init; }

        public Collection() {
            
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