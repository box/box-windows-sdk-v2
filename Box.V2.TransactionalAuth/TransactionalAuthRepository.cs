using Box.V2.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.TransactionalAuth
{
    /// <summary>
    /// Reposritory for transactional auth.
    /// </summary>
    /// <seealso cref="Box.V2.Auth.IAuthRepository" />
    public class TransactionalAuthRepository : IAuthRepository
    {
        /// <summary>
        /// The active OAuth2 session
        /// </summary>
        public OAuthSession Session { get; private set; }
        /// <summary>
        /// Gets the transactional authentication.
        /// </summary>
        /// <value>
        /// The transactional authentication.
        /// </value>
        public BoxTransactionalAuth TransactionalAuth { get; private set; }
        /// <summary>
        /// Event for when the session is no longer valid and a new set of Access/Refresh tokens are required
        /// </summary>
        public event EventHandler SessionInvalidated;

        public event EventHandler<SessionAuthenticatedEventArgs> SessionAuthenticated;
        public TransactionalAuthRepository(OAuthSession session, BoxTransactionalAuth boxTransactionalAuth)
        {
            this.Session = session;
            this.TransactionalAuth = boxTransactionalAuth;

        }
        /// <summary>
        /// This method is not implemented
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns>
        /// </returns>
        public Task<OAuthSession> AuthenticateAsync(string authCode)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Create new Oauth session from access token
        /// </summary>
        /// <param name="accessToken">The expired access token</param>
        /// <returns>
        /// A fully authenticated OAuth2 session
        /// </returns>
        public async Task<OAuthSession> RefreshAccessTokenAsync(string accessToken)
        {
            OAuthSession session = null;
            session = this.TransactionalAuth.Session(accessToken);
            this.Session = session;
            OnSessionAuthenticated(session);
            return session;
        }
        /// <summary>
        /// This method is not implemented
        /// </summary>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task LogoutAsync()
        {
            throw new NotImplementedException();
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
