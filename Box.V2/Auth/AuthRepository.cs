// using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.Extensions;
using Box.V2.Services;

namespace Box.V2.Auth
{
    /// <summary>
    /// Default auth repository implementation that will manage the life cycle of the authentication tokens. 
    /// This class can be extended to provide your own token management implementation by overriding the virtual methods
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        protected IBoxConfig _config;
        protected IBoxService _service;
        protected IBoxConverter _converter;

        private readonly List<string> _expiredTokens = new List<string>();
        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);

        /// <summary>
        /// Fires when the authentication session is invalidated
        /// </summary>
        public event EventHandler SessionInvalidated;

        /// <summary>
        /// Fires when a new set of auth token and refresh token pair has been fetched
        /// </summary>
        public event EventHandler<SessionAuthenticatedEventArgs> SessionAuthenticated;

        /// <summary>
        /// Instantiates a new AuthRepository.
        /// </summary>
        /// <param name="boxConfig">The Box configuration that should be used.</param>
        /// <param name="boxService">The Box service that will be used to make the requests.</param>
        /// <param name="converter">How requests/responses will be serialized/deserialized respectively.</param>
        public AuthRepository(IBoxConfig boxConfig, IBoxService boxService, IBoxConverter converter) : this(boxConfig, boxService, converter, null) { }

        /// <summary>
        /// Instantiates a new AuthRepository
        /// </summary>
        /// <param name="boxConfig">The Box configuration that should be used</param>
        /// <param name="boxService">The Box service that will be used to make the requests</param>
        /// <param name="converter">How requests/responses will be serialized/deserialized respectively</param>
        /// <param name="session">The current authenticated session</param>
        public AuthRepository(IBoxConfig boxConfig, IBoxService boxService, IBoxConverter converter, OAuthSession session)
        {
            _config = boxConfig;
            _service = boxService;
            _converter = converter;
            Session = session;
        }

        /// <summary>
        /// The current session of the Box Client
        /// </summary>
        public OAuthSession Session { get; protected set; }

        #region Overrideable Methods

        /// <summary>
        /// Authenticates the session by exchanging the provided auth code for a Access/Refresh token pair
        /// </summary>
        /// <param name="authCode">Authorization Code. The authorization code is only valid for 30 seconds.</param>
        /// <returns>The session of the Box Client after authentification</returns>
        public virtual async Task<OAuthSession> AuthenticateAsync(string authCode)
        {
            OAuthSession session = await ExchangeAuthCode(authCode).ConfigureAwait(false);

            await _mutex.WaitAsync().ConfigureAwait(false);
            // using (await _mutex.LockAsync().ConfigureAwait(false))
            try
            {
                Session = session;

                OnSessionAuthenticated(session);
            }
            finally
            {
                _mutex.Release();
            }

            return session;
        }

        /// <summary>
        /// Refreshes the session by exchanging the access token for a new Access/Refresh token pair. In general,
        /// this method should not need to be called explicitly, as an automatic refresh is invoked when the SDK 
        /// detects that the tokens have expired. 
        /// </summary>
        /// <param name="accessToken">The access token to refresh.</param>
        /// <returns>Refreshed session of Box Client.</returns>
        public virtual async Task<OAuthSession> RefreshAccessTokenAsync(string accessToken)
        {
            OAuthSession session;

            await _mutex.WaitAsync().ConfigureAwait(false);
            // using (await _mutex.LockAsync().ConfigureAwait(false))
            try
            {
                if (_expiredTokens.Contains(accessToken))
                {
                    session = Session;
                }
                else
                {
                    // Add the expired token to the list so subsequent calls will get new acces token. Add
                    // token to the list before making the network call. This way, if refresh fails, subsequent calls
                    // with the same refresh token will not attempt the call. 
                    _expiredTokens.Add(accessToken);

                    session = await ExchangeRefreshToken(Session.RefreshToken).ConfigureAwait(false);
                    Session = session;

                    OnSessionAuthenticated(session);
                }
            }
            finally
            {
                _mutex.Release();
            }

            return session;
        }

        /// <summary>
        /// Logs the current session out by invalidating the current Access/Refresh tokens
        /// </summary>
        public virtual async Task LogoutAsync()
        {
            string token;

            await _mutex.WaitAsync().ConfigureAwait(false);
            //using (await _mutex.LockAsync().ConfigureAwait(false))
            try
            {
                token = Session.AccessToken;
            }
            finally
            {
                _mutex.Release();
            }

            await InvalidateTokens(token).ConfigureAwait(false);
        }

        #endregion


        /// <summary>
        /// Performs the authentication request using the provided auth code
        /// </summary>
        /// <param name="authCode">Authorization Code. The authorization code is only valid for 30 seconds.</param>
        /// <returns>The current session after exchanging Authorization Code</returns>
        protected async Task<OAuthSession> ExchangeAuthCode(string authCode)
        {
            if (string.IsNullOrWhiteSpace(authCode))
                throw new ArgumentException("Auth code cannot be null or empty", "authCode");

            BoxRequest boxRequest = new BoxRequest(_config.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Method(RequestMethod.Post)
                                            .Header(Constants.RequestParameters.UserAgent, _config.UserAgent)
                                            .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.AuthorizationCode)
                                            .Payload(Constants.RequestParameters.Code, authCode)
                                            .Payload(Constants.RequestParameters.ClientId, _config.ClientId)
                                            .Payload(Constants.RequestParameters.ClientSecret, _config.ClientSecret)
                                            .Payload(Constants.RequestParameters.BoxDeviceId, _config.DeviceId)
                                            .Payload(Constants.RequestParameters.BoxDeviceName, _config.DeviceName);

            IBoxResponse<OAuthSession> boxResponse = await _service.ToResponseAsync<OAuthSession>(boxRequest).ConfigureAwait(false);
            boxResponse.ParseResults(_converter);

            return boxResponse.ResponseObject;
        }

        /// <summary>
        /// Performs the refresh request using the provided refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token used to exchange for a new access token. Each refresh_token is valid for one use in 60 days. Every time you get a new access_token by using a refresh_token, we reset your timer for the 60 day period and hand you a new refresh_token</param>
        /// <returns>Refreshed Box Client session</returns>
        protected async Task<OAuthSession> ExchangeRefreshToken(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException("Refresh token cannot be null or empty", "refreshToken");

            BoxRequest boxRequest = new BoxRequest(_config.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Method(RequestMethod.Post)
                                            .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.RefreshToken)
                                            .Payload(Constants.RequestParameters.RefreshToken, refreshToken)
                                            .Payload(Constants.RequestParameters.ClientId, _config.ClientId)
                                            .Payload(Constants.RequestParameters.ClientSecret, _config.ClientSecret)
                                            .Payload(Constants.RequestParameters.BoxDeviceId, _config.DeviceId)
                                            .Payload(Constants.RequestParameters.BoxDeviceName, _config.DeviceName);

            IBoxResponse<OAuthSession> boxResponse = await _service.ToResponseAsync<OAuthSession>(boxRequest).ConfigureAwait(false);
            if (boxResponse.Status == ResponseStatus.Success)
            {
                // Parse and return the new session
                boxResponse.ParseResults(_converter);
                return boxResponse.ResponseObject;
            }

            // The session has been invalidated, notify subscribers
            OnSessionInvalidated();

            // As well as the caller
            throw BoxSessionInvalidatedException.GetResponseException("The API returned an error", boxResponse);
        }

        /// <summary>
        /// Performs the revoke request using the provided access token. This will invalidate both the access and refresh tokens
        /// </summary>
        /// <param name="accessToken">The access token to invalidate</param>
        protected async Task InvalidateTokens(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentException("Access token cannot be null or empty", "accessToken");

            BoxRequest boxRequest = new BoxRequest(_config.BoxApiHostUri, Constants.RevokeEndpointString)
                                            .Method(RequestMethod.Post)
                                            .Payload(Constants.RequestParameters.ClientId, _config.ClientId)
                                            .Payload(Constants.RequestParameters.ClientSecret, _config.ClientSecret)
                                            .Payload(Constants.RequestParameters.Token, accessToken);

            await _service.ToResponseAsync<OAuthSession>(boxRequest).ConfigureAwait(false);
        }

        /// <summary>
        /// Allows sub classes to invoke the SessionInvalidated event
        /// </summary>
        protected void OnSessionInvalidated()
        {
            SessionInvalidated?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Allows sub classes to invoke the SessionAuthenticated event.
        /// </summary>
        ///<param name="session">Authenticated session.</param>
        protected void OnSessionAuthenticated(OAuthSession session)
        {
            SessionAuthenticated?.Invoke(this, new SessionAuthenticatedEventArgs(session));
        }

    }
}
