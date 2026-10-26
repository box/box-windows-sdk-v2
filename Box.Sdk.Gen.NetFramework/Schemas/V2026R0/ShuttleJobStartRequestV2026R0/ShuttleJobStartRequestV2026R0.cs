using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class ShuttleJobStartRequestV2026R0 : ISerializable {
        /// <summary>
        /// The run mode. Defaults to simulation when omitted.
        /// </summary>
        [JsonPropertyName("mode")]
        [JsonConverter(typeof(StringEnumConverter<ShuttleJobStartRequestV2026R0ModeField>))]
        public StringEnum<ShuttleJobStartRequestV2026R0ModeField> Mode { get; set; }

        public ShuttleJobStartRequestV2026R0() {
            
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