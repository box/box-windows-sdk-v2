﻿using Box.V2.Config;
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

using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using System.IO;

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

        private List<string> _expiredTokens = new List<string>();
        private readonly AsyncLock _mutex = new AsyncLock();

        /// <summary>
        /// Fires when the authenticaiton session is invalidated
        /// </summary>
        public event EventHandler SessionInvalidated;

        /// <summary>
        /// Fires when a new set of auth token and refresh token pair has been fetched
        /// </summary>
        public event EventHandler<SessionAuthenticatedEventArgs> SessionAuthenticated;

        /// <summary>
        /// Instantiates a new AuthRepository
        /// </summary>
        /// <param name="boxConfig">The Box configuration that should be used</param>
        /// <param name="boxService">The Box service that will be used to make the requests</param>
        /// <param name="converter">How requests/responses will be serialized/deserialized respectively</param>
        public AuthRepository(IBoxConfig boxConfig, IBoxService boxService, IBoxConverter converter)
            : this(boxConfig, boxService, converter, null)
        {
        }

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
        /// <param name="authCode"></param>
        /// <returns></returns>
        public virtual async Task<OAuthSession> AuthenticateAsync(string authCode)
        {
            OAuthSession session = await ExchangeAuthCode(authCode);

            using (await _mutex.LockAsync().ConfigureAwait(false))
            {
                Session = session;

                OnSessionAuthenticated(session);
            }

            return session;
        }

        public virtual async Task<OAuthSession> AuthenticateBoxDeveloperEditionAsync()
        {
            string assertion = JWTConstructAssertion();

            BoxRequest boxRequest = new BoxRequest(_config.BoxApiDeveloperEditionTokenUri)
                .Method(RequestMethod.Post)
                .Header(Constants.RequestParameters.UserAgent, _config.UserAgent)
                .Payload(Constants.RequestParameters.GrantType, Constants.RequestParameters.JWT)
                .Payload(Constants.RequestParameters.Assertion, assertion)
                .Payload(Constants.RequestParameters.ClientId, _config.ClientId)
                .Payload(Constants.RequestParameters.ClientSecret, _config.ClientSecret)
                .Payload(Constants.RequestParameters.BoxDeviceId, _config.DeviceId)
                .Payload(Constants.RequestParameters.BoxDeviceName, _config.DeviceName);

            IBoxResponse<OAuthSession> boxResponse = await _service.ToResponseAsync<OAuthSession>(boxRequest).ConfigureAwait(false);
            boxResponse.ParseResults(_converter);

            var session = boxResponse.ResponseObject;

            using (await _mutex.LockAsync().ConfigureAwait(false))
            {
                Session = session;

                OnSessionAuthenticated(session);
            }

            return session;
        }

        /// <summary>
        /// Refreshes the session by exchanging the access token for a new Access/Refresh token pair. In general,
        /// this method should not need to be called explicitly, as an automatic refresh is invoked when the SDK 
        /// detects that the tokens have expired. 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public virtual async Task<OAuthSession> RefreshAccessTokenAsync(string accessToken)
        {
            OAuthSession session;
            using (await _mutex.LockAsync().ConfigureAwait(false))
            {
                if (_expiredTokens.Contains(accessToken))
                {
                    session = Session;
                }
                else
                {
                    // Add the expired token to the list so subsequent calls will get new acces token. Add
                    // token to the list before making the network call. This way, if refresh fails, subsequent calls
                    // with the same refresh token will not attempt te call. 
                    _expiredTokens.Add(accessToken);

                    // if Box Developer Edition
                    if (_config.EntityId && _config.EntityType)
                    {
                        session = await AuthenticateBoxDeveloperEditionAsync();
                    }
                    else
                    {
                        session = await ExchangeRefreshToken(Session.RefreshToken).ConfigureAwait(false);
                    }

                    Session = session;

                    OnSessionAuthenticated(session);
                }
            }

            return session;
        }

        /// <summary>
        /// Logs the current session out by invalidating the current Access/Refresh tokens
        /// </summary>
        /// <returns></returns>
        public virtual async Task LogoutAsync()
        {
            string token;

            using (await _mutex.LockAsync().ConfigureAwait(false))
                token = Session.AccessToken;

            await InvalidateTokens(token);
        }

        #endregion

        /// <summary>
        /// Performs the authentication request using the provided auth code
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
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
        /// <param name="refreshToken"></param>
        /// <returns></returns>
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
            throw new BoxSessionInvalidatedException()
            {
                StatusCode = boxResponse.StatusCode,
            };
        }

        /// <summary>
        /// Performs the revoke request using the provided access token. This will invalidate both the access and refresh tokens
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
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

        protected string JWTConstructAssertion()
        {
            var expires = Convert.ToInt64((new DateTime(1970, 1, 1)).AddSeconds(30));

            var payload = new Dictionary<string, object>()
            {
                { "sub", _config.EntityId },
                { "box_sub_type", _config.EntityType },
                { "jti", Convert.ToBase64String(System.Guid.NewGuid().ToByteArray()) },
                { "iss", _config.ClientId },
                { "aud", _config.BoxApiDeveloperEditionTokenUri },
                { "exp", expires }
            };

            var pwf = (new PEMPasswordFinder(_config.PrivateKeyPassword));
            AsymmetricCipherKeyPair key;
            using (var reader = new StringReader(this._config.PrivateKey))
            {
                var pemReader = new PemReader(reader, pwf);
                key = (AsymmetricCipherKeyPair)pemReader.ReadObject();
            }

            var rsa = Org.BouncyCastle.Security.DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)key.Private);

            return Jose.JWT.Encode(payload, rsa, Jose.JwsAlgorithm.RS256);
        }

        protected string JWTConstructAssertionOld()
        {
            byte[] randomNumber = new byte[64];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomNumber);
            }

            var claims = new List<Claim>
            {
                new Claim("sub", _config.EntityId),
                new Claim("box_sub_type", _config.EntityType),
                new Claim("jti", Convert.ToBase64String(randomNumber)),
            };

            var token = new JwtSecurityToken(issuer: _cpnfig.clientId, audience: _config.BoxApiDeveloperEditionTokenUri, claims: claims, expires: DateTime.UtcNow.AddSeconds(30),
                            signingCredentials: this.credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Allows sub classes to invoke the SessionInvalidated event
        /// </summary>
        protected void OnSessionInvalidated()
        {
            var handler = SessionInvalidated;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Allows sub classes to invoke the SessionAuthenticated event
        /// </summary>
        /// <param name="e"></param>
        protected void OnSessionAuthenticated(OAuthSession session)
        {
            var handler = SessionAuthenticated;
            if (handler != null)
            {
                handler(this, new SessionAuthenticatedEventArgs(session));
            }
        }
    }

    class PEMPasswordFinder : IPasswordFinder
    {
        private string pword;

        public PEMPasswordFinder(string password)
        {
            pword = password;
        }

        public char[] GetPassword()
        {
            return pword.ToCharArray();
        }
    }
}
