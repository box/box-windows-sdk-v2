using Box.V2.Contracts;
using Box.V2.Exceptions;
using Box.V2.Services;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public AuthRepository(IBoxConfig boxConfig, IBoxService boxService, IBoxConverter converter) : this(boxConfig, boxService, converter, null) { }

        public AuthRepository(IBoxConfig boxConfig, IBoxService boxService, IBoxConverter converter, OAuthSession session)
        {
            _config = boxConfig;
            _service = boxService;
            _converter = converter;
            Session = session;
        }

        public OAuthSession Session { get; private set; }

        //public Uri AuthCodeUri
        //{
        //    get
        //    {
        //        return new BoxRequest(_config.BoxApiHostUri, Constants.AuthCodeString)
        //                        .Param("response_type", "code")
        //                        .Param("client_id", _config.ClientId)
        //                        .Param("redirect_uri", _config.RedirectUri)
        //                        .AbsoluteUri;
        //    }
        //}

        public async Task<OAuthSession> AuthenticateAsync(string authCode)
        {
            if (string.IsNullOrWhiteSpace(authCode))
                throw new ArgumentException("Auth code cannot be null or empty", "authCode");

            BoxRequest boxRequest = new BoxRequest(_config.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Method(RequestMethod.Post)
                                            .Payload("grant_type", "authorization_code")
                                            .Payload("code", authCode)
                                            .Payload("client_id", _config.ClientId)
                                            .Payload("client_secret", _config.ClientSecret);

            IBoxResponse<OAuthSession> boxResponse = await _service.ToResponseAsync<OAuthSession>(boxRequest);
            boxResponse.ParseResults(_converter);

            using (await _mutex.LockAsync())
                Session = boxResponse.ResponseObject;

            return boxResponse.ResponseObject;

            //throw new BoxException(string.Format("Error authenticating with provided auth code {0}: {1} {2}",
            //                authCode,
            //                boxResponse.Error.Name,
            //                boxResponse.Error.Description));

        }

        public async Task<OAuthSession> RefreshAccessTokenAsync(string accessToken)
        {
            OAuthSession session;

            using (await _mutex.LockAsync())
            {
                if (_expiredTokens.Contains(accessToken))
                    session = Session;
                else
                {
                    session = await ExchangeRefreshToken(Session.RefreshToken);
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
                                            .Payload("grant_type", "refresh_token")
                                            .Payload("refresh_token", refreshToken)
                                            .Payload("client_id", _config.ClientId)
                                            .Payload("client_secret", _config.ClientSecret);

            IBoxResponse<OAuthSession> boxResponse = await _service.ToResponseAsync<OAuthSession>(boxRequest);
            boxResponse.ParseResults(_converter);

            // Return new session
            return boxResponse.ResponseObject;
        }

        /// <summary>
        /// Revokes access by invalidating both the access token and refresh token
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            string token;

            using (await _mutex.LockAsync())
                token = Session.AccessToken;

            BoxRequest boxRequest = new BoxRequest(_config.BoxApiHostUri, Constants.RevokeEndpointString)
                                            .Method(RequestMethod.Post)
                                            .Payload("client_id", _config.ClientId)
                                            .Payload("client_secret", _config.ClientSecret)
                                            .Payload("refresh_token", token);

            await _service.ToResponseAsync<OAuthSession>(boxRequest);
        }
    }
}
