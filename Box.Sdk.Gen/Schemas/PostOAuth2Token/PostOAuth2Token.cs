using Box.Sdk.Gen;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using System.Collections.Generic;

namespace Box.Sdk.Gen.Schemas {
    public class PostOAuth2Token : ISerializable {
        /// <summary>
        /// The type of request being made, either using a client-side obtained
        /// authorization code, a refresh token, a JWT assertion, client credentials
        /// grant or another access token for the purpose of downscoping a token.
        /// </summary>
        [JsonPropertyName("grant_type")]
        [JsonConverter(typeof(StringEnumConverter<PostOAuth2TokenGrantTypeField>))]
        public StringEnum<PostOAuth2TokenGrantTypeField> GrantType { get; }

        /// <summary>
        /// The Client ID of the application requesting an access token.
        /// 
        /// Used in combination with `authorization_code`, `client_credentials`, or
        /// `urn:ietf:params:oauth:grant-type:jwt-bearer` as the `grant_type`.
        /// </summary>
        [JsonPropertyName("client_id")]
        public string? ClientId { get; init; }

        /// <summary>
        /// The client secret of the application requesting an access token.
        /// 
        /// Used in combination with `authorization_code`, `client_credentials`, or
        /// `urn:ietf:params:oauth:grant-type:jwt-bearer` as the `grant_type`.
        /// </summary>
        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; init; }

        /// <summary>
        /// The client-side authorization code passed to your application by
        /// Box in the browser redirect after the user has successfully
        /// granted your application permission to make API calls on their
        /// behalf.
        /// 
        /// Used in combination with `authorization_code` as the `grant_type`.
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; init; }

        /// <summary>
        /// A refresh token used to get a new access token with.
        /// 
        /// Used in combination with `refresh_token` as the `grant_type`.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; init; }

        /// <summary>
        /// A JWT assertion for which to request a new access token.
        /// 
        /// Used in combination with `urn:ietf:params:oauth:grant-type:jwt-bearer`
        /// as the `grant_type`.
        /// </summary>
        [JsonPropertyName("assertion")]
        public string? Assertion { get; init; }

        /// <summary>
        /// The token to exchange for a downscoped token. This can be a regular
        /// access token, a JWT assertion, or an app token.
        /// 
        /// Used in combination with `urn:ietf:params:oauth:grant-type:token-exchange`
        /// as the `grant_type`.
        /// </summary>
        [JsonPropertyName("subject_token")]
        public string? SubjectToken { get; init; }

        /// <summary>
        /// The type of `subject_token` passed in.
        /// 
        /// Used in combination with `urn:ietf:params:oauth:grant-type:token-exchange`
        /// as the `grant_type`.
        /// </summary>
        [JsonPropertyName("subject_token_type")]
        [JsonConverter(typeof(StringEnumConverter<PostOAuth2TokenSubjectTokenTypeField>))]
        public StringEnum<PostOAuth2TokenSubjectTokenTypeField>? SubjectTokenType { get; init; }

        /// <summary>
        /// The token used to create an annotator token.
        /// This is a JWT assertion.
        /// 
        /// Used in combination with `urn:ietf:params:oauth:grant-type:token-exchange`
        /// as the `grant_type`.
        /// </summary>
        [JsonPropertyName("actor_token")]
        public string? ActorToken { get; init; }

        /// <summary>
        /// The type of `actor_token` passed in.
        /// 
        /// Used in combination with `urn:ietf:params:oauth:grant-type:token-exchange`
        /// as the `grant_type`.
        /// </summary>
        [JsonPropertyName("actor_token_type")]
        [JsonConverter(typeof(StringEnumConverter<PostOAuth2TokenActorTokenTypeField>))]
        public StringEnum<PostOAuth2TokenActorTokenTypeField>? ActorTokenType { get; init; }

        /// <summary>
        /// The space-delimited list of scopes that you want apply to the
        /// new access token.
        /// 
        /// The `subject_token` will need to have all of these scopes or
        /// the call will error with **401 Unauthorized**..
        /// </summary>
        [JsonPropertyName("scope")]
        public string? Scope { get; init; }

        /// <summary>
        /// Full URL for the file that the token should be generated for.
        /// </summary>
        [JsonPropertyName("resource")]
        public string? Resource { get; init; }

        /// <summary>
        /// Used in combination with `client_credentials` as the `grant_type`.
        /// </summary>
        [JsonPropertyName("box_subject_type")]
        [JsonConverter(typeof(StringEnumConverter<PostOAuth2TokenBoxSubjectTypeField>))]
        public StringEnum<PostOAuth2TokenBoxSubjectTypeField>? BoxSubjectType { get; init; }

        /// <summary>
        /// Used in combination with `client_credentials` as the `grant_type`.
        /// Value is determined by `box_subject_type`. If `user` use user ID and if
        /// `enterprise` use enterprise ID.
        /// </summary>
        [JsonPropertyName("box_subject_id")]
        public string? BoxSubjectId { get; init; }

        /// <summary>
        /// Full URL of the shared link on the file or folder
        /// that the token should be generated for.
        /// </summary>
        [JsonPropertyName("box_shared_link")]
        public string? BoxSharedLink { get; init; }

        public PostOAuth2Token(PostOAuth2TokenGrantTypeField grantType) {
            GrantType = grantType;
        }
        
        [JsonConstructorAttribute]
        internal PostOAuth2Token(StringEnum<PostOAuth2TokenGrantTypeField> grantType) {
            GrantType = grantType;
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