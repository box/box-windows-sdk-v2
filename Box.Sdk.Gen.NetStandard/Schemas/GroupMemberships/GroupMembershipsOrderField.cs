using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class GroupMembershipsOrderField : ISerializable {
        /// <summary>
        /// The field to order by.
        /// </summary>
        [JsonPropertyName("by")]
        public string By { get; set; }

        /// <summary>
        /// The direction to order by, either ascending or descending.
        /// </summary>
        [JsonPropertyName("direction")]
        [JsonConverter(typeof(StringEnumConverter<GroupMembershipsOrderDirectionField>))]
        public StringEnum<GroupMembershipsOrderDirectionField> Direction { get; set; }

        public GroupMembershipsOrderField() {
            
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