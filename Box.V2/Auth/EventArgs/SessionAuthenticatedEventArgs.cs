using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Auth
{
    public class SessionAuthenticatedEventArgs : EventArgs
    {
        public SessionAuthenticatedEventArgs(OAuthSession session)
        {
            Session = session;
        }
        public OAuthSession Session { get; set;}
    }
}
