using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubItemOperationV2025R0 : ISerializable {
        /// <summary>
        /// The action to perform on a Hub item.
        /// </summary>
        [JsonPropertyName("action")]
        [JsonConverter(typeof(StringEnumConverter<HubItemOperationV2025R0ActionField>))]
        public StringEnum<HubItemOperationV2025R0ActionField> Action { get; }

        /// <summary>
        /// Reference to an item that can be added to a Hub.
        /// </summary>
        [JsonPropertyName("item")]
        public FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0 Item { get; }

        public HubItemOperationV2025R0(HubItemOperationV2025R0ActionField action, FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0 item) {
            Action = action;
            Item = item;
        }
        
        [JsonConstructorAttribute]
        internal HubItemOperationV2025R0(StringEnum<HubItemOperationV2025R0ActionField> action, FileReferenceOrFolderReferenceOrWeblinkReferenceV2025R0 item) {
            Action = action;
            Item = item;
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