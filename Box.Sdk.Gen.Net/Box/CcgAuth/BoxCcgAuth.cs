using Box.Sdk.Gen;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class BoxCcgAuth : IAuthentication {
        /// <summary>
        /// Configuration object of Client Credentials Grant auth.
        /// </summary>
        internal CcgConfig Config { get; }

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
        internal PostOAuth2TokenBoxSubjectTypeField? SubjectType { get; }

        public BoxCcgAuth(CcgConfig config) {
            Config = config;
            TokenStorage = this.Config.TokenStorage;
            SubjectId = this.Config.UserId != null ? this.Config.UserId : this.Config.EnterpriseId;
            SubjectType = this.Config.UserId != null ? PostOAuth2TokenBoxSubjectTypeField.User : PostOAuth2TokenBoxSubjectTypeField.Enterprise;
        }
        /// <summary>
        /// Get a new access token using CCG auth
        /// </summary>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task<AccessToken> RefreshTokenAsync(NetworkSession? networkSession = null) {
            AuthorizationManager authManager = new AuthorizationManager(networkSession: networkSession != null ? NullableUtils.Unwrap(networkSession) : new NetworkSession());
            AccessToken token = await authManager.RequestAccessTokenAsync(requestBody: new PostOAuth2Token(grantType: PostOAuth2TokenGrantTypeField.ClientCredentials) { ClientId = this.Config.ClientId, ClientSecret = this.Config.ClientSecret, BoxSubjectType = this.SubjectType, BoxSubjectId = this.SubjectId }).ConfigureAwait(false);
            await this.TokenStorage.StoreAsync(token: token).ConfigureAwait(false);
            return token;
        }

        /// <summary>
        /// Return a current token or get a new one when not available.
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
            return NullableUtils.Unwrap(oldToken);
        }

        public async System.Threading.Tasks.Task<string> RetrieveAuthorizationHeaderAsync(NetworkSession? networkSession = null) {
            AccessToken token = await this.RetrieveTokenAsync(networkSession: networkSession).ConfigureAwait(false);
            return string.Concat("Bearer ", token.AccessTokenField);
        }

        /// <summary>
        /// Create a new BoxCCGAuth instance that uses the provided user ID as the subject ID.
        /// May be one of this application's created App User. Depending on the configured User Access Level, may also be any other App User or Managed User in the enterprise.
        /// <https://developer.box.com/en/guides/applications/>
        /// <https://developer.box.com/en/guides/authentication/select/>
        /// </summary>
        /// <param name="userId">
        /// The id of the user to authenticate
        /// </param>
        /// <param name="tokenStorage">
        /// Object responsible for storing token in newly created BoxCCGAuth. If no custom implementation provided, the token will be stored in memory.
        /// </param>
        public BoxCcgAuth WithUserSubject(string userId, ITokenStorage? tokenStorage = default) {
            tokenStorage = tokenStorage ?? new InMemoryTokenStorage();
            CcgConfig newConfig = new CcgConfig(clientId: this.Config.ClientId, clientSecret: this.Config.ClientSecret, tokenStorage: tokenStorage) { EnterpriseId = this.Config.EnterpriseId, UserId = userId };
            return new BoxCcgAuth(config: newConfig);
        }

        /// <summary>
        /// Create a new BoxCCGAuth instance that uses the provided enterprise ID as the subject ID.
        /// </summary>
        /// <param name="enterpriseId">
        /// The id of the enterprise to authenticate
        /// </param>
        /// <param name="tokenStorage">
        /// Object responsible for storing token in newly created BoxCCGAuth. If no custom implementation provided, the token will be stored in memory.
        /// </param>
        public BoxCcgAuth WithEnterpriseSubject(string enterpriseId, ITokenStorage? tokenStorage = default) {
            tokenStorage = tokenStorage ?? new InMemoryTokenStorage();
            CcgConfig newConfig = new CcgConfig(clientId: this.Config.ClientId, clientSecret: this.Config.ClientSecret, tokenStorage: tokenStorage) { EnterpriseId = enterpriseId, UserId = null };
            return new BoxCcgAuth(config: newConfig);
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
            AccessToken downscopedToken = await authManager.RequestAccessTokenAsync(requestBody: new PostOAuth2Token(grantType: PostOAuth2TokenGrantTypeField.UrnIetfParamsOauthGrantTypeTokenExchange) { SubjectToken = NullableUtils.Unwrap(token).AccessTokenField, SubjectTokenType = PostOAuth2TokenSubjectTokenTypeField.UrnIetfParamsOauthTokenTypeAccessToken, Scope = string.Join(" ", scopes), Resource = resource, BoxSharedLink = sharedLink }).ConfigureAwait(false);
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
            await authManager.RevokeAccessTokenAsync(requestBody: new PostOAuth2Revoke() { ClientId = this.Config.ClientId, ClientSecret = this.Config.ClientSecret, Token = NullableUtils.Unwrap(oldToken).AccessTokenField }).ConfigureAwait(false);
            await this.TokenStorage.ClearAsync().ConfigureAwait(false);
        }

    }
}