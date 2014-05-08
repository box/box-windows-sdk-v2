using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.Extensions;
using Box.V2.Services;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Box.V2.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private IBoxConfig _config;
        private IBoxService _service;
        private IBoxConverter _converter;

        private List<string> _expiredTokens = new List<string>();

        private readonly AsyncLock _mutex = new AsyncLock();
        public event EventHandler SessionInvalidated;

        /// <summary>
        /// Instantiates a new AuthRepository
        /// </summary>
        /// <param name="boxConfig">The Box configuration that should be used</param>
        /// <param name="boxService">The Box service that will be used to make the requests</param>
        /// <param name="converter">How requests/responses will be serialized/deserialized respectively</param>
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

        public OAuthSession Session { get; private set; }

        public async Task<OAuthSession> AuthenticateAsync(string authCode)
        {
            if (string.IsNullOrWhiteSpace(authCode))
                throw new ArgumentException("Auth code cannot be null or empty", "authCode");

            BoxRequest boxRequest = new BoxRequest(_config.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Method(RequestMethod.Post)
                                            .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.AuthorizationCode)
                                            .Payload(Constants.RequestParameters.Code, authCode)
                                            .Payload(Constants.RequestParameters.ClientId, _config.ClientId)
                                            .Payload(Constants.RequestParameters.ClientSecret, _config.ClientSecret)
                                            .Payload(Constants.RequestParameters.BoxDeviceId, _config.DeviceId)
                                            .Payload(Constants.RequestParameters.BoxDeviceName, _config.DeviceName);

            IBoxResponse<OAuthSession> boxResponse = await _service.ToResponseAsync<OAuthSession>(boxRequest).ConfigureAwait(false);
            boxResponse.ParseResults(_converter);

            using (await _mutex.LockAsync().ConfigureAwait(false))
                Session = boxResponse.ResponseObject;

            return boxResponse.ResponseObject;
        }


        public async Task<OAuthSession> RefreshAccessTokenAsync(string accessToken)
        {
            OAuthSession session;

            using (await _mutex.LockAsync().ConfigureAwait(false))
            {
                if (_expiredTokens.Contains(accessToken))
                    session = Session;
                else
                {
                    session = await ExchangeRefreshToken(Session.RefreshToken).ConfigureAwait(false);
                    Session = session;

                    // Add the expired token to the list so subsequent calls will get new acces token
                    _expiredTokens.Add(accessToken);
                }
            }

            return session;
        }

        private async Task<OAuthSession> ExchangeRefreshToken(string refreshToken)
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
            var handler = SessionInvalidated;
            if (handler != null)
                handler(this, new EventArgs());

            // As well as the caller
            throw new BoxSessionInvalidatedException()
            {
                StatusCode = boxResponse.StatusCode,
            };
        }

        public async Task LogoutAsync()
        {
            string token;

            using (await _mutex.LockAsync().ConfigureAwait(false))
                token = Session.AccessToken;

            BoxRequest boxRequest = new BoxRequest(_config.BoxApiHostUri, Constants.RevokeEndpointString)
                                            .Method(RequestMethod.Post)
                                            .Payload(Constants.RequestParameters.ClientId, _config.ClientId)
                                            .Payload(Constants.RequestParameters.ClientSecret, _config.ClientSecret)
                                            .Payload(Constants.RequestParameters.Token, token);

            await _service.ToResponseAsync<OAuthSession>(boxRequest).ConfigureAwait(false);
        }
    }
}
