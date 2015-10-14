using Box.V2.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box.V2.JWTAuth
{
    public class JWTAuthRepository : IAuthRepository
    {
        public OAuthSession Session { get; private set; }
        public BoxJWTAuth BoxJWTAuth { get; private set; }
        public string UserId { get; private set; }

        public event EventHandler<SessionAuthenticatedEventArgs> SessionAuthenticated;
        public event EventHandler SessionInvalidated;

        public JWTAuthRepository(OAuthSession session, BoxJWTAuth boxJWTAuth, string userId=null)
        {
            this.Session = session;
            this.BoxJWTAuth = boxJWTAuth;
            this.UserId = userId;
        }

        public Task<OAuthSession> AuthenticateAsync(string authCode)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<OAuthSession> RefreshAccessTokenAsync(string accessToken)
        {
            OAuthSession session = null;

            if (UserId!=null)
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
