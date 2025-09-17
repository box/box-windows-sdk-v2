using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using System;
using Box.Sdk.Gen.Schemas;
using Box.Sdk.Gen.Managers;

namespace Box.Sdk.Gen {
    public class BoxOAuth : IAuthentication {
        /// <summary>
        /// Configuration object of OAuth.
        /// </summary>
        internal OAuthConfig Config { get; }

        /// <summary>
        /// An object responsible for storing token. If no custom implementation provided, the token will be stored in memory.
        /// </summary>
        public ITokenStorage TokenStorage { get; }

        public BoxOAuth(OAuthConfig config) {
            Config = config;
            TokenStorage = this.Config.TokenStorage;
        }
        /// <summary>
        /// Get the authorization URL for the app user.
        /// </summary>
        /// <param name="options">
        /// 
        /// </param>
        public string GetAuthorizeUrl(GetAuthorizeUrlOptions? options = default) {
            options = options ?? new GetAuthorizeUrlOptions();
            Dictionary<string, string> paramsMap = Utils.PrepareParams(map: new Dictionary<string, string>() { { "client_id", options.ClientId != null ? options.ClientId : this.Config.ClientId }, { "response_type", options.ResponseType != null ? options.ResponseType : "code" }, { "redirect_uri", options.RedirectUri }, { "state", options.State }, { "scope", options.Scope } });
            return string.Concat("https://account.box.com/api/oauth2/authorize?", JsonUtils.SdToUrlParams(data: SimpleJsonSerializer.Serialize(paramsMap)));
        }

        /// <summary>
        /// Acquires token info using an authorization code.
        /// </summary>
        /// <param name="authorizationCode">
        /// The authorization code to use to get tokens.
        /// </param>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task<AccessToken> GetTokensAuthorizationCodeGrantAsync(string authorizationCode, NetworkSession? networkSession = null) {
            AuthorizationManager authManager = new AuthorizationManager(networkSession: networkSession != null ? NullableUtils.Unwrap(networkSession) : new NetworkSession());
            AccessToken token = await authManager.RequestAccessTokenAsync(requestBody: new PostOAuth2Token(grantType: PostOAuth2TokenGrantTypeField.AuthorizationCode) { ClientId = this.Config.ClientId, ClientSecret = this.Config.ClientSecret, Code = authorizationCode }).ConfigureAwait(false);
            await this.TokenStorage.StoreAsync(token: token).ConfigureAwait(false);
            return token;
        }

        /// <summary>
        /// Get the current access token. If the current access token is expired or not found, this method will attempt to refresh the token.
        /// </summary>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task<AccessToken> RetrieveTokenAsync(NetworkSession? networkSession = null) {
            AccessToken? token = await this.TokenStorage.GetAsync().ConfigureAwait(false);
            if (token == null) {
                throw new BoxSdkException(message: "Access and refresh tokens not available. Authenticate before making any API call first.");
            }
            return NullableUtils.Unwrap(token);
        }

        /// <summary>
        /// Get a new access token for the platform app user.
        /// </summary>
        /// <param name="networkSession">
        /// An object to keep network session state
        /// </param>
        public async System.Threading.Tasks.Task<AccessToken> RefreshTokenAsync(NetworkSession? networkSession = null) {
            AccessToken? oldToken = await this.TokenStorage.GetAsync().ConfigureAwait(false);
            string? tokenUsedForRefresh = oldToken != null ? NullableUtils.Unwrap(oldToken).RefreshToken : null;
            AuthorizationManager authManager = new AuthorizationManager(networkSession: networkSession != null ? NullableUtils.Unwrap(networkSession) : new NetworkSession());
            AccessToken token = await authManager.RequestAccessTokenAsync(requestBody: new PostOAuth2Token(grantType: PostOAuth2TokenGrantTypeField.RefreshToken) { ClientId = this.Config.ClientId, ClientSecret = this.Config.ClientSecret, RefreshToken = tokenUsedForRefresh }).ConfigureAwait(false);
            await this.TokenStorage.StoreAsync(token: token).ConfigureAwait(false);
            return token;
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