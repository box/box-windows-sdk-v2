using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public class HubCopyRequestV2025R0 : ISerializable {
        /// <summary>
        /// Title of the Box Hub. It cannot be empty and should be less than 50 characters.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Description of the Box Hub.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// If true, the items which the user has Editor or Owner access to in the original Box Hub will be copied to the new Box Hub.
        /// Defaults to false.
        /// </summary>
        [JsonPropertyName("include_items")]
        public bool? IncludeItems { get; set; }

        public HubCopyRequestV2025R0() {
            
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