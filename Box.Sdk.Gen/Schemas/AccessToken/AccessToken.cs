using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Schemas {
    public class AccessToken : ISerializable {
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
        [JsonConverter(typeof(StringEnumConverter<AccessTokenTokenTypeField>))]
        public StringEnum<AccessTokenTokenTypeField>? TokenType { get; init; }

        /// <summary>
        /// The permissions that this access token permits,
        /// providing a list of resources (files, folders, etc)
        /// and the scopes permitted for each of those resources.
        /// </summary>
        [JsonPropertyName("restricted_to")]
        public IReadOnlyList<FileOrFolderScope>? RestrictedTo { get; init; }

        /// <summary>
        /// The refresh token for this access token, which can be used
        /// to request a new access token when the current one expires.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; init; }

        /// <summary>
        /// The type of downscoped access token returned. This is only
        /// returned if an access token has been downscoped.
        /// </summary>
        [JsonPropertyName("issued_token_type")]
        [JsonConverter(typeof(StringEnumConverter<AccessTokenIssuedTokenTypeField>))]
        public StringEnum<AccessTokenIssuedTokenTypeField>? IssuedTokenType { get; init; }

        public AccessToken() {
            
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