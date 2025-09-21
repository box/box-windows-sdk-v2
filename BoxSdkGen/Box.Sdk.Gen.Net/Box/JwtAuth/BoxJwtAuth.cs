using System.Text.Json.Serialization;
using System.Collections.Generic;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen;
using System.Collections.ObjectModel;
using System;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class BoxJwtAuth : IAuthentication {
        /// <summary>
        /// An object containing all JWT configuration to use for authentication
        /// </summary>
        internal JwtConfig Config { get; }

        /// <summary>
        /// An object responsible for storing token. If no custom implementation provided, the token will be stored in memory.
        /// </summary>
        public ITokenStorage TokenStorage { get; }

        /// <summary>
        /// The ID of the user or enterprise to authenticate as. If not provided, defaults to the enterprise ID if set, otherwise defaults to the user ID.
        /// </summary>
        internal string? SubjectId { get; }

        /// <summary>
        /// The type of the subject ID provided. Must be either 'user' or 'enterprise'.
        /// </summary>
        internal string? SubjectType { get; }

        public BoxJwtAuth(JwtConfig config) {
            Config = config;
            TokenStorage = this.Config.TokenStorage;
            SubjectId = this.Config.EnterpriseId != null ? this.Config.EnterpriseId : this.Config.UserId;
            SubjectType = this.Config.EnterpriseId != null ? "enterprise" : "user";
        }
        /// <summary>
        /// Get new access token using JWT auth.
        /// </summary>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task<AccessToken> RefreshTokenAsync(NetworkSession? networkSession = null) {
            if (Utils.IsBrowser()) {
                throw new BoxSdkException(message: "JWT auth is not supported in browser environment.");
            }
            JwtAlgorithm alg = this.Config.Algorithm != null ? NullableUtils.Unwrap(this.Config.Algorithm) : JwtAlgorithm.Rs256;
            Dictionary<string, object> claims = new Dictionary<string, object>() { { "exp", Utils.GetEpochTimeInSeconds() + 30 }, { "box_sub_type", this.SubjectType } };
            JwtSignOptions jwtOptions = new JwtSignOptions(algorithm: alg, audience: "https://api.box.com/oauth2/token", subject: this.SubjectId, issuer: this.Config.ClientId, jwtid: Utils.GetUUID(), keyid: this.Config.JwtKeyId, privateKeyDecryptor: this.Config.PrivateKeyDecryptor);
            JwtKey jwtKey = new JwtKey(key: this.Config.PrivateKey, passphrase: this.Config.PrivateKeyPassphrase);
            string assertion = JwtUtils.CreateJwtAssertion(claims: claims, key: jwtKey, options: jwtOptions);
            AuthorizationManager authManager = new AuthorizationManager(networkSession: networkSession != null ? NullableUtils.Unwrap(networkSession) : new NetworkSession());
            AccessToken token = await authManager.RequestAccessTokenAsync(requestBody: new PostOAuth2Token(grantType: PostOAuth2TokenGrantTypeField.UrnIetfParamsOauthGrantTypeJwtBearer) { Assertion = assertion, ClientId = this.Config.ClientId, ClientSecret = this.Config.ClientSecret }).ConfigureAwait(false);
            await this.TokenStorage.StoreAsync(token).ConfigureAwait(false);
            return token;
        }

        /// <summary>
        /// Get the current access token. If the current access token is expired or not found, this method will attempt to refresh the token.
        /// </summary>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task<AccessToken> RetrieveTokenAsync(NetworkSession? networkSession = null) {
            AccessToken? oldToken = await this.TokenStorage.GetAsync().ConfigureAwait(false);
            if (oldToken == null) {
                AccessToken newToken = await this.RefreshTokenAsync(networkSession: networkSession).ConfigureAwait(false);
                return newToken;
            }
            return oldToken;
        }

        public async System.Threading.Tasks.Task<string> RetrieveAuthorizationHeaderAsync(NetworkSession? networkSession = null) {
            AccessToken token = await this.RetrieveTokenAsync(networkSession: networkSession).ConfigureAwait(false);
            return string.Concat("Bearer ", token.AccessTokenField);
        }

        /// <summary>
        /// Create a new BoxJWTAuth instance that uses the provided user ID as the subject of the JWT assertion.
        /// May be one of this application's created App User. Depending on the configured User Access Level, may also be any other App User or Managed User in the enterprise.
        /// <https://developer.box.com/en/guides/applications/>
        /// <https://developer.box.com/en/guides/authentication/select/>
        /// </summary>
        /// <param name="userId">
        /// The id of the user to authenticate
        /// </param>
        /// <param name="tokenStorage">
        /// Object responsible for storing token in newly created BoxJWTAuth. If no custom implementation provided, the token will be stored in memory.
        /// </param>
        public BoxJwtAuth WithUserSubject(string userId, ITokenStorage? tokenStorage = default) {
            tokenStorage = tokenStorage ?? new InMemoryTokenStorage();
            JwtConfig newConfig = new JwtConfig(clientId: this.Config.ClientId, clientSecret: this.Config.ClientSecret, jwtKeyId: this.Config.JwtKeyId, privateKey: this.Config.PrivateKey, privateKeyPassphrase: this.Config.PrivateKeyPassphrase, tokenStorage: tokenStorage) { EnterpriseId = null, UserId = userId };
            BoxJwtAuth newAuth = new BoxJwtAuth(config: newConfig);
            return newAuth;
        }

        /// <summary>
        /// Create a new BoxJWTAuth instance that uses the provided enterprise ID as the subject of the JWT assertion.
        /// </summary>
        /// <param name="enterpriseId">
        /// The id of the enterprise to authenticate
        /// </param>
        /// <param name="tokenStorage">
        /// Object responsible for storing token in newly created BoxJWTAuth. If no custom implementation provided, the token will be stored in memory.
        /// </param>
        public BoxJwtAuth WithEnterpriseSubject(string enterpriseId, ITokenStorage? tokenStorage = default) {
            tokenStorage = tokenStorage ?? new InMemoryTokenStorage();
            JwtConfig newConfig = new JwtConfig(clientId: this.Config.ClientId, clientSecret: this.Config.ClientSecret, jwtKeyId: this.Config.JwtKeyId, privateKey: this.Config.PrivateKey, privateKeyPassphrase: this.Config.PrivateKeyPassphrase, tokenStorage: tokenStorage) { EnterpriseId = enterpriseId, UserId = null };
            BoxJwtAuth newAuth = new BoxJwtAuth(config: newConfig);
            return newAuth;
        }

        /// <summary>
        /// Downscope access token to the provided scopes. Returning a new access token with the provided scopes, with the original access token unchanged.
        /// </summary>
        /// <param name="scopes">
        /// The scope(s) to apply to the resulting token.
        /// </param>
        /// <param name="resource">
        /// The file or folder to get a downscoped token for. If None and shared_link None, the resulting token will not be scoped down to just a single item. The resource should be a full URL to an item, e.g. https://api.box.com/2.0/files/123456.
        /// </param>
        /// <param name="sharedLink">
        /// The shared link to get a downscoped token for. If None and item None, the resulting token will not be scoped down to just a single item.
        /// </param>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task<AccessToken> DownscopeTokenAsync(IReadOnlyList<string> scopes, string? resource = null, string? sharedLink = null, NetworkSession? networkSession = null) {
            AccessToken? token = await this.RetrieveTokenAsync(networkSession: networkSession).ConfigureAwait(false);
            if (token == null) {
                throw new BoxSdkException(message: "No access token is available. Make an API call to retrieve a token before calling this method.");
            }
            AuthorizationManager authManager = new AuthorizationManager(networkSession: networkSession != null ? NullableUtils.Unwrap(networkSession) : new NetworkSession());
            AccessToken downscopedToken = await authManager.RequestAccessTokenAsync(requestBody: new PostOAuth2Token(grantType: PostOAuth2TokenGrantTypeField.UrnIetfParamsOauthGrantTypeTokenExchange) { SubjectToken = token.AccessTokenField, SubjectTokenType = PostOAuth2TokenSubjectTokenTypeField.UrnIetfParamsOauthTokenTypeAccessToken, Resource = resource, Scope = string.Join(" ", scopes), BoxSharedLink = sharedLink }).ConfigureAwait(false);
            return downscopedToken;
        }

        /// <summary>
        /// Revoke the current access token and remove it from token storage.
        /// </summary>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task RevokeTokenAsync(NetworkSession? networkSession = null) {
            AccessToken? oldToken = await this.TokenStorage.GetAsync().ConfigureAwait(false);
            if (oldToken == null) {
                return;
            }
            AuthorizationManager authManager = new AuthorizationManager(networkSession: networkSession != null ? NullableUtils.Unwrap(networkSession) : new NetworkSession());
            await authManager.RevokeAccessTokenAsync(requestBody: new PostOAuth2Revoke() { Token = oldToken.AccessTokenField, ClientId = this.Config.ClientId, ClientSecret = this.Config.ClientSecret }).ConfigureAwait(false);
            await this.TokenStorage.ClearAsync().ConfigureAwait(false);
        }

    }
}