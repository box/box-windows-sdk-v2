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

        private List<string> _expiredTokens = new List<string>();

        private readonly AsyncLock _mutex = new AsyncLock();
        

        public AuthRepository(IBoxConfig boxConfig, IBoxService boxService) : this (boxConfig, boxService, null) { }

        public AuthRepository(IBoxConfig boxConfig, IBoxService boxService, OAuthSession session)
        {
            _config = boxConfig;
            _service = boxService;
            Session = session;
        }

        public OAuthSession Session { get; private set; }

        public Uri AuthCodeUri
        {
            get
            {
                return new BoxRequest(RequestMethod.GET, _config.BoxApiHostUri, Constants.AuthCodeEndpointString)
                        .Param("response_type", "code")
                        .Param("client_id", _config.ClientId)
                        .Param("redirect_uri", _config.RedirectUri)
                        .AbsoluteUri;
            }
        }


        public async Task<OAuthSession> Authenticate(string authCode)
        {
            if (string.IsNullOrWhiteSpace(authCode))
                throw new ArgumentException("Auth code cannot be null or empty", "authCode");

            BoxRequest boxRequest = new BoxRequest(RequestMethod.POST, _config.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Param("grant_type", "authorization_code")
                                            .Param("code", authCode)
                                            .Param("client_id", _config.ClientId)
                                            .Param("client_secret", _config.ClientSecret);

            IBoxResponse<OAuthSession> boxResponse = await _service.ToResponse<OAuthSession>(boxRequest);

            switch (boxResponse.Status)
            {
                case ResponseStatus.Success:
                    using (await _mutex.LockAsync())
                        Session = boxResponse.BoxModel;
                    return boxResponse.BoxModel;
                case ResponseStatus.Error:
                    throw new BoxException(string.Format("{0}: {1}", boxResponse.Error.Name, boxResponse.Error.Description));
                    //switch (boxResponse.Error.Name.Trim())
                    //{
                    //    case "invalid_request":
                    //    case "unauthorized_client":
                    //    case "invalid_grant":
                    //    case "invalid_client":
                    //    case "redirect_uri_mismatch":
                    //    case "insecure_redirect_uri":
                    //    case "invalid_redirect_uri":
                    //}
                    //break;
            }

            throw new BoxException(string.Format("Error authenticating with provided auth code {0}: {1} {2}", 
                            authCode, 
                            boxResponse.Error.Name,
                            boxResponse.Error.Description));
            
        }

        public async Task<OAuthSession> RefreshAccessToken(string accessToken)
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
            BoxRequest boxRequest = new BoxRequest(RequestMethod.POST, _config.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Param("grant_type", "refresh_token")
                                            .Param("refresh_token", refreshToken)
                                            .Param("client_id", _config.ClientId)
                                            .Param("client_secret", _config.ClientSecret);

            IBoxResponse<OAuthSession> boxResponse = await _service.ToResponse<OAuthSession>(boxRequest);
            
            // Return new session
            return boxResponse.BoxModel;
        }
    }
}
