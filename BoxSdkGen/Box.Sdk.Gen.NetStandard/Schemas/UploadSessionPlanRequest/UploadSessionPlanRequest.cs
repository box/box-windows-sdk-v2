using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class UploadSessionPlanRequest : ISerializable {
        /// <summary>
        /// The list of parts to check for existence.
        /// </summary>
        [JsonPropertyName("parts")]
        public IReadOnlyList<UploadPartPlan> Parts { get; set; }

        public UploadSessionPlanRequest(IReadOnlyList<UploadPartPlan> parts) {
            Parts = parts;
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