using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class HubCollaborationCreateRequestV2025R0HubField : ISerializable {
        /// <summary>
        /// The value will always be `hubs`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<HubCollaborationCreateRequestV2025R0HubTypeField>))]
        public StringEnum<HubCollaborationCreateRequestV2025R0HubTypeField> Type { get; }

        /// <summary>
        /// ID of the object.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; }

        public HubCollaborationCreateRequestV2025R0HubField(string id, HubCollaborationCreateRequestV2025R0HubTypeField type = HubCollaborationCreateRequestV2025R0HubTypeField.Hubs) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal HubCollaborationCreateRequestV2025R0HubField(string id, StringEnum<HubCollaborationCreateRequestV2025R0HubTypeField> type) {
            Type = HubCollaborationCreateRequestV2025R0HubTypeField.Hubs;
            Id = id;
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