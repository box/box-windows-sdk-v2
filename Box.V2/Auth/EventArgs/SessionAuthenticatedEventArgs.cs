using System;

namespace Box.V2.Auth
{
    public class SessionAuthenticatedEventArgs : EventArgs
    {
        public SessionAuthenticatedEventArgs(OAuthSession session)
        {
            Session = session;
        }
        public OAuthSession Session { get; set; }
    }
}
