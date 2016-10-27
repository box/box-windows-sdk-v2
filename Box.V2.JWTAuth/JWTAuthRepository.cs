﻿using Box.V2.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.JWTAuth
{
    /// <summary>
    /// JWT auth repository used by an AdminClient or UserClient
    /// </summary>
    public class JWTAuthRepository : IAuthRepository
    {
        /// <summary>
        /// OAuth session
        /// </summary>
        public OAuthSession Session { get; private set; }

        /// <summary>
        /// Box Authentication using a JSON Web Token (JWT)
        /// </summary>
        public BoxJWTAuth BoxJWTAuth { get; private set; }

        /// <summary>
        /// Id of the user
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// Event fired after authetication
        /// </summary>
        public event EventHandler<SessionAuthenticatedEventArgs> SessionAuthenticated;

        /// <summary>
        /// Event fired when session is invalidated 
        /// </summary>
        public event EventHandler SessionInvalidated;

        /// <summary>
        /// Constructor JWT auth. repository
        /// </summary>
        /// <param name="session">OAuth session</param>
        /// <param name="boxJWTAuth">JWT authentication</param>
        /// <param name="userId">Id of the user</param>
        public JWTAuthRepository(OAuthSession session, BoxJWTAuth boxJWTAuth, string userId = null)
        {
            this.Session = session;
            this.BoxJWTAuth = boxJWTAuth;
            this.UserId = userId;
        }
        /// <summary>
        /// Not used for this type of authentication
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        public Task<OAuthSession> AuthenticateAsync(string authCode)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Not used for this type of authentication
        /// </summary>
        /// <returns></returns>
        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a new access token using BoxJWTAuth 
        /// </summary>
        /// <param name="accessToken">This input is not used. Could be set to null</param>
        /// <returns>OAuth session</returns>
        public async Task<OAuthSession> RefreshAccessTokenAsync(string accessToken)
        {
            OAuthSession session = null;

            if (UserId != null)
            {
                session = this.BoxJWTAuth.Session(this.BoxJWTAuth.UserToken(this.UserId));
            }
            else
            {
                session = this.BoxJWTAuth.Session(this.BoxJWTAuth.AdminToken());
            }

            this.Session = session;
            OnSessionAuthenticated(session);

            return session;
        }

        private void OnSessionAuthenticated(OAuthSession session)
        {
            var handler = SessionAuthenticated;
            if (handler != null)
            {
                handler(this, new SessionAuthenticatedEventArgs(session));
            }
        }
    }
}
