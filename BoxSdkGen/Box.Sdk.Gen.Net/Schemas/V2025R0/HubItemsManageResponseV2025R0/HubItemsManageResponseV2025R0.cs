using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class HubItemsManageResponseV2025R0 : ISerializable {
        /// <summary>
        /// List of operations performed on Box Hub items.
        /// </summary>
        [JsonPropertyName("operations")]
        public IReadOnlyList<HubItemOperationResultV2025R0> Operations { get; }

        public HubItemsManageResponseV2025R0(IReadOnlyList<HubItemOperationResultV2025R0> operations) {
            Operations = operations;
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