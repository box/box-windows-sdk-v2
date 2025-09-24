using System;
using System.Threading.Tasks;
using Box.V2.Auth;

namespace Box.V2.CCGAuth
{
    /// <summary>
    /// CCG auth repository used by an AdminClient or UserClient
    /// </summary>
    public class CCGAuthRepository : IAuthRepository
    {
        /// <summary>
        /// OAuth session
        /// </summary>
        public OAuthSession Session { get; private set; }

        /// <summary>
        /// Box Authentication using a Client Credentials Grant (CCG)
        /// </summary>
        public BoxCCGAuth BoxCCGAuth { get; private set; }

        /// <summary>
        /// Id of the user
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// Event fired when session is invalidated
        /// </summary>
        public event EventHandler SessionInvalidated;

        /// <summary>
        /// Event fired after authetication
        /// </summary>
        public event EventHandler<SessionAuthenticatedEventArgs> SessionAuthenticated;

        /// <summary>
        /// Constructor CCG auth repository
        /// </summary>
        /// <param name="session">OAuth session</param>
        /// <param name="boxCCGAuth">CCG authentication</param>
        /// <param name="userId">Id of the user</param>
        public CCGAuthRepository(OAuthSession session, BoxCCGAuth boxCCGAuth, string userId = null)
        {
            Session = session;
            BoxCCGAuth = boxCCGAuth;
            UserId = userId;
        }

        /// <summary>
        /// Constructor CCG auth repository
        /// </summary>
        /// <param name="boxCCGAuth">CCG authentication</param>
        /// <param name="userId">Id of the user</param>
        public CCGAuthRepository(BoxCCGAuth boxCCGAuth, string userId = null)
            : this(null, boxCCGAuth, userId)
        {
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
        /// Retrieves a new access token using BoxCCGAuth
        /// </summary>
        /// <param name="accessToken">This input is not used. Could be set to null</param>
        /// <returns>OAuth session</returns>
        public async Task<OAuthSession> RefreshAccessTokenAsync(string accessToken)
        {
            OAuthSession session = UserId != null
                ? BoxCCGAuth.Session(await BoxCCGAuth.UserTokenAsync(UserId).ConfigureAwait(false))
                : BoxCCGAuth.Session(await BoxCCGAuth.AdminTokenAsync().ConfigureAwait(false));

            Session = session;
            OnSessionAuthenticated(session);

            return session;
        }

        private void OnSessionAuthenticated(OAuthSession session)
        {
            SessionAuthenticated?.Invoke(this, new SessionAuthenticatedEventArgs(session));
        }
    }
}
