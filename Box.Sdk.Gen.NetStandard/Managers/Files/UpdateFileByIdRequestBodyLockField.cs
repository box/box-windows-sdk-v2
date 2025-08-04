using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class UpdateFileByIdRequestBodyLockField : ISerializable {
        /// <summary>
        /// The type of this object.
        /// </summary>
        [JsonPropertyName("access")]
        [JsonConverter(typeof(StringEnumConverter<UpdateFileByIdRequestBodyLockAccessField>))]
        public StringEnum<UpdateFileByIdRequestBodyLockAccessField> Access { get; set; }

        /// <summary>
        /// Defines the time at which the lock expires.
        /// </summary>
        [JsonPropertyName("expires_at")]
        public System.DateTimeOffset? ExpiresAt { get; set; }

        /// <summary>
        /// Defines if the file can be downloaded while it is locked.
        /// </summary>
        [JsonPropertyName("is_download_prevented")]
        public bool? IsDownloadPrevented { get; set; }

        public UpdateFileByIdRequestBodyLockField() {
            
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