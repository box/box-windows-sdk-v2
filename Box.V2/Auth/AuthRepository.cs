using Box.V2.Contracts;
using Box.V2.Services;
using Box.V2.Web;
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
        

        public AuthRepository(IBoxConfig boxConfig, IBoxService boxService)
        {
            _config = boxConfig;
            _service = boxService;
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

            OAuthSession session;

            BoxRequest boxRequest = new BoxRequest(RequestMethod.POST, _config.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Param("grant_type", "authorization_code")
                                            .Param("code", authCode)
                                            .Param("client_id", _config.ClientId)
                                            .Param("client_secret", _config.ClientSecret);

            session = await _service.ToResponse<OAuthSession>(boxRequest);

            using (await _mutex.LockAsync())
                Session = session;

            return session;
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
                    session = await RefreshAccessToken();
                    Session = session;

                    // Add the expired token to the list so subsequent calls will get new acces token
                    _expiredTokens.Add(accessToken);
                }
            }

            return session;
        }

        private async Task<OAuthSession> RefreshAccessToken()
        {
            BoxRequest boxRequest = new BoxRequest(RequestMethod.POST, _config.BoxApiHostUri, Constants.AuthTokenEndpointString)
                                            .Param("grant_type", "refresh_token")
                                            .Param("refresh_token", Session.RefreshToken)
                                            .Param("client_id", _config.ClientId)
                                            .Param("client_secret", _config.ClientSecret);

            // Return new session
            return await _service.ToResponse<OAuthSession>(boxRequest);
        }
    }
}
