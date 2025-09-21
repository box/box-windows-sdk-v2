using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileFullLockField : ISerializable {
        [JsonInclude]
        [JsonPropertyName("_isapp_typeSet")]
        protected bool _isAppTypeSet { get; set; }

        protected StringEnum<FileFullLockAppTypeField>? _appType { get; set; }

        /// <summary>
        /// The unique identifier for this lock.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; init; }

        /// <summary>
        /// The value will always be `lock`.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(StringEnumConverter<FileFullLockTypeField>))]
        public StringEnum<FileFullLockTypeField>? Type { get; init; }

        [JsonPropertyName("created_by")]
        public UserMini? CreatedBy { get; init; }

        /// <summary>
        /// The time this lock was created at.
        /// </summary>
        [JsonPropertyName("created_at")]
        public System.DateTimeOffset? CreatedAt { get; init; }

        /// <summary>
        /// The time this lock is to expire at, which might be in the past.
        /// </summary>
        [JsonPropertyName("expired_at")]
        public System.DateTimeOffset? ExpiredAt { get; init; }

        /// <summary>
        /// Whether or not the file can be downloaded while locked.
        /// </summary>
        [JsonPropertyName("is_download_prevented")]
        public bool? IsDownloadPrevented { get; init; }

        /// <summary>
        /// If the lock is managed by an application rather than a user, this
        /// field identifies the type of the application that holds the lock.
        /// This is an open enum and may be extended with additional values in
        /// the future.
        /// </summary>
        [JsonPropertyName("app_type")]
        [JsonConverter(typeof(StringEnumConverter<FileFullLockAppTypeField>))]
        public StringEnum<FileFullLockAppTypeField>? AppType { get => _appType; init { _appType = value; _isAppTypeSet = true; } }

        public FileFullLockField() {
            
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