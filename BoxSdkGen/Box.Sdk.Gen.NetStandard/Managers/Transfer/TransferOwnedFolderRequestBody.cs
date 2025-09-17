using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class TransferOwnedFolderRequestBody : ISerializable {
        /// <summary>
        /// The user who the folder will be transferred to.
        /// </summary>
        [JsonPropertyName("owned_by")]
        public TransferOwnedFolderRequestBodyOwnedByField OwnedBy { get; set; }

        public TransferOwnedFolderRequestBody(TransferOwnedFolderRequestBodyOwnedByField ownedBy) {
            OwnedBy = ownedBy;
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