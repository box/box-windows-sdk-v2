using Box.Sdk.Gen;
using Box.Sdk.Gen.Internal;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class BoxDeveloperTokenAuth : IAuthentication {
        internal string Token { get; }

        /// <summary>
        /// Configuration object of DeveloperTokenAuth.
        /// </summary>
        internal DeveloperTokenConfig Config { get; }

        /// <summary>
        /// An object responsible for storing token. If no custom implementation provided, the token will be stored in memory.
        /// </summary>
        public ITokenStorage TokenStorage { get; }

        public BoxDeveloperTokenAuth(string token, DeveloperTokenConfig? config = default) {
            Token = token;
            Config = config ?? new DeveloperTokenConfig();
            TokenStorage = new InMemoryTokenStorage(token: new AccessToken() { AccessTokenField = this.Token });
        }
        /// <summary>
        /// Retrieves stored developer token
        /// </summary>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task<AccessToken> RetrieveTokenAsync(NetworkSession? networkSession = null) {
            AccessToken? token = await this.TokenStorage.GetAsync().ConfigureAwait(false);
            if (token == null) {
                throw new BoxSdkException(message: "No access token is available.");
            }
            return NullableUtils.Unwrap(token);
        }

        /// <summary>
        /// Developer token cannot be refreshed
        /// </summary>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task<AccessToken> RefreshTokenAsync(NetworkSession? networkSession = null) {
            throw new BoxSdkException(message: "Developer token has expired. Please provide a new one.");
        }

        public async System.Threading.Tasks.Task<string> RetrieveAuthorizationHeaderAsync(NetworkSession? networkSession = null) {
            AccessToken token = await this.RetrieveTokenAsync(networkSession: networkSession).ConfigureAwait(false);
            return string.Concat("Bearer ", token.AccessTokenField);
        }

        /// <summary>
        /// Revoke an active Access Token, effectively logging a user out that has been previously authenticated.
        /// </summary>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task RevokeTokenAsync(NetworkSession? networkSession = null) {
            AccessToken? token = await this.TokenStorage.GetAsync().ConfigureAwait(false);
            if (token == null) {
                return;
            }
            AuthorizationManager authManager = new AuthorizationManager(networkSession: networkSession != null ? NullableUtils.Unwrap(networkSession) : new NetworkSession());
            await authManager.RevokeAccessTokenAsync(requestBody: new PostOAuth2Revoke() { ClientId = this.Config.ClientId, ClientSecret = this.Config.ClientSecret, Token = NullableUtils.Unwrap(token).AccessTokenField }).ConfigureAwait(false);
            await this.TokenStorage.ClearAsync().ConfigureAwait(false);
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
            if (token == null || NullableUtils.Unwrap(token).AccessTokenField == null) {
                throw new BoxSdkException(message: "No access token is available.");
            }
            AuthorizationManager authManager = new AuthorizationManager(networkSession: networkSession != null ? NullableUtils.Unwrap(networkSession) : new NetworkSession());
            AccessToken downscopedToken = await authManager.RequestAccessTokenAsync(requestBody: new PostOAuth2Token(grantType: PostOAuth2TokenGrantTypeField.UrnIetfParamsOauthGrantTypeTokenExchange) { SubjectToken = NullableUtils.Unwrap(token).AccessTokenField, SubjectTokenType = PostOAuth2TokenSubjectTokenTypeField.UrnIetfParamsOauthTokenTypeAccessToken, Scope = string.Join(" ", scopes), Resource = resource, BoxSharedLink = sharedLink }).ConfigureAwait(false);
            return downscopedToken;
        }

    }
}