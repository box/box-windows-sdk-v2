using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class UploadSessionPlanResponse : ISerializable {
        /// <summary>
        /// The unique identifier for this upload session.
        /// </summary>
        [JsonPropertyName("upload_session_id")]
        public string UploadSessionId { get; set; }

        /// <summary>
        /// Parts that already exist on the server and
        /// do not need to be uploaded again.
        /// </summary>
        [JsonPropertyName("hits")]
        public IReadOnlyList<UploadPartPlanHit> Hits { get; set; }

        /// <summary>
        /// Parts that do not exist on the server and
        /// need to be uploaded.
        /// </summary>
        [JsonPropertyName("misses")]
        public IReadOnlyList<UploadPartPlan> Misses { get; set; }

        public UploadSessionPlanResponse(string uploadSessionId, IReadOnlyList<UploadPartPlanHit> hits, IReadOnlyList<UploadPartPlan> misses) {
            UploadSessionId = uploadSessionId;
            Hits = hits;
            Misses = misses;
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