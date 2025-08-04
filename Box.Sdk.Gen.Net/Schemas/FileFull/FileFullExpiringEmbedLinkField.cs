using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class FileFullExpiringEmbedLinkField : ISerializable {
        /// <summary>
        /// The requested access token.
        /// </summary>
        [JsonPropertyName("access_token")]
        public string? AccessTokenField { get; init; }

        /// <summary>
        /// The time in seconds by which this token will expire.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public long? ExpiresIn { get; init; }

        /// <summary>
        /// The type of access token returned.
        /// </summary>
        [JsonPropertyName("token_type")]
        [JsonConverter(typeof(StringEnumConverter<FileFullExpiringEmbedLinkTokenTypeField>))]
        public StringEnum<FileFullExpiringEmbedLinkTokenTypeField>? TokenType { get; init; }

        /// <summary>
        /// The permissions that this access token permits,
        /// providing a list of resources (files, folders, etc)
        /// and the scopes permitted for each of those resources.
        /// </summary>
        [JsonPropertyName("restricted_to")]
        public IReadOnlyList<FileOrFolderScope>? RestrictedTo { get; init; }

        /// <summary>
        /// The actual expiring embed URL for this file, constructed
        /// from the file ID and access tokens specified in this object.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }

        public FileFullExpiringEmbedLinkField() {
            
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