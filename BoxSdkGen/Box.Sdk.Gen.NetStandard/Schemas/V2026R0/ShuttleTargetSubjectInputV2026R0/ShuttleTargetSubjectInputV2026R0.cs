using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleTargetSubjectInputV2026R0 : ISerializable {
        /// <summary>
        /// Whether the Box target is a user or a group.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleTargetSubjectInputV2026R0TypeField>))]
        public StringEnum<ShuttleTargetSubjectInputV2026R0TypeField> Type { get; set; }

        /// <summary>
        /// The identifier of the target Box subject.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        public ShuttleTargetSubjectInputV2026R0(ShuttleTargetSubjectInputV2026R0TypeField type, string id) {
            Type = type;
            Id = id;
        }
        
        [JsonConstructorAttribute]
        internal ShuttleTargetSubjectInputV2026R0(StringEnum<ShuttleTargetSubjectInputV2026R0TypeField> type, string id) {
            Type = type;
            Id = id;
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